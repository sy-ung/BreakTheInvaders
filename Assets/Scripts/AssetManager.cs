using UnityEngine;
//using UnityEditor;
using System.Collections;

public class AssetManager : MonoBehaviour {

    private static AssetManager m_instance;

    public static AssetManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<AssetManager>();
                if(m_instance == null)
                {
                    GameObject t_AssetManagerObject = new GameObject("AssetManagerSingleton");
                    m_instance = t_AssetManagerObject.AddComponent<AssetManager>();
                    LoadAllAssets();
                }
            }
            return m_instance;
        }
    }

    private static Hashtable m_sprite;
    private static Hashtable m_audioClips;
    private static Hashtable m_prefabs;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private static void LoadAllAssets()
    {
        m_sprite = new Hashtable();
        m_audioClips = new Hashtable();
        m_prefabs = new Hashtable();
        
        //Textures
        m_sprite.Add("Player", Resources.Load<Sprite>("Images/Player"));
        m_sprite.Add("PlayerBall", Resources.Load<Sprite>("Images/Ball"));
        m_sprite.Add("EnemyOne", Resources.Load<Sprite>("Images/EnemyBasic"));
        m_sprite.Add("BallBullet", Resources.Load<Sprite>("Images/Bullet"));
        m_sprite.Add("ControlWheel", Resources.Load<Sprite>("Images/Wheel"));


        //Prefabs
        m_prefabs.Add("RedEnemy", Resources.Load<GameObject>("Prefabs/GameObjects/Red_Enemy"));
        m_prefabs.Add("GreenEnemy", Resources.Load<GameObject>("Prefabs/GameObjects/Green_Enemy"));
        m_prefabs.Add("ControlWheel", Resources.Load<GameObject>("Prefabs/UIObjects/ControlWheel"));

    }

    public Sprite GetSprite(string p_SpriteName)
    {
        return (Sprite)m_sprite[p_SpriteName];
    }

    public AudioClip GetAudioClip(string p_AudioClipName)
    {
        return (AudioClip)m_audioClips[p_AudioClipName];
    }

    public GameObject GetPrefab(string p_PrefabName)
    {
        return (GameObject)m_prefabs[p_PrefabName];
    }
}
