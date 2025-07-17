using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _damageBase = 10f;
    [SerializeField] float _lifeTime = 10f;

    public float speed { get; set; } = 0f;

    private void OnEnable()
    {
        Managers.Resource.Destroy(gameObject, _lifeTime);


    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
