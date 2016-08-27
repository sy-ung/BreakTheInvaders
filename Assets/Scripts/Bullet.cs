using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;
    public SpriteRenderer m_SpriteRenderer
    {
        get { return m_spriterenderer; }
    }
    private Rigidbody2D m_rigidbody2D;



    protected float m_lifetime;

    private float m_timer;

    protected float m_speed;

    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    protected BoxCollider2D m_boxcollider2D;

    public GameObject m_MuzzleFlash;

    protected bool m_alive = true;

    protected float m_Damage;

    protected void Awake()
    {

        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
        Initialize();
    }

    void Initialize()
    {

        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        m_rigidbody2D.isKinematic = false;

        m_boxcollider2D.size = m_spriterenderer.bounds.size;

        m_timer = 0;

        m_spriterenderer.sortingLayerName = "Bullet";
        gameObject.tag = "Bullet";

        gameObject.layer = 8;

    }


	// Use this for initialization
	protected void Start ()
    {
        m_RigidBody2D.velocity = Vector3.up * m_speed;
        PlayMuzzleFlash();
	}
	
	// Update is called once per frame
	protected void Update ()
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



    protected void CheckBoundry()
    {
        Vector2 t_screen = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint;


        if(transform.position.y - m_spriterenderer.bounds.size.y/2 > t_screen.y ||
            transform.position.y + m_spriterenderer.bounds.size.y/2 < -t_screen.y)
            { 
                m_alive = false;
            }
    }

    void PlayMuzzleFlash()
    {

    }

    protected void OnCollisionEnter2D(Collision2D p_Collision)
    {
        Destroy(gameObject);
    }
}
