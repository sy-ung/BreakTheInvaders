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
        m_sprite.Add("Player", Resources.Load<Sprite>("Images/Player/playerpaddle"));
        m_sprite.Add("PlayerBall", Resources.Load<Sprite>("Images/Ball"));

        m_sprite.Add("EnemyOne", Resources.Load<Sprite>("Images/EnemyBasic"));
        m_sprite.Add("EnemyTwo", Resources.Load<Sprite>("Images/EnemyBasic2"));
        m_sprite.Add("EnemySpecial", Resources.Load<Sprite>("Images/EnemySpecial"));

        m_sprite.Add("Bullet", Resources.Load<Sprite>("Images/Bullet"));
        m_sprite.Add("PlayerMuzzle", Resources.Load<Sprite>("Images/Player/playerbarrel"));

        m_sprite.Add("ControlWheel", Resources.Load<Sprite>("Images/Wheel"));
        m_sprite.Add("ControlBar", Resources.Load<Sprite>("Images/MovementBar"));
        m_sprite.Add("ShootButton", Resources.Load<Sprite>("Images/ShootButton"));


        //Prefabs
        m_prefabs.Add("RedEnemy", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/Red_Enemy_Type1"));
        m_prefabs.Add("GreenEnemy", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/Green_Enemy_Type1"));
        m_prefabs.Add("BlueEnemy", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/Blue_Enemy_Type2"));


        m_prefabs.Add("ControlWheel", Resources.Load<GameObject>("Prefabs/UIObjects/ControlWheel"));
        m_prefabs.Add("ControlBar", Resources.Load<GameObject>("Prefabs/UIObjects/ControlBar"));
        m_prefabs.Add("ShootButton", Resources.Load<GameObject>("Prefabs/UIObjects/ShootButton"));

        m_prefabs.Add("BulletDefault", Resources.Load<GameObject>("Prefabs/GameObjects/BulletRegular"));
        m_prefabs.Add("EnemyDeathParticle1", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticle1/EnemyDeathParticle1"));
        m_prefabs.Add("MuzzleFlashDefault", Resources.Load<GameObject>("Prefabs/Particles/MuzzleFlash/MuzzleFlash"));


        

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
