using UnityEngine;

public class Itembox : MonoBehaviour
{
    enum Type
    {
        Small,
        Big,
    }

    [SerializeField] private Type _type;
    [SerializeField] private UI_Itemtext _text;
    Animator _anim;
    Collider _collider;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        //  Random 통해 테이블내의 업그레이드 타입 고르기
        //  (외부에서 주입 || 현재 총기의 업그레이드 상태에 따라 선택)
        //  자식 UI에게 이름 전달

        _text.text = "Test";
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(true)    //  Player Tag || Layer
        {
            Open();
        }
    }

    private void Open()
    {
        //  Big Box의 경우 빅박스 업그레이드 목록 띄우기

        Managers.Resource.Destroy(gameObject, 0f);
    }
}
