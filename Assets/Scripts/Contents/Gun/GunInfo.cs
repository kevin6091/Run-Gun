using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

[CreateAssetMenu(menuName = "GunInfo/New Guninfo")]
public class GunInfo : ScriptableObject
{
    [SerializeField] Gun.Type _type;
    [SerializeField] float _scale = 1;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _modelPrefab;
    [SerializeField] float _bulletSpeedBase;
    [SerializeField] List<Vector3> _fireDirs;
    [SerializeField] Vector3 _offset;

    public Gun.Type type => _type;
    public float scale => _scale;
    public float speedBase => _bulletSpeedBase;
    public List<Vector3> dirs => _fireDirs;
    public Vector3 offset => _offset;

    private void Awake()
    {
        for(int i = 0; i < _fireDirs.Count; ++i)
        {
            _fireDirs[i] = _fireDirs[i].normalized;
        }
    }

    public GameObject CreateBullet(Vector3 startPos)
    {
        GameObject go = Managers.Resource.Instantiate(_bulletPrefab.name);
        go.transform.position = startPos;
        return go;
    }
}
