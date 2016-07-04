﻿using UnityEngine;

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

    public Ball()
    {
        m_ballaudiosystem = gameObject.AddComponent<AudioSource>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_circlecollider2D = gameObject.AddComponent<CircleCollider2D>();
    }

	// Use this for initialization
	void Awake ()
    {
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

    }

    void Start()
    {
        OnRespawn();
        
    }
    public void OnRespawn()
    {
        m_defaultspeed = 10f;
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
            //transform.position = new Vector2(transform.position.x, -m_boundrysize.y + m_ballsizecheck.y);
            //m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_rigidbody2D.velocity.y);
            BallManager.m_Instance.RespawnBall(Vector2.zero);
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
	
	// Update is called once per frame
	void Update ()
    {
        ScreenBoundryCheck();
        
        Debug.Log(m_rigidbody2D.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        
        if(p_Collision.collider.tag == "Player")
        {
            Player t_player = PlayerManager.m_Instance.GetPlayerReference();
            Vector2 t_direction = ((Vector2)transform.position - p_Collision.contacts[0].point).normalized;
            transform.position = p_Collision.contacts[0].point + t_direction * m_ballsizecheck.magnitude;
            m_rigidbody2D.velocity = t_direction * m_defaultspeed;


            if (p_Collision.contacts[0].point.y < t_player.GetTopYLine() &&
                p_Collision.contacts[0].point.y > t_player.transform.position.y)
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x + t_player.m_Velocity.x, -m_rigidbody2D.velocity.y);
            }
            else if (p_Collision.contacts[0].point.y > t_player.GetTopYLine())
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x + t_player.m_Velocity.x, m_rigidbody2D.velocity.y);
            }
            SpeedCheck();
        }
    }
    
    //So it never slows down past a certain speed
    void SpeedCheck()
    {
        Player t_player = PlayerManager.m_Instance.GetPlayerReference();

        m_direction = m_rigidbody2D.velocity.normalized;

        float t_yminspeedlimit = 3;

        if(m_rigidbody2D.velocity.magnitude < m_defaultspeed)
        {
            m_rigidbody2D.velocity = m_direction * m_defaultspeed;
        }

        if(m_rigidbody2D.velocity.y < m_defaultspeed && m_rigidbody2D.velocity.y > -m_defaultspeed)
        {
            if(transform.position.y > t_player.transform.position.y)
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, m_defaultspeed);
            if(transform.position.y < t_player.transform.position.y)
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -m_defaultspeed);
        }
 
        
    }
}
