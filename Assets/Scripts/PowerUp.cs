using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;
    private BoxCollider2D m_boxcollider2D;
    private Rigidbody2D m_rigidbody2D;

    private Vector2 m_screensize;

    protected GameObject m_muzzlepowerup;
    protected GameObject m_ballpowerup;

    protected float m_speed;

    protected void Awake()
    {
        Initialize();

    }

    void Initialize()
    {

        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        m_boxcollider2D.size = m_spriterenderer.bounds.size;
        m_screensize = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint;
        m_speed = 0.0f;
    }

	// Use this for initialization
	protected void Start ()
    {
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.velocity = -Vector3.up * m_spriterenderer.bounds.size.x * m_speed;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        m_rigidbody2D.isKinematic = true;

        m_spriterenderer.sortingLayerName = "PowerUp";
        gameObject.layer = 10;
        gameObject.tag = "PowerUp";

    }
	
	// Update is called once per frame
	protected void Update ()
    {
        BoundryCheck();
	}

    void BoundryCheck()
    {
        Vector2 t_size = m_spriterenderer.bounds.size / 2 ;
        

        if(transform.position.x - t_size.x < -m_screensize.x)
        {
            transform.position = new Vector2(-m_screensize.x + t_size.x, transform.position.y);
        }

        if (transform.position.x + t_size.x > m_screensize.x)
        {
            transform.position = new Vector2(m_screensize.x - t_size.x, transform.position.y);
        }

        if(transform.position.y + t_size.y > m_screensize.y)
        {
            transform.position = new Vector2(transform.position.x, m_screensize.y - t_size.y);
        }

        if(transform.position.y + t_size.y < -m_screensize.y)
        {
            Destroy(gameObject);
        }

    }

    public void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if (p_Collision.collider.tag == "Player")
        {
            ApplyMuzzlePowerUp();
        }

        if (p_Collision.collider.tag == "PlayerBall")
        {
            ApplyBallPowerUp();
        }
    }

    public void ApplyMuzzlePowerUp()
    {
        PlayerManager.m_Instance.ChangeMuzzle(m_muzzlepowerup);
        Destroy(gameObject);
    }

    public void ApplyBallPowerUp()
    {
        BallManager.m_Instance.ChangeBall(m_ballpowerup);
        Destroy(gameObject);
    }
}
