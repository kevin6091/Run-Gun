using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    Player _player = null;
    public int HP { get; set; } = 0;

    [field: SerializeField] float CollisionRange { get; set; } = 0.0f;

    void Start()
    {
        _player = (Managers.Scene.CurrentScene as GameScene).GamePlayer;
    }

    public virtual void CollisionCheck()
    {
        if ((_player.transform.position - transform.position).magnitude <= CollisionRange)
        {

        }
    }

    public virtual void OnDamaged(int damage)
    {
        HP -= damage;
    }
}