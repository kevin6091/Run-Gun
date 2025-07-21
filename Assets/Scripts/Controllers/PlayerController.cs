using bullet.fx.pack;
using UnityEditor.SettingsManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _limitX = 5.0f;
    [SerializeField] Gun _gun = null;
    PlayerStat _playerStat = null;
    Animator _anim = null;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_gun == null || _anim == null)
            return;

        if(layerIndex == 0)
        {
            GameObject lHand = _gun.leftHandSocket;
            GameObject rHand = _gun.rightHandSocket;

            SetIK(1f);

            _anim.SetIKPosition(AvatarIKGoal.LeftHand, lHand.transform.position);
            _anim.SetIKPosition(AvatarIKGoal.RightHand, rHand.transform.position);
            _anim.SetIKRotation(AvatarIKGoal.LeftHand, lHand.transform.rotation);
            _anim.SetIKRotation(AvatarIKGoal.RightHand, rHand.transform.rotation);

        }
    }

    void SetIK(float weight)
    {
        _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
        _anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
        _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
    }

    void Start()
    {
        _playerStat = GetComponent<PlayerStat>();

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

        Vector3 newPos = transform.position + new Vector3(dir.x, 0.0f, 0.0f) * Time.deltaTime * _playerStat.SpeedX;
        if (newPos.x >= _limitX || newPos.x <= -_limitX)
        {
            return;
        }

        transform.position = newPos;
    }
}
