using UnityEngine;
using System.Collections;

public class BallBullet : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;
    private Rigidbody2D m_rigidbody2D;

    private float m_lifetime;
    private float m_timer;

    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    private BoxCollider2D m_boxcollider2D;

    void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("BallBullet");


    }

    void Awake()
    {
        Initialize();

        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        m_rigidbody2D.isKinematic = false;

        m_boxcollider2D.size = m_spriterenderer.bounds.size;
        m_lifetime = 1.5f;
        m_timer = 0;

        BallBullet[] t_allbullets = FindObjectsOfType<BallBullet>();
        for(int i = 0; i<t_allbullets.Length;i++)
        {
            Physics2D.IgnoreCollision(m_boxcollider2D, t_allbullets[i].GetComponent<BoxCollider2D>());
        }

    }

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_lifetime)
            Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if(p_Collision.collider.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(p_Collision.collider, m_boxcollider2D);
        }

        if(p_Collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(p_Collision.collider.gameObject);
        }
    }

}
