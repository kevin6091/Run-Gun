using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public int HP { get; set; } = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnDamaged(int damage)
    {
        HP -= damage;
    }
}
