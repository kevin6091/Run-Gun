using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private EventManager _event = new EventManager();
    private InputManager _input = new InputManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();
    
    private static Managers Instance { get { Init(); return s_instance; } }
    public static EventManager Event { get { return Instance._event; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        Input.OnUpdate();
        Event.OnUpdate();
    }

    public static void OnSceneChanged()
    {
    }

    static void Init()
    {
        if (null == s_instance)
        {
            GameObject gameobject = GameObject.Find("@Managers");
            if (null == gameobject)
            {
                gameobject = new GameObject { name = "@Managers" };
                gameobject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameobject);
            s_instance = gameobject.GetComponent<Managers>();

            /* 자식 매너지 초기화  */
            s_instance.InitChilds();
            /* End */
        }
    }

    private void InitChilds()
    {
        Sound.Init();
        Pool.Init();
        Event.Init();
        Input.Init();
    }

    static public void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Event.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
