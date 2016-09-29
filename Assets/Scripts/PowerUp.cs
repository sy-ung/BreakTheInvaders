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

    protected float m_volume;

    protected PowerUpHighlight m_highlight;

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
        m_speed = 10.0f;
        SpawnHighlight();
        
    }

    void SpawnHighlight()
    {
        GameObject t_highlight = Instantiate(AssetManager.m_Instance.GetPrefab("PowerHighlight"));
        t_highlight.transform.SetParent(transform);
        t_highlight.transform.localScale = new Vector3(1.35f, 1.35f, 0);
        t_highlight.transform.localPosition = Vector2.zero;
        m_highlight = t_highlight.GetComponent<PowerUpHighlight>();

    }

	// Use this for initialization
	protected void Start ()
    {
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.velocity = -Vector3.up * m_spriterenderer.bounds.size.x * m_speed;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        m_rigidbody2D.isKinematic = false;

        m_spriterenderer.sortingLayerName = "PowerUp";
        gameObject.layer = 10;
        gameObject.tag = "PowerUp";

        m_boxcollider2D.isTrigger = true;

        m_volume = 0.7f;

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

    void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (p_Collider.tag == "Player")
            ApplyMuzzlePowerUp(p_Collider);
        else if(p_Collider.tag == "PlayerBall")
            ApplyBallPowerUp();
    
    }

    public virtual void ApplyMuzzlePowerUp(Collider2D p_PlayerCollider)
    {
        if(p_PlayerCollider.GetComponent<Player>().m_MuzzlePowerUpOverwritable)
        { 
            p_PlayerCollider.GetComponent<Player>().ApplyMuzzlePowerUp(m_muzzlepowerup);
            GameAudioManager.m_Instance.PlaySound("PowerUpHitPlayer", false, 1.0f, m_volume);
            Destroy(gameObject);
        }
    }

    public virtual void ApplyBallPowerUp()
    {
        if(BallManager.m_Instance.m_BallPowerupOverwrite)
        { 
            BallManager.m_Instance.ApplyPowerUp(m_ballpowerup);
            GameAudioManager.m_Instance.PlaySound("PowerUpHitBall", false, 1.0f, m_volume);
            Destroy(gameObject);
        }
    }
}
