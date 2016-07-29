using UnityEngine;
using System.Collections;

public class DeathParticle : MonoBehaviour {

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
        m_particlesystem.Play();
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_particlesystem.IsAlive())
            Destroy(gameObject);
	
	}
}
