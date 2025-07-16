using UnityEditor.SettingsManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _limitX = 5.0f;
    [SerializeField] PlayerStat playerStat = null;

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

        Vector3 newPos = transform.position + new Vector3(dir.x, 0.0f, 0.0f) * Time.deltaTime * playerStat.SpeedX;
        if (newPos.x >= _limitX || newPos.x <= -_limitX)
        {
            return;
        }

        transform.position = newPos;
    }
}
