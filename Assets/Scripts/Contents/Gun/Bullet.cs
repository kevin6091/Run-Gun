using Autodesk.Fbx;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _damageBase = 10f;
    [SerializeField] float _lifeTime = 2f;

    public float speed { get; set; } = 0f;

    bool isCheckRay = false;
    bool isHit = false;
    Vector3 rayStart = Vector3.zero;
    Vector3 rayEnd = Vector3.zero;

    float accTime = 0f;

    PlayerStat PlayerStat { get; set; }

    private void OnEnable()
    {
        isCheckRay = false;
        isHit = false;

        if (PlayerStat == null)
        {
            PlayerStat = (Managers.Scene.CurrentScene as GameScene).GamePlayer.PlayerStat;
        }

        _lifeTime = PlayerStat.GunLifeTime;

        accTime = 0f;
    }

    private void OnDisable()
    {
        if(isHit)
        {
            GameObject go = Managers.Resource.Instantiate("Explossion");
            go.transform.localScale = Vector3.one * 0.5f;
            go.transform.position = transform.position;
        }
    }

    private void Update()
    {
        if(!isCheckRay)
        {
            CheckMonster();
        }

        transform.position += transform.forward * speed * Time.deltaTime;

        rayStart += ScrollSystem.Instance.scrollVelocity * Time.deltaTime;
        rayEnd += ScrollSystem.Instance.scrollVelocity * Time.deltaTime;

        Debug.DrawLine(rayStart, rayEnd, Color.red);        //  Reletive
        Debug.DrawLine(rayStart, rayStart + transform.forward * speed * _lifeTime, Color.blue);       //  Absolute

        // 민재 추가
        // LifeTime
        // 이거 바로 죽이면 이펙트 생성안됨
        //accTime += Time.deltaTime;
        //if (accTime >= _lifeTime)
        //{
        //    Managers.Resource.Destroy(gameObject, 0.0f);
        //}
    }

    private void OnDrawGizmos()
    {
        if(isHit)
        {
            Gizmos.DrawCube(rayEnd, Vector3.one);
        }
    }

    private void CheckMonster()
    {
        LayerMask layerMask = LayerMask.GetMask("Monster");
        rayStart = transform.position;
        rayEnd = rayStart + transform.forward * speed * _lifeTime;

        Vector3 scrollVelocity = ScrollSystem.Instance.scrollVelocity;
        Vector3 reletiveVelocity = transform.forward * speed - scrollVelocity;
        Vector3 reletiveDir = reletiveVelocity.normalized;
        float reletiveSpeed = reletiveVelocity.magnitude;

        if (Physics.Raycast(transform.position, reletiveDir, out RaycastHit hit, reletiveSpeed * _lifeTime, layerMask))
        {
            rayEnd = hit.point;

            float reletiveDist = (rayEnd - rayStart).magnitude;
            _lifeTime = reletiveDist / reletiveSpeed;

            GameObject go = hit.transform.gameObject;
            BaseMonster monster = go.GetComponent<BaseMonster>();
            
            StartCoroutine(LatedHit(_lifeTime, monster));
        }

        isCheckRay = true;
    }

    IEnumerator LatedHit(float time, BaseMonster monster)
    {
        if (null == monster)
            yield break;

        yield return new WaitForSeconds(time);

        if (monster.CurState == BaseMonster.MonsterState.Die) //민재 추가
        {
            isCheckRay = false;
            yield break;
        }
        //  이펙트 생성및 총알 라이프 타임관련 처리 여기서 하기
        isHit = true;
        monster.OnHit(ComputeDamage());
        Managers.Resource.Destroy(gameObject, 0f);
    }

    float ComputeDamage()
    {
        return _damageBase;
    }
}
