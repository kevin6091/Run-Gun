using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : BaseManager
{
    private float _spawnX = 3.5f;
    private float _spawnY = 10.0f;
    private float _spawnZ = 10.0f;
    private float _accCoolTime = 0.0f;
    private float _spawnScale = 2.5f;

    private int _basicMonsterCount = 3;

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
                        SpawnMonster(randomIndex, true, ref spawnObj);
                    }
                    else
                    {
                        SpawnMonster(randomIndex, true, ref spawnObj);
                    }
                }
                else
                {
                    int randomIndex = UnityEngine.Random.Range(0, _basicMonsterCount);
                    if (spawnObj == null)
                    {
                        SpawnMonster(randomIndex, false, ref spawnObj);
                    }
                    else
                    {
                        SpawnMonster(randomIndex, false, ref spawnObj);
                    }
                }
            }
        }
    }

    public override void Clear()
    {
        base.Clear();
    }

    private void SpawnMonster(int randomIndex, bool boolean, ref GameObject spawnObj)
    {
        if(boolean)
        {
            spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
            spawnObj.transform.position = new Vector3(_spawnX, _spawnY, _spawnZ);
            spawnObj.transform.localScale = Vector3.one * _spawnScale;
        }
        else
        {
            spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
            spawnObj.transform.position = new Vector3(-_spawnX, _spawnY, _spawnZ + 2.0f);
            spawnObj.transform.localScale = Vector3.one * _spawnScale;
        }
    }
}
