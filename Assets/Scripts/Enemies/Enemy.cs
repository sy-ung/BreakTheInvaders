using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    protected SpriteRenderer m_spriterenderer;

    protected float m_movementspeedy;

    protected float m_movementspeedx;
    public float m_MovementSpeedX
    {
        set { m_movementspeedx = value; }
    }
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

    protected float m_movementtimer;
    float m_timer = 0;

    private Vector2 m_boundries;

    public bool m_movedown = false;

    protected void Awake()
    {
        Initialize();
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.isKinematic = true;
        m_rigidbody2D.freezeRotation = true;
        m_movementspeedy = 0.5f;
        m_boundries = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
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

	// Use this for initialization
	void Start() {
	
	}

    // Update is called once per frame
    public bool stopmoving = false;

    protected void Update()
    {
        if (m_movedown)
            MoveDown();
        else
            MoveEnemy();
    }

    protected void LateUpdate()
    {
        if(!m_movedown)
            CheckBoundry();
        Debug.Log(transform.position);
    }

    public void MoveEnemy()
    {
        if (!stopmoving)
        { 
            if (m_timer > m_movementtimer)
            {
                m_timer = 0;

                if (m_changedirection)
                    transform.position = new Vector2(transform.position.x - m_HalfSize.x * m_movementspeedx, transform.position.y);
                else if (!m_changedirection)
                    transform.position = new Vector2(transform.position.x + m_HalfSize.x * m_movementspeedx, transform.position.y);  
            }
            else
            {
                m_timer += Time.deltaTime;
            }
        }
    }
    public bool CheckBoundry()
    {
        if (!m_changedirection)
        {
            if (transform.position.x + m_HalfSize.x * m_movementspeedx > m_boundries.x - m_HalfSize.x * 2)
            {
                m_spriterenderer.color = Color.red;
                EnemyManager.m_Instance.MoveEnemiesDown();
                return true;
            }
            else
                return false;

        }
        else if (m_changedirection)
        {
            if (transform.position.x - m_HalfSize.x * m_movementspeedx < -m_boundries.x + m_HalfSize.x * 2)
            {
                m_spriterenderer.color = Color.blue;
                EnemyManager.m_Instance.MoveEnemiesDown();
                return true;
            }
            else
                return false;
        }
        return false;
    }

    public void MoveDown()
    {

        ChangeDirection();
        transform.position += new Vector3(0, -0.1f, 0);
        m_movedown = false;
        if (transform.position.y < PlayerManager.m_Instance.m_Player.GetTopYLine() + m_HalfSize.y * 2)
        {
            EnemyManager.m_Instance.KillAllEnemies();
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
}
