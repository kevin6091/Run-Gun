using UnityEditor.SettingsManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _speed = 10.0f;

    void Awake()
    {
        
    }

    void Start()
    {
        Managers.Input.Actions[Define.InputType.Press] -= OnPressing;
        Managers.Input.Actions[Define.InputType.Press] += OnPressing;
    }

    void Update()
    {
    }
    
    void OnPressing(Vector2 touchPosition)
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = touchPosition - playerScreenPosition;

        if (dir.magnitude <= 0.1f)
        {
            return;
        }

        dir = dir.normalized;
        transform.position += new Vector3(dir.x, 0.0f, 0.0f) * Time.deltaTime * _speed;
    }
}
