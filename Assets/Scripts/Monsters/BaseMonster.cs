using Autodesk.Fbx;
using Define;
using NUnit;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Hit,
        Attack,
        Explosion,
        Die,
        End,
    }

    protected Player Player { get; set; } = null;
    public MonsterState CurState { get; set; } = MonsterState.End;
    protected MonsterState LastState { get; set; } = MonsterState.End;
    protected Animator Anim { get; set; }
    protected AnimatorStateInfo CurAnimState { get; set; }
    protected AnimatorStateInfo NextAnimState { get; set; }
    protected Renderer Renderer { get; set; }
    protected float DissolveAmount { get; set; }

    [field : SerializeField] public int HP { get; set; } = 30;
    [field: SerializeField] public int MaxHP { get; set; } = 30;
    [field: SerializeField] public float AttackRange { get; set; } = 0.0f;
    [field: SerializeField] public float CollisionRange { get; set; } = 0.0f;
    public Material Material { get; set; }

    public bool IsDead => CurState == MonsterState.Die;
    

    protected void OnEnable()
    {
        HP = MaxHP;
        CurState = MonsterState.Idle;
        LastState = MonsterState.Idle;
        DissolveAmount = 0.0f;
        
        if(Material != null)
        {
            Material.SetFloat("_DissolveAmount", DissolveAmount);
        }
    }

    protected virtual void Start()
    {
        Player = (Managers.Scene.CurrentScene as GameScene).GamePlayer;
        Anim = GetComponent<Animator>();
        Renderer = GetComponentInChildren<Renderer>();
    }

    protected virtual void Update()
    {
        // AnimState Update
        CurAnimState = Anim.GetCurrentAnimatorStateInfo(0);
        NextAnimState = Anim.GetNextAnimatorStateInfo(0);

        // 플레이어 충돌검사
        if ((Player.transform.position - transform.position).magnitude <= CollisionRange)
        {
            OnCollision(Player.gameObject);
        }
        else if ((Player.transform.position - transform.position).magnitude <= AttackRange)
        {
            CurState = MonsterState.Attack;
        }

        // 플레이어 지나쳤는지 -> 삭제
        if (Player.transform.position.z - 10.0f > transform.position.z)
        {
            Managers.Monster.Monsters.Remove(gameObject);
            Managers.Resource.Destroy(gameObject, 0.0f);
        }
    }

    public virtual void OnHit(float damage)
    {
        if (CurState != MonsterState.Die)
        {
            HP = Mathf.Max(HP - (int)damage, 0);
            if (0 == HP)
            {
                //  ItemBox 생성
                //  몬스터타입에따라 스몰박스, 빅박스 선택하여 
                //  오브젝트 생성

                CurState = MonsterState.Die;

                return;
            }

            CurState = MonsterState.Hit;

            PlayAnim("Hit", 0.1f);
        }
    }

    public virtual void OnCollision(GameObject ohter)
    {
        // 플레이어랑
        if (ohter.tag == "Player")
        {
            if (CurState != MonsterState.Die)
            {
                CurState = MonsterState.Explosion;
            }
        }
    }

    protected bool IsStateEnter(MonsterState curState)
    {
        if(CurState != LastState && CurState == curState)
        {
            return true;
        }

        return false;
    }

    protected void PlayAnimRandom_Unique((int begin, int end) range, string name, float interpol, MonsterState curState)
    {
        if (CurState != LastState && CurState == curState)
        {
            PlayAnimRandom((0, 3), name, interpol);
        }
    }

    protected void PlayAnim_Unique(string name, float interpol, MonsterState curState)
    {
        if (LastState != curState)
        {
            PlayAnim(name, interpol);
        }
    }

    protected void PlayAnimRandom((int begin, int end) range, string name, float interpol)
    {
        int randomIndex = UnityEngine.Random.Range(range.begin, range.end);
        Anim.CrossFadeInFixedTime($"{name}{randomIndex}", interpol);
    }

    protected void PlayAnim(string name, float interpol)
    {
        Anim.CrossFadeInFixedTime(name, interpol);
    }

    protected bool IsAnimOverTime(string name, float time)
    {
        if (Anim.IsInTransition(0) == false)
        {
            if (CurAnimState.IsName(name) && CurAnimState.normalizedTime >= time)
            {
                return true;
            }
        }

        return false;
    }
}