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

    public bool m_SoleSurvivor = false;

    protected void Awake()
    {
        Initialize();
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.isKinematic = true;
        m_rigidbody2D.freezeRotation = true;
        m_boundries = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_spriterenderer.sortingOrder = 2;

        //SetSprite(AssetManager.m_Instance.GetSprite(m_spritename));
    }

    private void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
    }

    public void SetSprite(string p_NewSpriteName)
    {
        //When setting a new sprite, change the collision box size also
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite(p_NewSpriteName);
        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);
        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float t_scaleFactor = (m_spriterenderer.bounds.size.x / t_screensize.x) * 0.5f;
        SetScale(t_scaleFactor);
        //transform.localScale = transform.localScale * t_scaleFactor;
    }

    public void SetScale(float p_ScaleFactor)
    {
        transform.localScale = transform.localScale * p_ScaleFactor;
    }

	//Check to see if only one enemy has spawned. Make it super fast if so.
	protected void Start()
    {
        if (m_SoleSurvivor)
        {
            BecomeSoleSurvivor();
        }
        else
            m_movementtimer = m_origmovementtimer;

        m_alive = true;

    }

    public void BecomeSoleSurvivor()
    {
        m_movementtimer = 0;
        m_movementspeed *= 2;
    }

    protected void Update()
    {
        MoveEnemyCheck();
    }

    protected void LateUpdate()
    {
        if (m_stayinbounds && !m_movedown)
            CheckBoundry();
    }

    public void MoveEnemyCheck()
    {
        if(!m_move)
        { 
            if (m_timer > m_movementtimer)
            {
                if (!m_movedown)
                    m_move = true;
                else
                    m_move = false;

                m_timer = 0;
                MoveEnemy();
            }
            else
            {
                m_timer += Time.deltaTime;
            }
        }
    }

    public void MoveEnemy()
    {
        if(m_move)
        { 
            if (m_changedirection)
                transform.position = new Vector2(transform.position.x - m_HalfSize.x * m_movementspeed.x, transform.position.y);
            else if (!m_changedirection)
                transform.position = new Vector2(transform.position.x + m_HalfSize.x * m_movementspeed.x, transform.position.y);

            m_move = false;
        }
        else if(m_movedown)
        {
            MoveDown();
        }
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

        if(transform.position.y - m_HalfSize.y < Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.y)
        {
            m_inboundsY = true;
        }
        else
        {
            m_inboundsY = false;
        }



        if (!m_changedirection)
        {
            if (transform.position.x + m_HalfSize.x > m_boundries.x - (m_HalfSize.x))
            {
                m_CurrentEnemySquad.MoveSquadDown();

            }

        }
        else if (m_changedirection)
        {
            if (transform.position.x - m_HalfSize.x< -m_boundries.x + (m_HalfSize.x))
            {
                m_CurrentEnemySquad.MoveSquadDown();
            }
        }
    }

    public void MoveDown()
    {
        ChangeDirection();
        transform.position += new Vector3(0, -(m_HalfSize.y * m_movementspeed.y), 0);
        m_movedown = false;
        if (transform.position.y < PlayerManager.m_Instance.m_Player.GetTopYLine() + m_HalfSize.y * 2)
        {
            m_CurrentEnemySquad.KillAllInSquad();
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
        if(p_Collision.collider.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(m_boxcollider2D, p_Collision.collider);
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
                Instantiate(m_deathparticle, transform.position, Quaternion.identity);

                m_alive = false;

                m_CurrentEnemySquad.RemoveEnemyFromSquad(this);

                if(!m_CurrentEnemySquad.m_IsSquadDead)
                    m_CurrentEnemySquad.IncreaseSquadSpeed();

                GameObject.FindGameObjectWithTag("pointblank").GetComponent<Game>().AddPoints(m_points);
                Destroy(gameObject);
            }
        }
    }
}
