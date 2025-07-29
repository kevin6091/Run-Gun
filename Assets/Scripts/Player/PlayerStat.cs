using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [field : SerializeField] public int HP { get; set; } = 50;
    [field : SerializeField] public float SpeedX { get; set; } = 10.0f;
    [field : SerializeField] public float DrainLife { get; set; } = 0.0f;
    [field : SerializeField] public float Critical { get; set; } = 0.0f;
    [field : SerializeField] public float Defence { get; set; } = 0.0f;

    [field : SerializeField] public float GunCoolDown { get; set; } = 1.0f;
    [field: SerializeField] public int GunDamage { get; set; } = 10;
    [field: SerializeField] public float GunLifeTime { get; set; } = 2;
    [field: SerializeField] public float GunSpeed { get; set; } = 10;

    [field : SerializeField] public float DroneCoolDown { get; set; } = 3.0f; 
    [field: SerializeField] public int DroneDamage { get; set; } = 10;
    [field: SerializeField] public float DroneLifeTime { get; set; } = 2;
    [field: SerializeField] public float DroneSpeed { get; set; } = 10;
    [field: SerializeField] public int NumDrone { get; set; } = 0;

    [field : SerializeField] public float TankCoolDown { get; set; } = 1.0f;
    [field : SerializeField] public int TankDamage { get; set; } = 10;
    [field : SerializeField] public int NumTank { get; set; } = 0;
}
