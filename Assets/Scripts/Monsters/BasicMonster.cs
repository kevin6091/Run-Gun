using UnityEngine;

public class BasicMonster : BaseMonster
{
    protected override void Start()
    {
        base.Start();

        CurState = MonsterState.Idle;
    }

    protected override void Update()
    {
        base.Update();

        switch (CurState)
        {
            case MonsterState.Idle:

                PlayAnimRandom_Unique((0, 3), "Idle", 0.1f, MonsterState.Idle);

                if(Anim.IsInTransition(0) == false)
                {
                    if (CurAnimState.normalizedTime >= 1.0f)
                    {
                        PlayAnimRandom((0, 3), "Idle", 0.1f);
                    }
                }
                
                break;

            case MonsterState.Hit:

                if (Anim.IsInTransition(0) == false)
                {
                    if (CurAnimState.normalizedTime >= 1.0f)
                    {
                        CurState = MonsterState.Idle;
                    }
                }

                break;

            case MonsterState.Attack:

                PlayAnimRandom_Unique((0, 2), "Attack", 0.15f, MonsterState.Attack);

                break;

            case MonsterState.Die:
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
}
