using UnityEngine;
public class MainCameraController : MonoBehaviour
{
    [SerializeField] PlayerController playerController = null;

    void Awake()
    {
        if(playerController == null)
        {
            Debug.LogError("None PlayerController");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
