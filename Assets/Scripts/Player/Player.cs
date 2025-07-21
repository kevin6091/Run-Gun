using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController PlayerController { get; set; } = null;
    public PlayerStat PlayerStat { get; set; } = null;

    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerStat = GetComponent<PlayerStat>();
    }
}
