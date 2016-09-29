using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    protected ParticleSystem m_particlesystem;


    protected string[] m_DeathsoundClipName;

    protected void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        m_particlesystem = GetComponent<ParticleSystem>();

    }

    // Use this for initialization
    protected void Start ()
    {
        m_particlesystem.loop = false;
        m_particlesystem.Play();

        if(m_DeathsoundClipName != null)
            GameAudioManager.m_Instance.PlaySound(m_DeathsoundClipName[Random.Range(0, m_DeathsoundClipName.Length)],false,1.0f, 0.3f);
	}

    // Update is called once per frame
    protected void Update ()
    {
        if (!m_particlesystem.IsAlive())
        {
            DeathFinished();
        }
    }

    void DeathFinished()
    {
        Destroy(gameObject);
    }
}
