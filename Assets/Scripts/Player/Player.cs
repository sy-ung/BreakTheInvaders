using UnityEngine;


public class Player : MonoBehaviour {
    public enum ControlMode
    {
        DRAG,
        BATTING,
        SLINGSHOT
    };

    private ControlMode m_currentcontrolmode;
    public ControlMode m_CurrentControlMode
    {
        get { return m_currentcontrolmode; }
        set { m_currentcontrolmode = value; }
    }

    private float m_health;
    public float m_Health
    {
        get { return m_health; }
    }

    private SpriteRenderer m_spriterenderer;
    public SpriteRenderer m_SpriteRenderer
    {
        get {return m_spriterenderer;}
    }
    public Vector2 m_PlayerSize
    {
        get { return m_spriterenderer.bounds.size; }
    }



    private Vector2 m_velocity;
    public Vector2 m_Velocity
    {
        get {return m_velocity;}
        private set { m_velocity = value;}
    }



    private Vector2 m_newposition;
    public Vector2 m_NewPosition
    {
        set { m_newposition = value; }
    }

    private Quaternion m_newrotation;

    private BoxCollider2D m_boxcollider2D;

    private Rigidbody2D m_rigidbody2D;
    private Vector2 m_originalscale;

    //Offsets for boundry check against player image
    private Vector2 m_playersizecheck;
    private Vector2 m_boundrysize;

    private float m_startingline;

    private Vector2 m_previousmousepos;

    private Muzzle m_muzzle;
    public Muzzle m_Muzzle
    {
        get { return m_muzzle; }
    }

    private HealthBar m_healthbox;
    private float m_healthdisplaytime;
    private float m_healthdisplaytimer;
    private bool m_showhealth;

    private bool m_alive;
    public bool m_Alive
    {
        get { return m_alive; }
    }

    private void Initialize()
    {


        m_rigidbody2D.gravityScale = 0;
        m_spriterenderer.color = new Color(0, 100, 25);

        m_rigidbody2D.mass = 1;
        m_rigidbody2D.freezeRotation = true;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        m_rigidbody2D.isKinematic = true;

        //This is to calculate the scale of the player paddle in propotion to the screen size
        float t_AspectRatio = (float)Camera.main.pixelHeight / (float)Camera.main.pixelWidth;
        m_originalscale = (transform.localScale * t_AspectRatio) / 4f;
        transform.localScale = m_originalscale;


        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_playersizecheck = new Vector2(m_spriterenderer.bounds.size.x / 2, m_spriterenderer.bounds.size.y / 2);

        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);

        m_startingline = -Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y + (m_playersizecheck.y * 25);

        transform.position = new Vector2(0, m_startingline);

        m_newposition = transform.position;

        gameObject.layer = 14;

        m_health = 100.0f;

        m_healthdisplaytime = 2.0f;

        m_showhealth = true;

        m_alive = true;
    }
    
    void Awake()
    {
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void ChangeSprite(Sprite p_NewSprite)
    {
        m_spriterenderer.sprite = p_NewSprite;
        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);
    }

	// Use this for initialization
	void Start ()
    {
        Initialize();

        m_spriterenderer.sortingLayerName = "Player";

        m_newposition = transform.position;
        m_currentcontrolmode = ControlMode.DRAG;
        
        SetPlayerScale(1);


        SpawnBarrel(AssetManager.m_Instance.GetPrefab("GreenMuzzle"));

        m_healthbox = Instantiate(AssetManager.m_Instance.GetPrefab("HealthBar")).GetComponentInChildren<HealthBar>();
        m_healthbox.HideHealth();



        //t_mf.transform.parent = transform;
    }

    public void SpawnBarrel(GameObject p_NewMuzzle)
    {
        if (p_NewMuzzle != m_muzzle)
        { 
            if(m_muzzle !=null)
            {
                m_muzzle.DestroyMuzzle();
            }
            m_muzzle = Instantiate(p_NewMuzzle).GetComponent<Muzzle>();
            m_muzzle.transform.SetParent(transform);
        }
    }

    public void ChangePlayerSprite(Sprite p_NewPlayerSprite)
    {
        m_spriterenderer.sprite = p_NewPlayerSprite;
    }
    public void MovePlayer(Vector2 p_DeltaMovement)
    {
        m_newposition = (Vector2)transform.position + p_DeltaMovement;
    }
    public void SetPlayerPosition(Vector2 p_NewPlayerPosition)
    {
        m_newposition = p_NewPlayerPosition;
    }
    public void SetPlayerScale(float p_ScaleMultiplier)
    {
        transform.localScale *= p_ScaleMultiplier;
        m_playersizecheck = m_spriterenderer.bounds.size / 2;
    }

    

	// Update is called once per frame
	void Update ()
    {
        if(m_alive)
        { 
            MoveCheck();
            ScreenBoundryCheck();
            HealthCheck();
        }

        switch (m_currentcontrolmode)
        {
            case ControlMode.BATTING:
                BattingMode();
                break;
            case ControlMode.DRAG:
                break;
                DragMode();
                break;
            default:
                break;
        }
        m_rigidbody2D.velocity = Vector2.zero;
	}

    void HealthCheck()
    {
        if(m_health <= 0)
        {
            Death();
            return;
        }

        if (m_health <= 50)
        { 
            m_healthbox.ShowHealth();
            m_showhealth = true;
        }
        else
        { 
            if (m_showhealth)
            { 
                if(m_healthdisplaytimer < m_healthdisplaytime)
                {
                    m_healthbox.ShowHealth();
                    m_healthdisplaytimer += Time.deltaTime;
                }
                else
                {
                    m_healthbox.HideHealth();
                    m_showhealth = false;
                }
            }
        }
    }

    private void MoveCheck()
    {
        if(m_newposition != (Vector2)transform.position)
        {
            Vector2 t_PreviousPosition = transform.position;
            transform.position = Vector2.Lerp(transform.position, m_newposition, 0.2f);
            m_Velocity = ((Vector2)transform.position - t_PreviousPosition) / Time.deltaTime;
        }
        else
        {
            transform.position = m_newposition;
        }


        if (transform.position.y != m_startingline)
        {
            m_newposition = new Vector2(m_newposition.x, m_startingline);
        }

        return;

        if(m_newrotation != transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, m_newrotation, 0.25f);
        }


    }

    private void ScreenBoundryCheck()
    {
        if(transform.position.y - m_playersizecheck.y < -m_boundrysize.y)
            transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_playersizecheck.y);

        else if (transform.position.y + m_playersizecheck.y > m_boundrysize.y)
            transform.position = new Vector2(transform.position.x, m_boundrysize.y - m_playersizecheck.y);
        

        if (transform.position.x - m_playersizecheck.x < -m_boundrysize.x)
            transform.position = new Vector2(-m_boundrysize.x + m_playersizecheck.x, transform.position.y);

        else if (transform.position.x + m_playersizecheck.x > m_boundrysize.x)
            transform.position = new Vector2(m_boundrysize.x - m_playersizecheck.x, transform.position.y);
     }

    public float GetTopYLine()
    {
        return transform.position.y + m_playersizecheck.y;
    }

    private bool m_swinging = false;
    private float t_swingtimer = 0.20f;
    private float t_resettimer = 0.0f;
    void BattingMode()
    {
        Vector2 t_mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 t_direction = (t_mousepos - (Vector2)transform.position).normalized;
        Vector2 t_distance = t_direction * m_playersizecheck.magnitude * 0.5f;
        if (!m_swinging)
        { 
            //Keeping the paddle close to the mouse cursor
            m_newposition = t_mousepos - t_distance * 2;

            //Keeping the right of the paddle pointing towards the cursor
            m_newrotation = Quaternion.FromToRotation(Vector3.right, t_direction);
        }
        else
        {
            t_resettimer += Time.deltaTime;
            if(t_resettimer > t_swingtimer)
            {
                t_resettimer = 0.0f;
                m_swinging = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_swinging = true;
            transform.position = t_mousepos + t_distance * 2;
            transform.rotation = Quaternion.FromToRotation(-Vector3.right, t_direction);
            m_newrotation = transform.rotation;
            m_newposition = transform.position;
        }
        


    }

    //Was for testing
    void DragMode()
    {
        Vector2 t_mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        t_mousepos = new Vector2(t_mousepos.x, m_startingline);
        m_newposition = t_mousepos;
        m_newrotation = Quaternion.identity;

        if (Input.touchCount > 0)
        { 
            Vector2 t_touchloc = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            t_touchloc = new Vector2(t_touchloc.x, m_startingline);
            m_newposition = t_touchloc;
            m_newrotation = Quaternion.identity;
        }
    }

    public void SetMuzzleToFire(bool p_value)
    {
        m_muzzle.m_StartFiring = p_value;
    }

    public void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if (!m_alive)
            return;
    }

    public void TakeDamage(float p_Damage)
    {
        m_health -= p_Damage;
        m_healthdisplaytimer = 0;
        m_showhealth = true;
    }

    void Death()
    {
        m_alive = false;
        (Instantiate(AssetManager.m_Instance.GetPrefab("PlayerDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = m_spriterenderer.color;
        m_muzzle.DestroyMuzzle();
        BallManager.m_Instance.m_PlayerBall.Death();

        m_healthbox.Death();

        GameObject.FindGameObjectWithTag("pointblank").GetComponent<Game>().m_GameOver = true;

        Destroy(gameObject);
    }

}
