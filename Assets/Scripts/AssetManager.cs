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


        m_sprite.Add("Player", Resources.Load<Sprite>("Images/Player"));
        m_sprite.Add("PlayerBall", Resources.Load<Sprite>("Images/Ball"));



    }

    public Sprite GetSprite(string p_SpriteName)
    {
        return (Sprite)m_sprite[p_SpriteName];
    }

    public AudioClip GetAudioClip(string p_AudioClipName)
    {
        return (AudioClip)m_audioClips[p_AudioClipName];
    }
}
