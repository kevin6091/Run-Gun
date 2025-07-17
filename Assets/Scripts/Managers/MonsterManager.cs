using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : BaseManager
{
    private float _spawnX = 3.5f;
    private float _spawnY = 10.0f;
    private float _spawnZ = 40.0f;
    private float _accCoolTime = 0.0f;
    private float _spawnScale = 2.5f;

    private int _basicMonsterCount = 3;

    public override void Init()
    {
        base.Init();

        Managers.Event.Subscribe(Define.Events.SpawnMonster, OnSpawnMonster);
    }

    public override void OnUpdate()
    {
    }

    public override void Clear()
    {
        base.Clear();
    }

    private void SpawnMonster(int randomIndex, bool boolean, Vector3 bundlePos, ref GameObject spawnObj)
    {
        if(boolean)
        {
            spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
            spawnObj.transform.position = bundlePos + new Vector3(_spawnX, _spawnY, _spawnZ);
            spawnObj.transform.localScale = Vector3.one * _spawnScale;
        }
        else
        {
            spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
            spawnObj.transform.position = bundlePos + new Vector3(-_spawnX, _spawnY, _spawnZ + 2.0f);
            spawnObj.transform.localScale = Vector3.one * _spawnScale;
        }
    }

    private void OnSpawnMonster()
    {
        Vector3 bundlePos = BundleLoopSystem.Instance.GetLastBundlePosition();

        // 하나 or 둘
        int spawnCount = UnityEngine.Random.Range(1, 3);
        GameObject spawnObj = null;

        for (int i = 0; i < spawnCount; i++)
        {
            // 왼쪽 or 오른쪽
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                int randomIndex = UnityEngine.Random.Range(0, _basicMonsterCount);
                if (spawnObj == null)
                {
                    SpawnMonster(randomIndex, true, bundlePos, ref spawnObj);
                }
                else
                {
                    SpawnMonster(randomIndex, true, bundlePos, ref spawnObj);
                }
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, _basicMonsterCount);
                if (spawnObj == null)
                {
                    SpawnMonster(randomIndex, false, bundlePos, ref spawnObj);
                }
                else
                {
                    SpawnMonster(randomIndex, false, bundlePos, ref spawnObj);
                }
            }
        }
    }
}
