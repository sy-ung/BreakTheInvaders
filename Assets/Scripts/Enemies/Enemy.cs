using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    protected SpriteRenderer m_spriterenderer;

    public Vector2 m_movementspeed;

    protected bool m_changedirection = false;

    public Vector2 m_HalfSize
    {
        get 
        {
            return m_spriterenderer.bounds.size/2;
        }
    }

    private Rigidbody2D m_rigidbody2D;
    private BoxCollider2D m_boxcollider2D;

    protected float m_origmovementtimer;
    public float m_OriginalMovementTimer
    {
        get { return m_origmovementtimer; }
    }

    public float m_movementtimer;
    public float m_timer = 0;

    private Vector2 m_boundries;

    public bool m_movedown = false;
    public bool m_move = false;

    protected GameObject m_deathparticle;

    protected int m_points;

    protected bool m_stayinbounds;

    private bool m_inboundsX;
    public bool m_InBoundsX
    {
        get { return m_inboundsX; }
    }

    private bool m_inboundsY;
    public bool m_InBoundsY
    {
        get { return m_inboundsY; }
    }

    public EnemySquad m_CurrentEnemySquad;

    private bool m_alive;
    public bool m_Alive
    {
        get { return m_alive; }
    }

    protected float m_firingrate;
    private float m_firingtimer;

    protected GameObject m_currentbullet;

    public bool m_SoleSurvivor = false;

    private bool m_cleartofire;
    public bool m_ClearToFire
    {
        get { return m_cleartofire; }
    }

    public Sprite m_IdleFrame;
    public Sprite m_MoveFrame;
    bool m_PlayFirstFrame;

    protected Color m_currentcolor;

    private float m_raylength;

    protected void Awake()
    {
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();

        //SetSprite(AssetManager.m_Instance.GetSprite(m_spritename));
    }

    private void Initialize()
    {
        m_spriterenderer.color = m_currentcolor;
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.isKinematic = true;
        m_rigidbody2D.freezeRotation = true;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;

        m_boundries = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_spriterenderer.sortingOrder = 2;
        m_raylength = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.y * 2;
        m_PlayFirstFrame = true;

        gameObject.layer = 9;
        gameObject.tag = "Enemy";

        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);

        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float t_scaleFactor = (m_spriterenderer.bounds.size.x / t_screensize.x) * 1.5f;
        SetScale(t_scaleFactor);

        m_firingrate = Random.Range(5.0f, 15f);
    }

    public void SetScale(float p_ScaleFactor)
    {
        transform.localScale = transform.localScale * p_ScaleFactor;
    }

	//Check to see if only one enemy has spawned. Make it super fast if so.
	protected void Start()
    {
        Initialize();

        if (m_SoleSurvivor)
        {
            BecomeSoleSurvivor();
        }
        else
            m_movementtimer = m_origmovementtimer;



        m_alive = true;


        CheckBoundry();

        if(!m_inboundsX || !m_inboundsY)
        {
            m_movementtimer = 0;
        }
        
    }

    public void BecomeSoleSurvivor()
    {
        m_movementtimer = 0;
        m_movementspeed *= 1.35f;
        m_SoleSurvivor = true;
    }



    protected void Update()
    {
        MoveEnemyCheck();

        if(m_currentbullet != null)
        { 
            FireBulletCheck();
            FireBullet();
        }
    }

    protected void LateUpdate()
    {
        if (!m_movedown)
            CheckBoundry();
    }

    public void MoveEnemyCheck()
    {
        if (m_timer > m_movementtimer)
        {
            m_timer = 0;
            m_move = true;
            MoveEnemy();
        }
        else
        {
            m_timer += Time.deltaTime;
        }
        
    }

    public void ResetMovement()
    {
        m_movementtimer = m_origmovementtimer;
    }

    public void MoveEnemy()
    {

        if (m_movedown)
        {
            MoveDown();
        }
        else if (m_move)
        { 
            if (m_changedirection)
                transform.position = new Vector2(transform.position.x - m_HalfSize.x * m_movementspeed.x, transform.position.y);
            else if (!m_changedirection)
                transform.position = new Vector2(transform.position.x + m_HalfSize.x * m_movementspeed.x, transform.position.y);

            m_move = false;
        }
        PlayFrame();
    }


    public void CheckBoundry()
    {
        if (transform.position.x + m_HalfSize.x > -Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x &&
            transform.position.x - m_HalfSize.x < Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x)
        {
            m_inboundsX = true;
        }
        else
        {
            m_inboundsX = false;
        }

        if(transform.position.y < Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.y)
        {
            m_inboundsY = true;
        }
        else
        {
            m_inboundsY = false;
        }

        if (m_stayinbounds)
        {
            if (!m_changedirection)
            {
                if (transform.position.x + m_HalfSize.x > m_boundries.x - (m_HalfSize.x))
                {
                    m_CurrentEnemySquad.MoveSquadDown(true);

                }
            }
            else if (m_changedirection)
            {
                if (transform.position.x - m_HalfSize.x < -m_boundries.x + (m_HalfSize.x))
                {

                    m_CurrentEnemySquad.MoveSquadDown(true);
                }
            }
        }
        
    }

    public void MoveDown()
    {
        ChangeDirection();

        transform.position += new Vector3(0, -(m_HalfSize.y * m_movementspeed.y), 0);
        m_movedown = false;

        if(PlayerManager.m_Instance.m_Player != null)
        { 
            if (transform.position.y < PlayerManager.m_Instance.m_Player.GetTopYLine() + m_HalfSize.y * 2)
            {
                m_CurrentEnemySquad.KillAllInSquad();
            }
        }
    }
    public void ChangeDirection()
    {
        if (m_changedirection)
            m_changedirection = false;
        else if (!m_changedirection)
            m_changedirection = true;
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {


    }

    public void Death()
    {
        //Check to see if enemy is in view
        Vector2 t_screen = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint;
        if(transform.position.y - m_spriterenderer.bounds.size.y /2 < t_screen.y)
        { 
            if(transform.position.x + m_spriterenderer.bounds.size.x / 2 > -t_screen.x &&
                transform.position.x - m_spriterenderer.bounds.size.x / 2 < t_screen.x) 
                { 
                    GameObject t_explosion = Instantiate(m_deathparticle, transform.position, Quaternion.identity) as GameObject;
                    t_explosion.GetComponent<ParticleSystem>().startColor = m_spriterenderer.color;

                    m_alive = false;
                    
                if(m_CurrentEnemySquad!=null)
                { 
                    m_CurrentEnemySquad.RemoveEnemyFromSquad(this);

                    if(!m_CurrentEnemySquad.m_IsSquadDead)
                        m_CurrentEnemySquad.IncreaseSquadSpeed();
                }

                GameObject.FindGameObjectWithTag("pointblank").GetComponent<Game>().AddPoints(m_points);
                    Destroy(gameObject);
                }
        }
    }

    void FireBulletCheck()
    {
        if (m_firingtimer > m_firingrate)
        {
            //FireBullet();
            FireBulletRayCastCheck();

        }
        else
            m_firingtimer += Time.deltaTime;
    }

    void FireBulletRayCastCheck()
    {
        
        Vector2 t_origin = new Vector2(transform.position.x, transform.position.y - (m_spriterenderer.bounds.size.y/1.99f));
        Vector2 t_direction = -Vector2.up;

        RaycastHit2D t_rayhit = Physics2D.Raycast(t_origin, t_direction, m_raylength);

        if (t_rayhit.collider != null)
        {
            if (t_rayhit.collider.tag == "Enemy")
            {
                if (t_rayhit.collider.gameObject.GetComponent<Enemy>().m_CurrentEnemySquad == m_CurrentEnemySquad)
                {
                    m_cleartofire = false;
                }
                else
                    m_cleartofire = true;
            }
            else
            { 
                m_cleartofire = true;
            }
        }
        else
            m_cleartofire = true;

    }

    public void FireBullet()
    {
        if(m_cleartofire)
        { 
            Vector2 t_firedirection = -Vector3.up;
            Bullet t_bullet = Instantiate(m_currentbullet).GetComponent<Bullet>();
            t_bullet.transform.position = new Vector2(transform.position.x, transform.position.y - m_spriterenderer.bounds.size.y/2 - t_bullet.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            t_bullet.tag = "Bullet";

            m_firingtimer = 0;
            m_cleartofire = false;
        }
    }

    void PlayFrame()
    {
        if (m_PlayFirstFrame)
        { 
            m_spriterenderer.sprite = m_IdleFrame;
            m_PlayFirstFrame = false;
        }
        else
        {
            m_spriterenderer.sprite = m_MoveFrame;
            m_PlayFirstFrame = true;
        }
    }

    public void SetColor(Color p_NewColor)
    {
        m_currentcolor = p_NewColor;
    }
}
