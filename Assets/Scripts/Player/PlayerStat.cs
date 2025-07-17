using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [field : SerializeField] public int HP { get; set; } = 50;
    [field : SerializeField] public int GunDamage { get; set; } = 30;
    [field : SerializeField] public float SpeedX { get; set; } = 10.0f;
    [field : SerializeField] public float DrainLife { get; set; } = 0.0f;
    [field: SerializeField] public float Critical { get; set; } = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
