using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : BaseManager
{
    float _spawnX = 2.25f;
    float _spawnY = 1.5f;
    float _spawnZ = 10.0f;
    private float _accCoolTime = 0.0f;

    private int _basicMonsterCount = 1;
    public float SpawnCool { get; set; } = 3.0f;

    public override void Init()
    {
        base.Init();
    }

    public override void OnUpdate()
    {
        _accCoolTime += Time.deltaTime;
        if(_accCoolTime >= SpawnCool)
        {
            _accCoolTime = 0.0f;

            // 하나 or 둘
            int spawnCount = UnityEngine.Random.Range(1, 3);
            GameObject spawnObj = null;

            for (int i = 0; i < spawnCount; i++)
            {
                // 왼쪽 or 오른쪽
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    int randomIndex = UnityEngine.Random.Range(0, _basicMonsterCount);
                    if(spawnObj == null)
                    {
                        spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
                        spawnObj.transform.position = new Vector3(_spawnX, _spawnY, _spawnZ);
                    }
                    else
                    {
                        Managers.Resource.Instantiate($"BasicMonster_{randomIndex}").transform.position = new Vector3(_spawnX, _spawnY, _spawnZ + 2.0f);
                    }
                }
                else
                {
                    int randomIndex = UnityEngine.Random.Range(0, _basicMonsterCount);
                    if (spawnObj == null)
                    {
                        spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
                        spawnObj.transform.position = new Vector3(-_spawnX, _spawnY, _spawnZ);
                    }
                    else
                    {
                        Managers.Resource.Instantiate($"BasicMonster_{randomIndex}").transform.position = new Vector3(-_spawnX, _spawnY, _spawnZ + 2.0f);
                    }
                }
            }
        }
    }

    public override void Clear()
    {
        base.Clear();
    }
}
