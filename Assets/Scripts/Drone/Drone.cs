using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Rendering;
using UnityEngine.Splines.Interpolators;

public class Drone : MonoBehaviour, IComparable<Drone>
{
    public DroneController DroneController { get; set; }
    public DroneGun DroneGun { get; set; }
    public static List<Drone> Drones { get; set; } = new List<Drone>();
    public static int MuzzleCount { get; set; }
    [field: SerializeField] private Color BaseColor { get; set; }
    [field: SerializeField] private Color ChargingColor { get; set; }
    private List<Material> Materials { get; set; } = new List<Material>();

    private static Color FinalColor { get; set; }

    private static Action _fireAction = null;
    private static PlayerStat _playerStat = null;
    private static float _accCoolTime = 0.0f;
    public int CompareTo(Drone other)
    {
        return DroneController.DistanceToTarget.CompareTo(other.DroneController.DistanceToTarget);
    }

    private void OnDisable()
    {
        Drones.Remove(this);
    }

    private void Awake()
    {
        Drones.Add(this);
    }

    void Start()
    {
        Transform modelTransform = transform.Find("Drone Black Model");
        Transform fansTransform = modelTransform.Find("Fans");

        foreach (Material ele in modelTransform.GetComponent<Renderer>().materials)
        {
            Materials.Add(ele);
        }
        foreach (Material ele in fansTransform.GetComponent<Renderer>().materials)
        {
            Materials.Add(ele);
        }

        DroneController = GetComponent<DroneController>();
        DroneGun = GetComponent<DroneGun>();

        _fireAction -= DroneGun.Fire;
        _fireAction += DroneGun.Fire;

        if (_playerStat == null)
        {
            _playerStat = (Managers.Scene.CurrentScene as GameScene).GamePlayer.PlayerStat;
            StartCoroutine(Co_Fire());
        }
    }

    void Update()
    {
        foreach (Material ele in Materials)
        {
            ele.SetColor("_EmissionColor", FinalColor);
        }
    }

    IEnumerator Co_Fire()
    {
        float accTime = 0.0f;
        bool boolean = false;

        while (true)
        {
            _accCoolTime += Time.deltaTime;

            ChangeMaterial(ref accTime, ref boolean);

            if (_accCoolTime >= _playerStat.DroneCoolDown)
            {
                _accCoolTime -= _playerStat.DroneCoolDown;

                _fireAction.Invoke();
                MuzzleCount = 0;
            }

            yield return null;
        }
    }

    void ChangeMaterial(ref float accTime, ref bool boolean)
    {
        if (boolean == false)
        {
            accTime -= Time.deltaTime * 5.0f;

            if (accTime <= 0.0f)
            {
                boolean = true;
            }
        }
        else if (boolean == true)
        {
            accTime = _accCoolTime / _playerStat.DroneCoolDown;

            if (accTime >= 1.0f)
            {
                boolean = false;
            }
        }

        FinalColor = Color.Lerp(BaseColor, ChargingColor, accTime);
    }
}
