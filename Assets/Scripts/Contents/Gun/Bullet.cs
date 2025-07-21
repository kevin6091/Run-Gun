using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _damageBase = 10f;
    [SerializeField] float _lifeTime = 2f;

    public float speed { get; set; } = 0f;

    bool isCheckRay = false;
    bool isHit = false;
    Vector3 rayStart = Vector3.zero;
    Vector3 rayEnd = Vector3.zero;

    float accTime = 0f;

    private void OnEnable()
    {
        isCheckRay = false;
        isHit = false;

        accTime = 0f;
    }

    private void OnDisable()
    {
        if(isHit)
        {
            GameObject go = Managers.Resource.Instantiate("Explossion");
            go.transform.localScale = Vector3.one * 0.5f;
            go.transform.position = transform.position;
        }
    }

    private void Update()
    {
        if(!isCheckRay)
        {
            CheckMonster();
        }

        transform.position += transform.forward * speed * Time.deltaTime;

        rayStart += ScrollSystem.Instance.scrollVelocity * Time.deltaTime;
        rayEnd += ScrollSystem.Instance.scrollVelocity * Time.deltaTime;

        Debug.DrawLine(rayStart, rayEnd, Color.red);        //  Reletive
        Debug.DrawLine(rayStart, rayStart + transform.forward * speed * _lifeTime, Color.blue);       //  Absolute
    }

    private void OnDrawGizmos()
    {
        if(isHit)
        {
            Gizmos.DrawCube(rayEnd, Vector3.one);
        }
    }

    private void CheckMonster()
    {
        LayerMask layerMask = LayerMask.GetMask("Monster");
        rayStart = transform.position;
        rayEnd = rayStart + transform.forward * speed * _lifeTime;

        Vector3 scrollVelocity = ScrollSystem.Instance.scrollVelocity;
        Vector3 reletiveVelocity = transform.forward * speed - scrollVelocity;
        Vector3 reletiveDir = reletiveVelocity.normalized;
        float reletiveSpeed = reletiveVelocity.magnitude;

        if (Physics.Raycast(transform.position, reletiveDir, out RaycastHit hit, reletiveSpeed * _lifeTime, layerMask))
        {
            isHit = true;
            rayEnd = hit.point;

            float reletiveDist = (rayEnd - rayStart).magnitude;
            _lifeTime = reletiveDist / reletiveSpeed;

            GameObject go = hit.transform.gameObject;
            BaseMonster monster = go.GetComponent<BaseMonster>();
            StartCoroutine(LatedHit(_lifeTime, monster));
        }

        isCheckRay = true;
    }

    IEnumerator LatedHit(float time, BaseMonster monster)
    {
        if (null == monster)
            yield break;

        yield return new WaitForSeconds(time);


        //  이펙트 생성및 총알 라이프 타임관련 처리 여기서 하기

        monster.OnHit(ComputeDamage());
        Managers.Resource.Destroy(gameObject, 0f);
    }

    float ComputeDamage()
    {
        return _damageBase;
    }
}
