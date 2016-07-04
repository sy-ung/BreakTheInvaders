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

    private BoxCollider2D m_boxcollider2D;

    private Vector2 m_newposition;
    private Rigidbody2D m_rigidbody2D;
    private Vector2 m_originalscale;

    //Offsets for boundry check against player image
    private Vector2 m_playersizecheck;
    private Vector2 m_boundrysize;

    public Player()
    {
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
    }
    
    void Awake()
    {
        m_rigidbody2D.gravityScale = 0;
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("Player");

        m_rigidbody2D.mass = 1;
        m_rigidbody2D.isKinematic = true;

        //This is to calculate the scale of the player paddle in propotion to the screen size
        float t_AspectRatio = (float)Camera.main.pixelHeight / (float)Camera.main.pixelWidth;
        m_originalscale = (transform.localScale * t_AspectRatio);
        transform.localScale = m_originalscale;

        //Converting screen width and height to world points
        m_boundrysize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_playersizecheck = new Vector2(m_spriterenderer.bounds.size.x / 2, m_spriterenderer.bounds.size.y / 2);

        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);
       
    }

	// Use this for initialization
	void Start ()
    {
        m_newposition = transform.position;

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
        m_playersizecheck = new Vector2(m_spriterenderer.bounds.size.x / 2, m_spriterenderer.bounds.size.y / 2);
    }

	// Update is called once per frame
	void Update ()
    {
        MoveCheck();
        ScreenBoundryCheck();
	}


    private void MoveCheck()
    {
        if(m_newposition != (Vector2)transform.position)
        {
            Vector2 t_PreviousPosition = transform.position;
            transform.position = Vector2.Lerp(transform.position, m_newposition, 0.2f);
            m_Velocity = ((Vector2)transform.position - t_PreviousPosition) / Time.deltaTime;
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
}
