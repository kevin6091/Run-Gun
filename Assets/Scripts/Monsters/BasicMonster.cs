using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMonster : BaseMonster
{
    protected override void Start()
    {
        base.Start();

        CurState = MonsterState.Idle;

        Material material = Renderer.material;
        Material newMaterial = Material = new Material(material);

        Renderer.material = Material;
    }

    protected override void Update()
    {
        base.Update();

        switch (CurState)
        {
            case MonsterState.Idle:

                PlayAnimRandom_Unique((0, 3), "Idle", 0.1f, MonsterState.Idle);

                if (IsAnimOverTime("Idle0", 1.0f) || IsAnimOverTime("Idle1", 1.0f) || IsAnimOverTime("Idle2", 1.0f))
                {
                    PlayAnimRandom((0, 3), "Idle", 0.1f);
                }

                break;

            case MonsterState.Hit:

                if (IsAnimOverTime("Hit", 1.0f))
                {
                    CurState = MonsterState.Idle;
                }

                break;

            case MonsterState.Attack:

                //PlayAnimRandom_Unique((0, 2), "Attack", 0.15f, MonsterState.Attack);

                break;

            case MonsterState.Die:

                PlayAnim_Unique("Die", 0.0f, MonsterState.Die);

                if(IsStateEnter(MonsterState.Die))
                {
                    StartCoroutine(Co_KnockBack());
                }

                DissolveAmount += Time.deltaTime * 2.0f;
                Material.SetFloat("_DissolveAmount", DissolveAmount);

                if (IsAnimOverTime("Die", 1.0f))
                {
                    Managers.Resource.Destroy(gameObject, 0f);
                }

                break;

            case MonsterState.End:
                break;

            default:
                break;
        }

        LastState = CurState;
    }

    public override void OnCollision(GameObject other)
    {
        base.OnCollision(other);


    }

    IEnumerator Co_KnockBack()
    {
        yield return null;

        float accel = 10.0f;

        while (true)
        {
            accel -= Time.deltaTime * 2.0f;

            transform.position += transform.forward * -1.0f * Time.deltaTime * accel;

            if(accel <= 0.0f)
            {
                yield break;
            }
            yield return null;
        }
    }
}
