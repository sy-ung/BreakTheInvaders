using UnityEngine;
using System.Collections;

public class SoundClip : MonoBehaviour {

    // Use this for initialization
    public AudioSource m_audiosource;
    public string m_Key;

    private bool m_music;
    private float m_musictimeposition;


    void Awake()
    {
        m_audiosource = gameObject.AddComponent<AudioSource>();
    }

    public void LoadClip(string p_Key, AudioClip p_NewAudioClip, bool p_Loop, float p_Pitch, float p_Volume,bool p_Music)
    {
        m_music = p_Music;
        if (m_music)
            DontDestroyOnLoad(gameObject);
        LoadClip(p_Key, p_NewAudioClip, p_Loop, p_Pitch, p_Volume);
    }

    public void LoadClip(string p_Key, AudioClip p_NewAudioClip, bool p_Loop, float p_Pitch, float p_Volume)
    {
        m_audiosource.clip = p_NewAudioClip;
        m_audiosource.loop = p_Loop;
        m_audiosource.pitch = p_Pitch;
        m_audiosource.volume = p_Volume;
        m_Key = p_Key;
    }

	void Start ()
    {
        m_audiosource.Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_audiosource.isPlaying)
        {
            if(!m_music)
            { 
                GameAudioManager.m_Instance.SoundFinished(m_Key);
                Destroy(gameObject);
            }
        }
    }

    public void StopPlaying()
    {
        m_audiosource.Stop();
    }

    public void Pause()
    {
        if(m_music)
        {
            m_musictimeposition = m_audiosource.time;
            m_audiosource.Stop();
        }
    }

    public void Resume()
    {
        if (m_music)
        {
            m_audiosource.time = m_musictimeposition;
            m_audiosource.Play();
        }
    }
}
