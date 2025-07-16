using UnityEngine;

public class BasicMonster : BaseMonster
{
    float accTime = 0.0f;
    
    void Start()
    {
    }

    void Update()
    {
        accTime += Time.deltaTime;
        if(accTime >= 2.0f)
        {
            Managers.Resource.Destroy(gameObject, 0.0f);
            accTime = 0.0f;
        }
    }
}
