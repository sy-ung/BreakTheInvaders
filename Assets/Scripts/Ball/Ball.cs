using UnityEngine;

public class Ball : MonoBehaviour {

    //Components created in Code
    private AudioSource m_ballaudiosystem;
    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriterenderer;
    private Vector2 m_originalscale;
    private CircleCollider2D m_circlecollider2D;

    //Offsets for boundry check against ball image
    private Vector2 m_ballsizecheck;
    private Vector2 m_boundrysize;

    private float m_defaultspeed;

    private Vector2 m_direction;

    private Vector2 m_storedcurrentposition;

    bool m_piercing;

    private float m_firingrate = 0.1f;
    private float m_firingtimer = 0;

    private Player m_player;

    private GameObject m_currentbullet;
    public GameObject m_CurrentBullet
    {
        set { m_currentbullet = value; }
    }

    private void Initialize()
    {
        m_ballaudiosystem = gameObject.AddComponent<AudioSource>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_circlecollider2D = gameObject.AddComponent<CircleCollider2D>();
    }

	// Use this for initialization
	void Awake ()
    {
        Initialize();

        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("PlayerBall");
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.mass = 1;
        m_rigidbody2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // This is to make sure that the scale of the ball is compliant with the size of the paddle, on start of the game.
        Vector2 t_PlayerSize = PlayerManager.m_Instance.m_Player.m_PlayerSize;
        float t_ScaleFactor = t_PlayerSize.y/ m_spriterenderer.bounds.size.y;
        m_originalscale = transform.localScale * (t_ScaleFactor * 1.5f);
        transform.localScale = m_originalscale;

        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        m_ballsizecheck = new Vector2(m_spriterenderer.bounds.size.x / 2, m_spriterenderer.bounds.size.y / 2);

        m_circlecollider2D.radius = m_spriterenderer.sprite.bounds.size.x / 2;

        m_piercing = false;

        m_firingtimer = m_firingrate;

        SetCurrentBullet("BallBulletDefault");

        m_player = PlayerManager.m_Instance.m_Player;
    }

    void Start()
    {
        OnRespawn();
        
    }
    public void OnRespawn()
    {
        m_defaultspeed = 5f;
        m_rigidbody2D.velocity = new Vector2(0, -m_defaultspeed);
    }

    public void ChangeBallSprite(Sprite p_NewBallSprite)
    {
        m_spriterenderer.sprite = p_NewBallSprite;
    }

    public void ScaleBall(float p_ScaleMultiplier)
    {
        transform.localScale = transform.localScale * p_ScaleMultiplier;

        //Recalculating offset for boundry check
        m_ballsizecheck.y = m_spriterenderer.bounds.size.y / 2;
        m_ballsizecheck.x = m_spriterenderer.bounds.size.x / 2;
    }

    private void ScreenBoundryCheck()
    {
        
        if(transform.position.y + m_ballsizecheck.y > m_boundrysize.y) //Top
        {
            transform.position = new Vector2(transform.position.x, m_boundrysize.y - m_ballsizecheck.y);
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y);
        }
        else if(transform.position.y - m_ballsizecheck.y < -m_boundrysize.y) //Bottom
        {
            transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_ballsizecheck.y);
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y);
        }



        if (transform.position.x + m_ballsizecheck.x > m_boundrysize.x)//Right
        {
            transform.position = new Vector2(m_boundrysize.x - m_ballsizecheck.x, transform.position.y);
            m_rigidbody2D.velocity = new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y);
        }
        else if(transform.position.x - m_ballsizecheck.x < -m_boundrysize.x)//Left
        {
            transform.position = new Vector2(-m_boundrysize.x + m_ballsizecheck.x, transform.position.y);
            m_rigidbody2D.velocity = new Vector2(-m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y);
        }
        
    }

    public void SetCurrentBullet(string p_BulletPrefabName)
    {
        m_currentbullet = AssetManager.m_Instance.GetPrefab(p_BulletPrefabName);
    }



	// Update is called once per frame
	void Update ()
    {
        ScreenBoundryCheck();
        SpeedCheck();
        
        if(m_firingtimer>m_firingrate)
        {

            //if(Input.GetMouseButton(0))
            if(Input.touchCount > 0)
            {
                for(int i = 0; i<Input.touchCount;i++)
                {
                    Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    if (t_touchpos.y > m_player.GetTopYLine())
                    { 
                        FireBullet(t_touchpos);
                        m_firingtimer = 0;
                    }
                }
            }
        }
        else
        {
            m_firingtimer += Time.deltaTime;
        }
        m_storedcurrentposition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {

        //Check to see if the ball is able to pierce through enemies
        //m_piercing = true;
        transform.position = m_storedcurrentposition;
        //transform.position = p_Collision.contacts[0].point - t_direction * m_ballsizecheck.magnitude;

        

        if (!m_piercing || p_Collision.collider.tag == "Player")
        { 
            Vector2 t_direction = ((Vector2)transform.position - p_Collision.contacts[0].point).normalized;
            //transform.position = p_Collision.contacts[0].point + t_direction * m_ballsizecheck.magnitude;
            m_rigidbody2D.velocity = t_direction * m_defaultspeed;
        }
        else
        {
           m_rigidbody2D.velocity = m_direction * m_defaultspeed;
        }



        //What to do when colliding with player
        if (p_Collision.collider.tag == "Player")
        {
            Player t_player = PlayerManager.m_Instance.m_Player;
            if (p_Collision.contacts[0].point.y < t_player.GetTopYLine() &&
                p_Collision.contacts[0].point.y > t_player.transform.position.y)
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x + t_player.m_Velocity.x, -m_rigidbody2D.velocity.y);
            }
            else if (p_Collision.contacts[0].point.y > t_player.GetTopYLine())
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x + t_player.m_Velocity.x, m_rigidbody2D.velocity.y);
            }
            //SpeedCheck();
        }

        //What to when colliding with enemy
        if(p_Collision.collider.tag == "Enemy")
        {
            Destroy(p_Collision.collider.gameObject);
        }

        //Dont move on collision

    }
    
    //So it never slows down past a certain speed
    void SpeedCheck()
    {
        m_direction = m_rigidbody2D.velocity.normalized;

        
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

        //if (m_rigidbody2D.velocity.magnitude > m_defaultspeed || m_rigidbody2D.velocity.magnitude < m_defaultspeed)
        //{
            
        //    m_rigidbody2D.velocity = m_direction * m_defaultspeed;
        //}

        
        
    }

    void FireBullet(Vector2 p_TargetLocation)
    {
        Vector2 t_firedirection = (p_TargetLocation - (Vector2)transform.position).normalized;

        //BallBullet t_bullet = (new GameObject("BallBullet").AddComponent<BallBullet>());

        Bullet t_bullet = Instantiate(m_currentbullet).GetComponent<Bullet>();

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (t_bullet.GetComponent<SpriteRenderer>().bounds.size.magnitude/2));
        t_bullet.transform.position = t_startPosition;

        if(m_rigidbody2D.velocity.magnitude == 0)
            t_bullet.m_RigidBody2D.velocity = t_firedirection * 10 * 1.5f;
        else
            t_bullet.m_RigidBody2D.velocity = t_firedirection * m_defaultspeed * 1.5f;

        t_bullet.transform.rotation = Quaternion.FromToRotation(t_bullet.transform.right, t_firedirection);
        t_bullet.tag = "Bullet";


        Physics2D.IgnoreCollision(m_circlecollider2D, t_bullet.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(t_bullet.GetComponent<BoxCollider2D>(), PlayerManager.m_Instance.m_Player.GetComponent<BoxCollider2D>());

    }
}
