using UnityEngine;


public class Player : MonoBehaviour {


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


    private float m_startingline;

    private Vector2 m_previousmousepos;

    private Muzzle m_muzzle;
    public Muzzle m_Muzzle
    {
        get { return m_muzzle; }
    }

    private HealthBar m_healthbar;


    private bool m_muzzlepowerupactivated;
    private float m_muzzlepoweruptimer;
    private float m_muzzlepowerupduration;
    private GameObject m_defaultmuzzle;
    private bool m_muzzlepowerupoverwrite;
    public bool m_MuzzlePowerUpOverwritable
    {
        get { return m_muzzlepowerupoverwrite; }
    }

    public bool m_Alive;

    private Color m_defaultcolor;

    private Vector2 m_screensize;

    private void Initialize()
    {


        m_rigidbody2D.gravityScale = 0;
        m_spriterenderer.color = m_defaultcolor = new Color(0, 100, 25);
        

        m_rigidbody2D.mass = 1;
        m_rigidbody2D.freezeRotation = true;
        m_rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        m_rigidbody2D.isKinematic = true;



        m_boxcollider2D.size = new Vector2(m_spriterenderer.bounds.size.x, m_spriterenderer.bounds.size.y);
        m_boxcollider2D.isTrigger = true;


        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_startingline = -t_screensize.y + t_screensize.y / 1.65f;

        transform.position = new Vector2(0, m_startingline);

        m_newposition = transform.position;

        gameObject.layer = 14;


        m_Alive = true;

        m_muzzlepowerupduration = 7.0f;
        m_defaultmuzzle = AssetManager.m_Instance.GetPrefab("BasicMuzzle");

        m_muzzlepowerupoverwrite = true;

        m_spriterenderer.sortingLayerName = "Player";

        m_newposition = transform.position;

        m_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


    }
    
    void Awake()
    {
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
        Initialize();
    }

    void ChangeSprite(Sprite p_NewSprite)
    {
        m_spriterenderer.sprite = p_NewSprite;
        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);
    }

	// Use this for initialization
	void Start ()
    {

        transform.localScale = new Vector2(0.25f, 0.25f);
        SpawnBarrel(m_defaultmuzzle);
        SpawnHealthBox();
    }

    void SpawnHealthBox()
    {
        GameObject t_healthBox = Instantiate(AssetManager.m_Instance.GetPrefab("HealthBar"));
        m_healthbar = t_healthBox.GetComponentInChildren<HealthBar>();

        m_healthbar.ShowHealth();

        t_healthBox.transform.localScale = new Vector2(1, 1);
        t_healthBox.transform.position = new Vector2(0, transform.position.y +  t_healthBox.GetComponent<SpriteRenderer>().bounds.size.y * 0.25f);
        m_healthbar.SetTargetTransform(transform);

    }

    public void ApplyMuzzlePowerUp(GameObject p_NewMuzzle, bool p_Overwritable)
    {
        m_muzzlepowerupoverwrite = p_Overwritable;
        ApplyMuzzlePowerUp(p_NewMuzzle);
    }

    public void ApplyMuzzlePowerUp(GameObject p_NewMuzzle)
    {
        SpawnBarrel(p_NewMuzzle);
        m_spriterenderer.color = m_muzzle.m_SpriteRenderer.color;
        m_muzzlepoweruptimer = 0;
        m_muzzlepowerupactivated = true;
    }

    void MuzzlePowerUpLife()
    {
        if(m_muzzlepoweruptimer>m_muzzlepowerupduration)
        {
            SpawnBarrel(m_defaultmuzzle,m_defaultcolor);
            m_muzzlepoweruptimer = 0;
            m_muzzlepowerupactivated = false;
            m_muzzlepowerupoverwrite = true;
        }
        else
        {
            m_muzzlepoweruptimer += Time.deltaTime;
        }
    }

    void SpawnBarrel(GameObject p_NewMuzzle, Color p_NewColor)
    {
        m_spriterenderer.color = p_NewColor;
        SpawnBarrel(p_NewMuzzle);
    }

    void SpawnBarrel(GameObject p_NewMuzzle)
    {
        if(m_muzzle == null)
        {
            CreateBarrel(p_NewMuzzle);
        }
        else
        {
            if(p_NewMuzzle.name != m_muzzle.name)
            {
                CreateBarrel(p_NewMuzzle);
            }
            else
            {
                m_muzzle.Reload();
            }
        }
    }



    void CreateBarrel(GameObject p_NewMuzzle)
    {
        if (m_muzzle != null)
        {
            m_muzzle.DestroyMuzzle();
        }
        m_muzzle = Instantiate(p_NewMuzzle).GetComponent<Muzzle>();
        m_muzzle.transform.localScale = transform.localScale;

        m_muzzle.transform.position = new Vector2(transform.position.x, transform.position.y + m_spriterenderer.bounds.size.y / 2 + m_muzzle.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        m_muzzle.name = p_NewMuzzle.name;

        m_muzzle.ShowAmmo();


        m_muzzle.transform.SetParent(transform);
        m_muzzle.transform.localScale = new Vector2(1, 1);

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
    }

    

	// Update is called once per frame
	void Update ()
    {
        if (m_Alive)
        {
            MoveCheck();
            ScreenBoundryCheck();

            if (m_muzzlepowerupactivated)
                MuzzlePowerUpLife();

            if(m_healthbar!=null)
                if(!m_healthbar.m_Alive)
                {
                    Death();
                }
            
        }
        else
            return;




	}


    public void Heal(float p_Amount)
    {
        m_healthbar.Heal(p_Amount);
    }
    public void TakeDamage(float p_Damage)
    {
        m_healthbar.TakeDamage(p_Damage);
        CameraShake t_cs = Camera.main.GetComponent<CameraShake>();
        t_cs.StartShake(0.05f, 0.25f);
    }

    private void MoveCheck()
    {
        Vector2 t_PreviousPosition = transform.position;
        if (m_newposition != (Vector2)transform.position)
        {
            transform.position = Vector2.Lerp(transform.position, m_newposition, 0.2f);
        }
        else
        {
            transform.position = m_newposition;
        }
        m_Velocity = ((Vector2)transform.position - t_PreviousPosition) / Time.deltaTime;

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

        Vector2 t_playerbounds = GetComponent<SpriteRenderer>().bounds.size / 2;


        if (transform.position.x - t_playerbounds.x < -m_screensize.x)
            transform.position = new Vector2(-m_screensize.x + t_playerbounds.x, transform.position.y);

        else if (transform.position.x + t_playerbounds.x > m_screensize.x)
            transform.position = new Vector2(m_screensize.x - t_playerbounds.x, transform.position.y);
     }

    public void SetMuzzleToFire(bool p_value)
    {
        m_muzzle.m_StartFiring = p_value;
    }

 

    public void Death()
    {
        m_Alive = false;
        (Instantiate(AssetManager.m_Instance.GetPrefab("PlayerDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = m_spriterenderer.color;

        m_muzzle.DestroyMuzzle();
        m_healthbar.Death();
        BallManager.m_Instance.m_PlayerBall.Death();

        GameObject.FindGameObjectWithTag("pointblank").GetComponent<Game>().m_GameOver = true;

        Destroy(gameObject);
    }

}
