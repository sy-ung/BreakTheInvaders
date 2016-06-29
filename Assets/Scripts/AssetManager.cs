using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class AssetManager : MonoBehaviour {

    private static AssetManager m_Instance;

    private Hashtable m_Textures;
    private Hashtable m_AudioClips;

    public static AssetManager getInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new AssetManager();
        }

        return m_Instance;
    }

    private AssetManager()
    {
        m_Textures = new Hashtable();
        m_AudioClips = new Hashtable();

        LoadAllAssets();
    }

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadAllAssets()
    {
        
        Sprite t_Texture = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Player.png");
        m_Textures.Add("Player",t_Texture);

    }

    public Sprite GetTexture(string p_TextureName)
    {
        return (Sprite) m_Textures[p_TextureName];
    }

    public AudioClip GetAudioClip(string p_AudioClipName)
    {
        return (AudioClip)m_AudioClips[p_AudioClipName];
    }
}
