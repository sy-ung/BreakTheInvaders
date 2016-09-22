using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    protected SpriteRenderer m_spriterenderer;

    public Vector2 m_HalfSize
    {
        get 
        {
            return m_spriterenderer.bounds.size/2;
        }
    }

    private Rigidbody2D m_rigidbody2D;
    private BoxCollider2D m_boxcollider2D;



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

    public bool m_Alive;


    protected float m_firingrate;
    public float m_FiringRateFactor;
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

    protected string m_firesoundname;

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

        m_boxcollider2D.isTrigger = true;
        //float t_scaleFactor = (m_spriterenderer.bounds.size.x / t_screensize.x) * 1.5f;
        //SetScale(t_scaleFactor);

        m_firingrate = Random.Range(5.0f, 15f) / m_FiringRateFactor;
    }

    public void SetScale(float p_ScaleFactor)
    {
        transform.localScale = transform.localScale * p_ScaleFactor;
    }

	//Check to see if only one enemy has spawned. Make it super fast if so.
	protected void Start()
    {
        Initialize();
        m_Alive = true;
        CheckBoundry();
        
    }

    //public void BecomeSoleSurvivor()
    //{
    //    m_movementtimer = 0;
    //    m_movementspeed *= 1.35f;
    //    m_SoleSurvivor = true;
    //}



    protected void Update()
    {


        if (!m_Alive)
        {
            Death();
            return;
        }
        if (m_currentbullet != null)
        {
            if (m_firingtimer> m_firingrate)
            {
                m_firingtimer = 0;

                if (!m_cleartofire)
                    FireBulletRayCastCheck();

                if (m_cleartofire)
                    FireBullet();
            }
            else
            {
                m_firingtimer += Time.deltaTime;
            }
            
        }
        if (m_CurrentEnemySquad != null)
        { 
            if (m_CurrentEnemySquad.m_PlayFrame)
                PlayFrame();
        }
    }

    protected void LateUpdate()
    {
        if (!m_Alive)
            return;

        if(!m_CurrentEnemySquad.m_MoveSquadDown)
            CheckBoundry();
    }

    public void CheckBoundry()
    {
        if (transform.position.x > -Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x &&
            transform.position.x < Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x)
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
            if (!m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x > m_boundries.x - (m_HalfSize.x * 2))
                {
                    m_CurrentEnemySquad.m_MoveSquadDown = true;

                }
            }
            else if (m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x < -m_boundries.x + (m_HalfSize.x * 2))
                {
                    m_CurrentEnemySquad.m_MoveSquadDown = true;
                }
            }
        }
        else
        {
            if(!m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x > m_boundries.x + (m_HalfSize.x * 2))
                    Destroy(m_CurrentEnemySquad.gameObject);
            }
            else if (m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x < -m_boundries.x - (m_HalfSize.x * 2))
                    Destroy(m_CurrentEnemySquad.gameObject);
            }
        }
        
    }


    void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (p_Collider.tag == "Player")
        {
            p_Collider.GetComponent<Player>().TakeDamage(1000);
        }
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

                m_Alive = false;
                    
                if(m_CurrentEnemySquad!=null)
                { 
                    m_CurrentEnemySquad.RemoveEnemyFromSquad(this);
                }

                GameObject.FindGameObjectWithTag("pointblank").GetComponent<Game>().AddPoints(m_points);
                SpawnPowerUp();
                Destroy(gameObject);
            }
        }
    }

    void FireBulletRayCastCheck()
    {
        
        Vector2 t_origin = new Vector2(transform.position.x, transform.position.y - (m_spriterenderer.bounds.size.y/1.99f));
        Vector2 t_direction = -Vector2.up;

        //RaycastHit2D t_rayhit = Physics2D.Raycast(t_origin, t_direction, m_raylength);

        RaycastHit2D[] t_rayhits = Physics2D.RaycastAll(t_origin, t_direction, m_raylength);

        m_cleartofire = true;

        foreach (RaycastHit2D t_rayhit in t_rayhits)
        {
            if(t_rayhit.collider.tag == "Enemy")
            {
                if( t_rayhit.collider.GetComponent<Enemy>().m_CurrentEnemySquad == m_CurrentEnemySquad)
                {
                    m_cleartofire = false;
                    break;
                }
                else
                {
                    m_cleartofire = true;
                }
            }
        }

    }

    public void FireBullet()
    {
        if(m_cleartofire)
        { 
            Vector2 t_firedirection = -Vector3.up;
            Bullet t_bullet = Instantiate(m_currentbullet).GetComponent<Bullet>();
            t_bullet.transform.position = new Vector2(transform.position.x, transform.position.y - m_spriterenderer.bounds.size.y/2 - t_bullet.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            t_bullet.tag = "Bullet";
            PlayFireSound();
        }
    }

    public void PlayFrame()
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

    void PlayFireSound()
    {
        GameAudioManager.m_Instance.PlaySound(m_firesoundname, false, 1.0f, false);
    }

    public virtual void SpawnPowerUp()
    {
        if (Random.Range(0, 25) == 10)
        {
            int t_powerupchoice = Random.Range(1, 4);

            if (t_powerupchoice == 1)
            {
                Instantiate(AssetManager.m_Instance.GetPrefab("BluePower"), transform.position, Quaternion.identity);
            }

            if (t_powerupchoice == 2)
            {
                Instantiate(AssetManager.m_Instance.GetPrefab("GreenPower"), transform.position, Quaternion.identity);
            }
        }
    }

}
