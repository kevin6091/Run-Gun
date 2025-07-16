using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidBody = null;
    Animator _anim = null;

    private void Awake()
    {
        _rigidBody = gameObject.GetOrAddComponent<Rigidbody>();
        _anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Press)] -= OnKeyPress;
        Managers.Input.Actions[(Define.InputEvent.KeyEvent, Define.InputType.Press)] += OnKeyPress;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        _rigidBody.MovePosition(transform.position + Vector3.right * x * 3f * Time.fixedDeltaTime);
    }

    void OnKeyPress(object[] objects)
    {
        
    }
}
