using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public AudioClip m_DefaultBounceSound;

    //Components created in Code
    private AudioSource m_BallAudioSystem;
    private Rigidbody2D m_RigidBody2D;
    private SpriteRenderer m_SpriteRenderer;


	// Use this for initialization
	void Awake ()
    {
        m_BallAudioSystem = gameObject.AddComponent<AudioSource>();
        m_RigidBody2D = gameObject.AddComponent<Rigidbody2D>();
        m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = AssetManager.m_Instance.GetSprite("PlayerBall");
        m_RigidBody2D.gravityScale = 0;
	}

    void Start()
    {

    }

    public void ChangeBallSprite(Sprite p_NewBallSprite)
    {
        m_SpriteRenderer.sprite = p_NewBallSprite;
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
