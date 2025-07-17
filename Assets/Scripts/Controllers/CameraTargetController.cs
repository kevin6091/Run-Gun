using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    [SerializeField] PlayerController _playerController = null;
    [SerializeField] float _ratio = 5.0f;

    Vector3 _lastPlayerPosition = Vector3.zero;

    void Awake()
    {
        if(_playerController == null)
        {
            Debug.LogError("None Player");
        }
    }

    void Start()
    {
        _lastPlayerPosition = _playerController.transform.position;
        transform.position = _playerController.transform.position;
    }

    void Update()
    {
        Vector3 dir = _playerController.transform.position - _lastPlayerPosition;
        dir.x /= _ratio;
        transform.position += dir;
        _lastPlayerPosition = _playerController.transform.position;
    }
}
