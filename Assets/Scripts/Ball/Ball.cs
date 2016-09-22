using UnityEngine;

public class Ball : MonoBehaviour {


    private Rigidbody2D m_rigidbody2D;
    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    private SpriteRenderer m_spriterenderer;
    private Vector2 m_originalscale;
    protected CircleCollider2D m_circlecollider2D;

    //Offsets for boundry check against ball image
    private Vector2 m_boundrysize;

    protected float m_defaultspeed;


    private Vector2 m_currentvelocity;
    public Vector2 m_CurrentVelocity
    {
        get { return m_currentvelocity; }
    }

    private Player m_player;

    private GameObject m_balldeathparticle;

    private float m_maxspeed;

    private bool m_alive;

    private float m_angleoffset;

    private void Initialize()
    {
        //m_ballaudiosystem = gameObject.AddComponent<AudioSource>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_circlecollider2D = gameObject.GetComponent<CircleCollider2D>();

        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.mass = 1;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.None;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        m_rigidbody2D.freezeRotation = true;

        m_circlecollider2D.isTrigger = true;

        // This is to make sure that the scale of the ball is compliant with the size of the paddle, on start of the game.
        Vector2 t_PlayerSize = PlayerManager.m_Instance.m_Player.m_PlayerSize;
        float t_ScaleFactor = t_PlayerSize.y / m_spriterenderer.bounds.size.y;
        m_originalscale = transform.localScale;

        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        m_circlecollider2D.radius = m_spriterenderer.sprite.bounds.size.x / 2;

        m_player = PlayerManager.m_Instance.m_Player;

        gameObject.layer = 12;

        m_alive = true;

        m_angleoffset = 0.30f;

    }

	// Use this for initialization
	protected void Awake ()
    {
        Initialize();
    }

    void Reset()
    {

    }

    protected void Start()
    {
        ScaleBall(0.75f);
        gameObject.tag = "PlayerBall";
        m_spriterenderer.sortingLayerName = "Ball";
        m_defaultspeed = 5;
        m_maxspeed = m_defaultspeed * 1.5f;

    }

    public void StartLaunch()
    {
        Start();
        ChangeVelocity(new Vector2(0, -1) * m_defaultspeed);
    }

    public void ChangeBallSprite(Sprite p_NewBallSprite)
    {
        m_spriterenderer.sprite = p_NewBallSprite;
    }

    public void ScaleBall(float p_ScaleMultiplier)
    {
        transform.localScale = m_originalscale * p_ScaleMultiplier;
    }

    private void ScreenBoundryCheck()
    {
        
        if(transform.position.y > m_boundrysize.y - m_spriterenderer.bounds.size.y/2) //Top
        {
            transform.position = new Vector2(transform.position.x, m_boundrysize.y - m_spriterenderer.bounds.size.y / 2);

            ChangeVelocity(new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y));

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f,false);
            SpeedCheck();
        }
        else if(transform.position.y < -m_boundrysize.y + m_spriterenderer.bounds.size.y/2) //Bottom
        {
            transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_spriterenderer.bounds.size.y / 2);

            ChangeVelocity(new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y));
            
            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f,false);

            SpeedCheck();
        }


        if (transform.position.x > m_boundrysize.x - m_spriterenderer.bounds.size.x/2)//Right
        {
            transform.position = new Vector2(m_boundrysize.x - m_spriterenderer.bounds.size.x/2, transform.position.y);

            ChangeVelocity(new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y));

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f,false);
            SpeedCheck();
        }
        else if(transform.position.x < -m_boundrysize.x + m_spriterenderer.bounds.size.x/2)//Left
        {
            transform.position = new Vector2(-m_boundrysize.x + m_spriterenderer.bounds.size.x/2, transform.position.y);

            ChangeVelocity(new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y));

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f,false);
            SpeedCheck();
        }


    }
    // Update is called once per frame
    protected void Update()
    {

        if (!m_alive)
            return;
        ScreenBoundryCheck();
        m_currentvelocity = m_rigidbody2D.velocity;
    }

    protected void LateUpdate()
    {
        if (!m_alive)
            return;

    }

    protected void CheckCollision(Collider2D p_Object)
    {
        Vector2 t_objectbounds = p_Object.GetComponent<SpriteRenderer>().bounds.size / 2;
        Vector2 t_objectposition = p_Object.transform.position;

        Vector2 t_ballbounds = m_spriterenderer.bounds.size / 2;

        //Within length of object
        if (transform.position.x > t_objectposition.x - t_objectbounds.x && transform.position.x < t_objectposition.x + t_objectbounds.x)
        {
            if(transform.position.y > t_objectposition.y)
            {
                transform.position = new Vector2(transform.position.x, t_objectposition.y + t_objectbounds.y + m_spriterenderer.bounds.size.y/2);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, t_objectposition.y - t_objectbounds.y - m_spriterenderer.bounds.size.y/2);
            }
            ChangeVelocity(new Vector2(m_currentvelocity.x, -m_currentvelocity.y));

            if (p_Object.tag == "Player")
            {
                Player t_player = p_Object.GetComponent<Player>();
                ChangeVelocity((m_currentvelocity + t_player.m_Velocity).normalized * (m_currentvelocity.magnitude + t_player.m_Velocity.magnitude));
            }
        }
        else
        {
            if (transform.position.x > t_objectposition.x)
            {
                transform.position = new Vector2(t_objectposition.x + t_objectbounds.x + m_spriterenderer.bounds.size.x / 2, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(t_objectposition.x - t_objectbounds.x - m_spriterenderer.bounds.size.x / 2, transform.position.y);
            }

            if (p_Object.tag == "Player")
            {
                //Vector2 t_newheading = ((Vector2)transform.position - p_Object.contacts[0].point);

                Player t_player = p_Object.GetComponent<Player>();

                Vector2 t_newheading;

                //Get position of the sides of the player
                if(transform.position.x > t_objectposition.x)
                {
                    t_newheading = ((Vector2)transform.position - (t_objectposition + new Vector2(t_objectbounds.x, 0)));
                }
                else
                {
                    t_newheading = ((Vector2)transform.position - (t_objectposition - new Vector2(t_objectbounds.x, 0)));
                }

                //if the reflected vector is 90 degrees perpendicular to the player
                if (t_newheading.y == 0)
                {
                    if (transform.position.y <= t_objectposition.y)
                    {
                        t_newheading = new Vector2(t_newheading.x, -m_angleoffset).normalized;
                    }
                    else
                    {
                        t_newheading = new Vector2(t_newheading.x, m_angleoffset).normalized;
                    }
                }

                if (Mathf.Sign(m_currentvelocity.x) != Mathf.Sign(t_player.m_Velocity.x))
                    ChangeVelocity((m_currentvelocity + t_player.m_Velocity).normalized * (m_currentvelocity.magnitude + t_player.m_Velocity.magnitude));

                ChangeVelocity(t_newheading.normalized * m_currentvelocity.magnitude);
                
            }
            else
            {
                ChangeVelocity(new Vector2(-m_currentvelocity.x, m_currentvelocity.y));
            }
        }

        if (p_Object.tag == "Enemy")
        {
            p_Object.GetComponent<Enemy>().Death();
        }

        SpeedCheck();

    }

    public void OnTriggerStay2D(Collider2D p_Collider)
    {
        if(p_Collider.tag == "Player")
            CheckCollision(p_Collider);
    }

    public virtual void OnTriggerEnter2D(Collider2D p_Collider)
    {
        CheckCollision(p_Collider);
    }

    protected void OnTriggerExit2D(Collider2D p_Collider)
    {
        if(p_Collider.tag == "Player")
        {
            p_Collider.GetComponent<Player>().m_Muzzle.Reload();
            GameAudioManager.m_Instance.PlaySound("BallPlayerHit", false, 1.0f, false);
        }
    }


    //So it never slows down passed a certain speed
    void SpeedCheck()
    {

        if (Mathf.Abs(m_rigidbody2D.velocity.y) < m_defaultspeed)
        {
            if (m_rigidbody2D.velocity.y >= 0)
            {
                //m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, m_defaultspeed/2);
                ChangeVelocity(new Vector2(m_currentvelocity.x, m_defaultspeed));
            }
            else
            {
                //m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_defaultspeed/2);
                ChangeVelocity(new Vector2(m_currentvelocity.x, -m_defaultspeed));
            }
        }

        if (m_rigidbody2D.velocity.magnitude > m_maxspeed)
        {
            //m_rigidbody2D.velocity = m_rigidbody2D.velocity.normalized * m_maxspeed;
            ChangeVelocity(m_currentvelocity.normalized * m_maxspeed);
        }
        if (m_rigidbody2D.velocity.magnitude < m_defaultspeed)
        {
            //m_rigidbody2D.velocity = m_rigidbody2D.velocity.normalized * m_defaultspeed;
            ChangeVelocity(m_currentvelocity.normalized * m_defaultspeed);
        }
    }

    public void ChangeVelocity(Vector2 p_NewVelocity)
    {
        m_rigidbody2D.velocity = p_NewVelocity;
        m_currentvelocity = p_NewVelocity;
    }


    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void DestroyBall()
    {
        Destroy(gameObject);
    }

}
