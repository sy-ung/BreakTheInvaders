using UnityEngine;

public class Ball : MonoBehaviour {

    //Components created in Code
    private AudioSource m_ballaudiosystem;

    private Rigidbody2D m_rigidbody2D;
    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    private SpriteRenderer m_spriterenderer;
    private Vector2 m_originalscale;
    private CircleCollider2D m_circlecollider2D;

    //Offsets for boundry check against ball image
    private float m_radius;
    private Vector2 m_boundrysize;

    private float m_defaultspeed;

    public Vector2 m_Direction;

    public Vector2 m_StoredCurrentPosition;

    protected bool m_piercing;

    private Vector2 m_currentvelocity;

    private Player m_player;

    private GameObject m_balldeathparticle;

    protected bool m_dospeedcheck;

    private bool m_alive;

    private void Initialize()
    {
        //m_ballaudiosystem = gameObject.AddComponent<AudioSource>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_circlecollider2D = gameObject.GetComponent<CircleCollider2D>();
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.mass = 1;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // This is to make sure that the scale of the ball is compliant with the size of the paddle, on start of the game.
        Vector2 t_PlayerSize = PlayerManager.m_Instance.m_Player.m_PlayerSize;
        float t_ScaleFactor = t_PlayerSize.y / m_spriterenderer.bounds.size.y;
        m_originalscale = transform.localScale;

        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_radius = m_spriterenderer.bounds.size.x / 2;

        m_circlecollider2D.radius = m_spriterenderer.sprite.bounds.size.x / 2;

        m_piercing = false;

        m_player = PlayerManager.m_Instance.m_Player;

        gameObject.layer = 12;

        m_alive = true;
    }

	// Use this for initialization
	protected void Awake ()
    {
        Initialize();
    }

    protected void Start()
    {
        ScaleBall(0.75f);
        m_defaultspeed = 10;
        gameObject.tag = "PlayerBall";
        m_spriterenderer.sortingLayerName = "Ball";
        m_dospeedcheck = true;
    }

    public void LaunchBall()
    {
        m_Direction = Vector3.up;
    }

    public void ChangeBallSprite(Sprite p_NewBallSprite)
    {
        m_spriterenderer.sprite = p_NewBallSprite;
    }

    public void ScaleBall(float p_ScaleMultiplier)
    {
        transform.localScale = m_originalscale * p_ScaleMultiplier;
        m_radius = m_spriterenderer.bounds.size.x / 2;
    }

    private void ScreenBoundryCheck()
    {
        
        if(transform.position.y + m_radius > m_boundrysize.y) //Top
        {
            transform.position = new Vector2(transform.position.x, m_boundrysize.y - m_radius);

            m_Direction = new Vector2(m_Direction.x, -m_Direction.y);
            //m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y);
        }
        else if(transform.position.y - m_radius < -m_boundrysize.y) //Bottom
        {
            transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_radius);
            m_Direction = new Vector2(m_Direction.x, -m_Direction.y);
            //m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y);
        }



        if (transform.position.x + m_radius > m_boundrysize.x)//Right
        {
            transform.position = new Vector2(m_boundrysize.x - m_radius, transform.position.y);
            m_Direction = new Vector2(-m_Direction.x, m_Direction.y);
            //m_rigidbody2D.velocity = new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y);
        }
        else if(transform.position.x - m_radius < -m_boundrysize.x)//Left
        {
            transform.position = new Vector2(-m_boundrysize.x + m_radius, transform.position.y);
            m_Direction = new Vector2(-m_Direction.x, m_Direction.y);
            //m_rigidbody2D.velocity = new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y);
        }


    }
    // Update is called once per frame
    protected void Update()
    {
        if (!m_alive)
            return;

        ScreenBoundryCheck();
        m_StoredCurrentPosition = transform.position;
    }

    protected void LateUpdate()
    {
        if (!m_alive)
            return;

        transform.position = m_StoredCurrentPosition;
        m_rigidbody2D.velocity = m_Direction * m_defaultspeed;

        if(m_dospeedcheck)
            SpeedCheck();
    }

    protected void OnCollisionEnter2D(Collision2D p_Collision)
    {

        if (!m_alive)
            return;

        if (p_Collision.collider.tag == "Player")
        {
            Player t_player = p_Collision.collider.gameObject.GetComponent<Player>();
            Vector2 t_playervelocity = t_player.m_Velocity;

            t_player.m_Muzzle.Reload();

            if(t_playervelocity == Vector2.zero)
            {
                if(transform.position.y > t_player.transform.position.y)
                {
                    m_Direction = Vector3.up;
                }
            }
            else
            { 
                m_Direction = (((Vector2)transform.position + t_playervelocity) - p_Collision.contacts[0].point).normalized;
            }
        }
        if (p_Collision.collider.tag == "Enemy")
        {

            transform.position = m_StoredCurrentPosition;
            if (m_piercing)
            {
                m_rigidbody2D.velocity = m_Direction * m_defaultspeed;
            }
            else
            { 

                m_Direction = ((Vector2)transform.position - p_Collision.contacts[0].point).normalized;
            }
            p_Collision.collider.gameObject.GetComponent<Enemy>().Death();
        }

        //if(p_Collision.collider.tag == "PowerUp")
        //{

        //    p_Collision.gameObject.GetComponent<PowerUp>().ApplyBallPowerUp();
        //    m_rigidbody2D.velocity = m_Direction * m_defaultspeed;
        //}

    }
    
    //So it never slows down passed a certain speed
    void SpeedCheck()
    {        
        if(m_rigidbody2D.velocity.y < m_defaultspeed/2 && m_rigidbody2D.velocity.y > -m_defaultspeed/2)
        {
            if(m_rigidbody2D.velocity.y > 0)
            { 
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, m_defaultspeed);
            }
            else
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_defaultspeed);
            }
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
