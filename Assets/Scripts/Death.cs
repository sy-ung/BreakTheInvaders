using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    private ParticleSystem m_particlesystem;

    void Awake()
    {
        Initialize();
    }
    void Initialize()
    {
        //m_particlesystem = gameObject.AddComponent<ParticleSystem>();
        m_particlesystem = GetComponent<ParticleSystem>();
    }

	// Use this for initialization
	void Start () {
        m_particlesystem.loop = false;
        m_particlesystem.Play();
	    
	}
	
	// Update is called once per frame
	void Update () {
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
