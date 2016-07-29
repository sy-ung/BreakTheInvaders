using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;
    private Rigidbody2D m_rigidbody2D;



    protected float m_lifetime;
    protected string p_SpriteName;

    private float m_timer;

    protected float m_speed;

    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    private BoxCollider2D m_boxcollider2D;

    public GameObject m_MuzzleFlash;

    bool m_alive = true;

    void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite(p_SpriteName);


    }

    protected void Awake()
    {
        Initialize();

        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        m_rigidbody2D.isKinematic = false;

        m_boxcollider2D.size = m_spriterenderer.bounds.size;
        m_lifetime = 1f;
        m_timer = 0;

        Bullet[] t_allbullets = FindObjectsOfType<Bullet>();
        for(int i = 0; i<t_allbullets.Length;i++)
        {
            Physics2D.IgnoreCollision(m_boxcollider2D, t_allbullets[i].GetComponent<BoxCollider2D>());
        }
        
    }

	// Use this for initialization
	void Start () {
        m_rigidbody2D.velocity = Vector3.up * m_speed;
        Physics2D.IgnoreCollision(m_boxcollider2D, BallManager.m_Instance.m_PlayerBall.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(m_boxcollider2D, PlayerManager.m_Instance.m_Player.GetComponent<Collider2D>());
        PlayMuzzleFlash();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_alive)
        { 
            m_timer += Time.deltaTime;
            if (m_timer > m_lifetime)
                Destroy(gameObject);
            else
                CheckBoundry();
        }
    }
    void LateUpdate()
    {
        if (!m_alive)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if(m_alive)
        { 
            if(p_Collision.collider.tag == "Enemy")
            {
                p_Collision.collider.GetComponent<Enemy>().Death();
                Destroy(gameObject);
            }
        }
    }

    void CheckBoundry()
    {
        Vector2 t_screen = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint;

        if (transform.position.y - m_spriterenderer.bounds.size.y/2  > t_screen.y)
        {
            m_alive = false;
        }
    }

    void PlayMuzzleFlash()
    {

    }
}
