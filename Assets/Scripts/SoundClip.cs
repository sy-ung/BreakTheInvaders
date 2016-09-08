using UnityEngine;
using System.Collections;

public class SoundClip : MonoBehaviour {

    // Use this for initialization
    AudioSource m_audiosource;
    public string m_Key;
    void Awake()
    {
        m_audiosource = gameObject.AddComponent<AudioSource>();
    }

    public void LoadClip(string p_Key, AudioClip p_NewAudioClip, bool p_Loop, float p_Pitch)
    {
        m_audiosource.clip = p_NewAudioClip;
        m_audiosource.loop = p_Loop;
        m_audiosource.pitch = p_Pitch;
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
            GameAudioManager.m_Instance.SoundFinished(m_Key);
            Destroy(gameObject);
        }
    }

    public void StopPlaying()
    {
        m_audiosource.Stop();
    }
}
