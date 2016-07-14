using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    protected SpriteRenderer m_spriterenderer;

    protected float m_movementspeed;

    public Vector2 m_HalfSize
    {
        get 
        {
            return m_spriterenderer.bounds.size / 2;
        }
    }

    private Rigidbody2D m_rigidbody2D;
    private BoxCollider2D m_boxcollider2D;

    protected float m_movementtimer;
    float m_timer = 0;



    protected void Awake()
    {
        Initialize();
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.isKinematic = true;
        m_rigidbody2D.freezeRotation = true;
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
        transform.localScale = transform.localScale * t_scaleFactor;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update ()
    {


        if(m_timer>m_movementtimer)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - m_HalfSize.y * m_movementspeed);
            m_timer = 0;
        }
        else
        {
            m_timer += Time.deltaTime;
        }

        if(transform.position.y < PlayerManager.m_Instance.m_Player.GetTopYLine())
        {
            EnemyManager.m_Instance.KillAllEnemies();
        }
	
	}

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if(p_Collision.collider.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(m_boxcollider2D, p_Collision.collider);
        }
    }
}
