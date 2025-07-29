using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;

public class Gun : MonoBehaviour
{
    public enum Type
    {
        _HandGun,
        _ShotGun,
        _AssaultRifle,
        _SniperRifle,
    }

    [SerializeField] GunInfo _gunInfo = null;

    [SerializeField] GameObject _gunFire = null;
    [SerializeField] ParticleSystem _muzzleFlash = null;
    [SerializeField] Animator _anim = null;

    [SerializeField] GameObject _leftHandSocket = null;
    [SerializeField] GameObject _rightHandSocket = null;

    public GameObject leftHandSocket => _leftHandSocket;
    public GameObject rightHandSocket => _rightHandSocket;

    private void Awake()
    {
        _muzzleFlash.Stop(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.one * _gunInfo.scale;
        transform.localPosition += _gunInfo.offset;
    }

    float accTime = 0f;
    // Update is called once per frame
    void Update()
    {
        accTime += Time.deltaTime; 
        while(accTime > 0.5f)
        {
            accTime -= 0.5f;
            //Fire();
        }
    }

    void Fire()
    {
        List<Vector3> dirs = _gunInfo.dirs;
        if(dirs == null)
        {
            return;
        }

        for(int i = 0; i < dirs.Count; ++i)
        {
            Bullet bullet = CreateBullet();

            Quaternion rotation = Quaternion.LookRotation(dirs[i]);
            bullet.transform.rotation = rotation;
            bullet.speed = _gunInfo.speedBase;
        }

        if(_anim != null)
        {
            _anim.Play("Fire", -1, 0f);
        }

        if(_muzzleFlash != null)
        {
            _muzzleFlash.transform.position = _gunFire.transform.position;
            _muzzleFlash.Stop(true);

            _muzzleFlash.Play();
        }
    }

    Bullet CreateBullet()
    {
        GameObject go = _gunInfo.CreateBullet(_gunFire.transform.position);
        Bullet bullet = go.GetComponent<Bullet>();

        return bullet;
    }
}
