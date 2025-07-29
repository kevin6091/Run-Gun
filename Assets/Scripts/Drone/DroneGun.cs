using UnityEngine;

public class DroneGun : MonoBehaviour
{
    [field : SerializeField] public ParticleSystem MuzzleFlash { get; set; }

    void Start()
    {
        MuzzleFlash.Stop();
    }

    void Update()
    {

    }

    public void Fire()
    {
        if(Drone.Drones.Count >= 10)
        {
            if(Drone.MuzzleCount < 10)
            {
                Drone.MuzzleCount++;
                if (MuzzleFlash != null)
                {
                    MuzzleFlash.Stop();
                    MuzzleFlash.Play();
                }
            }
        }
        else
        {
            if (MuzzleFlash != null)
            {
                MuzzleFlash.Stop();
                MuzzleFlash.Play();
            }
        }
    }
}
