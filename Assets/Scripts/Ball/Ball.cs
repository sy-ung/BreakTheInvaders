using UnityEngine;

public class Ball : MonoBehaviour {


    private Rigidbody2D m_rigidbody2D;
    public Rigidbody2D m_RigidBody2D
    {
        get { return m_rigidbody2D; }
    }

    private SpriteRenderer m_spriterenderer;
    private Vector2 m_originalscale;
    private CircleCollider2D m_circlecollider2D;

    //Offsets for boundry check against ball image
    private Vector2 m_boundrysize;

    private float m_defaultspeed;

    public Vector2 m_StoredCurrentPosition;

    protected bool m_piercing;

    private Vector2 m_currentvelocity;

    private Player m_player;

    private GameObject m_balldeathparticle;

    private float m_maxspeed;

    private bool m_alive;

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

        m_circlecollider2D.isTrigger = false;

        // This is to make sure that the scale of the ball is compliant with the size of the paddle, on start of the game.
        Vector2 t_PlayerSize = PlayerManager.m_Instance.m_Player.m_PlayerSize;
        float t_ScaleFactor = t_PlayerSize.y / m_spriterenderer.bounds.size.y;
        m_originalscale = transform.localScale;

        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        m_circlecollider2D.radius = m_spriterenderer.sprite.bounds.size.x / 2;

        m_piercing = false;

        m_player = PlayerManager.m_Instance.m_Player;

        gameObject.layer = 12;

        m_alive = true;

        m_maxspeed = 40;

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
        m_defaultspeed = 10;
        gameObject.tag = "PlayerBall";
        m_spriterenderer.sortingLayerName = "Ball";


        ChangeVelocity(new Vector2(1,-1) * m_defaultspeed);
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

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f);
            SpeedCheck();
        }
        else if(transform.position.y < -m_boundrysize.y + m_spriterenderer.bounds.size.y/2) //Bottom
        {
            transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_spriterenderer.bounds.size.y / 2);

            ChangeVelocity(new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y));
            
            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f);

            SpeedCheck();
        }


        if (transform.position.x > m_boundrysize.x - m_spriterenderer.bounds.size.x/2)//Right
        {
            transform.position = new Vector2(m_boundrysize.x - m_spriterenderer.bounds.size.x/2, transform.position.y);

            ChangeVelocity(new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y));

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f);
            SpeedCheck();
        }
        else if(transform.position.x < -m_boundrysize.x + m_spriterenderer.bounds.size.x/2)//Left
        {
            transform.position = new Vector2(-m_boundrysize.x + m_spriterenderer.bounds.size.x/2, transform.position.y);

            ChangeVelocity(new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y));

            GameAudioManager.m_Instance.PlaySound("BallWallHit", false, 1.0f);
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

    public void OnTriggerStay2D(Collider2D p_Collider)
    {
        if(p_Collider.tag == "Player")
        {
            CheckPlayerCollision(p_Collider.GetComponent<Player>());
        }
    }

    void CheckPlayerCollision(Player p_Player)
    {
        Vector2 t_playerpos = p_Player.transform.position;
        Vector2 t_playerbounds = p_Player.GetComponent<SpriteRenderer>().bounds.size / 2;

        Vector2 t_ballvelocity = m_rigidbody2D.velocity;
        Vector2 t_ballbounds = m_spriterenderer.bounds.size / 2;

       

        //Within length of player
        if (transform.position.x > t_playerpos.x - t_playerbounds.x && transform.position.x < t_playerpos.x + t_playerbounds.x)
        {
            //Above the player
            if (transform.position.y > t_playerpos.y)
            { 
                transform.position = new Vector2(transform.position.x, t_playerpos.y + t_playerbounds.y + t_ballbounds.y);
            }

            //Below the player
            else if(transform.position.y < t_playerpos.y)
            {
                transform.position = new Vector3(transform.position.x, t_playerpos.y - t_playerbounds.y - t_ballbounds.y);

            }
            ChangeVelocity(new Vector2(t_ballvelocity.x, - t_ballvelocity.y));
        }
        //Hitting the side of player
        else
        {
            //Left of player
            if(transform.position.x < t_playerpos.x)
            {
                transform.position = new Vector2(t_playerpos.x - t_playerbounds.x - t_ballbounds.x, transform.position.y);
            }
            //Right of player
            else if (transform.position.x > t_playerpos.x)
            {
                transform.position = new Vector2(t_playerpos.x + t_playerbounds.x + t_ballbounds.x, transform.position.y);
            }
            ChangeVelocity(new Vector2(-t_ballvelocity.x, t_ballvelocity.y));
        }
    }
    protected void OnCollisionEnter2D(Collision2D p_Collision)
    {


        if (p_Collision.collider.tag == "Player")
        {
            Player t_player = p_Collision.collider.gameObject.GetComponent<Player>();


            Debug.Log(m_currentvelocity);
            if (t_player.m_Velocity != Vector2.zero)
                ChangeVelocity(new Vector2(m_currentvelocity.x, -m_currentvelocity.y) + t_player.m_Velocity);
            else
                ChangeVelocity(new Vector2(m_currentvelocity.x, -m_currentvelocity.y));
        }
    }

    protected void OnCollisionStay2D(Collision2D p_Collision)
    {

        //if (!m_alive)
        //    return;



            //if (p_Collision.collider.tag == "Enemy")
            //{

            //    transform.position = m_StoredCurrentPosition;
            //    if (m_piercing)
            //    {
            //        //m_rigidbody2D.velocity = m_Direction * m_defaultspeed;
            //    }
            //    else
            //    { 
            //        //m_Direction = ((Vector2)transform.position - p_Collision.contacts[0].point).normalized;

            //    }
            //    p_Collision.collider.gameObject.GetComponent<Enemy>().Death();
            //}

            //if (p_Collision.collider.tag == "PowerUp")
            //{

            //}


            //if(p_Collision.collider.tag == "PowerUp")
            //{

            //    p_Collision.gameObject.GetComponent<PowerUp>().ApplyBallPowerUp();
            //    m_rigidbody2D.velocity = m_Direction * m_defaultspeed;
            //}

        }

    protected void OnCollisionExit2D(Collision2D p_Collision)
    {
        if(p_Collision.collider.gameObject.tag == "Player")
        {
            p_Collision.collider.gameObject.GetComponent<Player>().m_Muzzle.Reload();

            GameAudioManager.m_Instance.PlaySound("BallPlayerHit", false, 1.0f);
        }

        if(p_Collision.collider.gameObject.tag == "Enemy")
        {
            //SpeedCheck();
        }
    }


    //So it never slows down passed a certain speed
    void SpeedCheck()
    {

        float t_yspeed = Mathf.Abs(m_rigidbody2D.velocity.y);
        if(t_yspeed<m_defaultspeed/2)
        {
            if(m_RigidBody2D.velocity.y > 0)
            {
                //m_Direction = Quaternion.AngleAxis(15, transform.forward) * m_Direction;
            }
            else
            {
                //m_Direction = Quaternion.AngleAxis(-15, transform.forward) * m_Direction;
            }
        }

    }

    void ChangeVelocity(Vector2 p_NewVelocity)
    {
        m_rigidbody2D.velocity = p_NewVelocity;
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
