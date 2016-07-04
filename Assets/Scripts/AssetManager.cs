using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

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

    static void LoadAllAssets()
    {

        m_sprite = new Hashtable();
        m_audioClips = new Hashtable();


        //Sprite t_Sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Player.png");
        m_sprite.Add("Player", AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Player.png"));
        m_sprite.Add("PlayerBall", AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Ball.png"));

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
