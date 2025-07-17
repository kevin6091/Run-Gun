using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController PlayerController { get; set; } = null;
    public PlayerStat PlayerStat { get; set; } = null;

    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerStat = GetComponent<PlayerStat>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
