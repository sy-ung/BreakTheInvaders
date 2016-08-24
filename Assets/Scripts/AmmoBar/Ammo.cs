using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

    Rigidbody2D m_rigidbody2D;
    SpriteRenderer m_spriterenderer;

    public Sprite m_EmptyShell;

    float m_lifetime = 1.5f;
    float m_lifetimer;
    bool m_ejected = false;

    public Vector2 m_NewLocalPosition;

    private Color m_alpha;

    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriterenderer = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start ()
    {
        m_rigidbody2D.isKinematic = false;
        m_rigidbody2D.gravityScale = 0;
        m_spriterenderer.sortingLayerName = "UI";
        transform.localScale = new Vector2(2, 2);
        m_spriterenderer.color = new Color(1, 1, 1, 0);
	}

    public void Eject()
    {
        gameObject.transform.SetParent(null);
        m_rigidbody2D.gravityScale = 1;

        Vector2 m_randomejectvector = new Vector2(Random.Range(-1.0f,-0.3f), Random.Range(0.25f, 1.0f));
        m_rigidbody2D.AddForce(m_randomejectvector.normalized * 300);
        m_rigidbody2D.AddTorque(Random.Range(-3000.0f,3000.0f));
        m_spriterenderer.sprite = m_EmptyShell;
        m_ejected = true;
    }
	
    public void SetTransparency(float p_Alpha)
    {
        m_alpha = new Color(1, 1, 1, p_Alpha);
    }

    public void AddTransparency(float p_DeltaAlpha)
    {
        m_alpha = new Color(1, 1, 1, m_alpha.a + p_DeltaAlpha);
    }

	// Update is called once per frame
	void Update ()
    {

        if (m_spriterenderer.color != m_alpha)
        {
            m_spriterenderer.color = Color.Lerp(m_spriterenderer.color, m_alpha, 0.1f);
        }
        if (m_ejected)
        {
            m_lifetimer += Time.deltaTime;
            if (m_lifetimer > m_lifetime)
                Destroy(gameObject);
        }
        else
        {
            if((Vector2)transform.localPosition != m_NewLocalPosition)
            {
                transform.localPosition = Vector2.Lerp(transform.localPosition, m_NewLocalPosition, 0.33f);
            }

        }
	}
}
