using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;

    public Vector2 m_HalfSize
    {
        get 
        {
            return m_spriterenderer.bounds.size / 2;
        }
    }

    private Rigidbody2D m_rigidbody2D;
    private BoxCollider2D m_boxcollider2D;

    

    public Enemy()
    {

    }

    void Awake()
    {
        Initialize();
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.isKinematic = false;

        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("EnemyOne");
        m_boxcollider2D.size = new Vector2(m_spriterenderer.sprite.bounds.size.x, m_spriterenderer.sprite.bounds.size.y);

        Vector2 t_screensize = Camera.main.ScreenToWorldPoint( new Vector2(Screen.width, Screen.height));
        float t_scaleFactor = (m_spriterenderer.bounds.size.x / t_screensize.x) * 0.5f;
        transform.localScale = transform.localScale * t_scaleFactor;

    }

    public void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
