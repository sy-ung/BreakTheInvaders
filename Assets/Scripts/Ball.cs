using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Texture2D m_DefaultBallTexture;
    public AudioClip m_DefaultBounceSound;

    //Components created in Code
    private AudioSource m_BallAudioSystem;
    private Rigidbody2D m_RigidBody2D;



	// Use this for initialization
	void Awake ()
    {
        m_BallAudioSystem = gameObject.AddComponent<AudioSource>();
        m_RigidBody2D = gameObject.AddComponent<Rigidbody2D>();
        m_RigidBody2D.gravityScale = 0;

	}

    void Start()
    {

    }


	
	// Update is called once per frame
	void Update () {
	
	}
}
