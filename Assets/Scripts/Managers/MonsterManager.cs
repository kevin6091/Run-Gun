using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterManager : BaseManager
{
    private float _spawnX = 3.5f;
    private float _spawnY = 10.0f;
    private float _spawnZ = 40.0f;
    private float _accCoolTime = 0.0f;
    private float _spawnScale = 2.5f;

    private int _numBasicMonster = 3;
    private bool _isFirstSpawn = true;

    public Dictionary<GameObject, BaseMonster> Monsters { get; set; } = new Dictionary<GameObject, BaseMonster>();

    public override void Init()
    {
        base.Init();

        Managers.Event.Subscribe(Define.Events.SpawnMonster, OnSpawnMonster);
        _isFirstSpawn = true;
    }

    public override void OnUpdate()
    {
    }

    public override void Clear()
    {
        base.Clear();
    }

    private void SpawnMonster(int randomIndex, Vector3 position, ref GameObject spawnObj)
    {
        spawnObj = Managers.Resource.Instantiate($"BasicMonster_{randomIndex}");
        spawnObj.transform.position = position;
        spawnObj.transform.localScale = Vector3.one * _spawnScale;
    }

    private void OnSpawnMonster()
    {
        Vector3 bundlePos = BundleLoopSystem.Instance.GetLastBundlePosition();
        if (_isFirstSpawn)
        {
            bundlePos += new Vector3(0.0f, 0.0f, 30.0f);
            _isFirstSpawn = false;
        }

        // 하나 or 둘
        int spawnCount = UnityEngine.Random.Range(1, 3);
        GameObject spawnObj = null;

        for (int i = 0; i < spawnCount; i++)
        {
            // 왼쪽 or 오른쪽
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                int randomIndex = UnityEngine.Random.Range(0, _numBasicMonster);
                if (spawnObj == null)
                {
                    SpawnMonster(randomIndex, bundlePos + new Vector3(_spawnX, _spawnY, _spawnZ), ref spawnObj);
                }
                else
                {
                    SpawnMonster(randomIndex, bundlePos + new Vector3(_spawnX, _spawnY, _spawnZ + 7.5f), ref spawnObj);
                }
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, _numBasicMonster);
                if (spawnObj == null)
                {
                    SpawnMonster(randomIndex, bundlePos + new Vector3(-_spawnX, _spawnY, _spawnZ), ref spawnObj);
                }
                else
                {
                    SpawnMonster(randomIndex, bundlePos + new Vector3(-_spawnX, _spawnY, _spawnZ + 7.5f), ref spawnObj);
                }
            }
        }
    }
}
