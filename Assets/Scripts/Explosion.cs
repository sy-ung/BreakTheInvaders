using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_circlecollider2D;
    private SpriteRenderer m_spriterenderer;

    void Awake()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_circlecollider2D = gameObject.GetComponent<CircleCollider2D>();
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        Initialize();
    }

    void Initialize()
    {
        if(m_circlecollider2D!=null)
        { 
            m_circlecollider2D.radius = m_spriterenderer.bounds.size.x / 2;
            m_circlecollider2D.isTrigger = true;
        }
        if(m_rigidbody!=null)
        { 
            m_rigidbody.isKinematic = true;
            m_rigidbody.gravityScale = 0;
        }
        m_spriterenderer.sortingLayerName = "Explosion";
        gameObject.tag = "Explosion";
        gameObject.layer = 11;
        transform.localScale *= 2;
    }

	// Use this for initialization
	void Start ()
    {
           
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Explode()
    {
        Collider2D[] t_collisions = Physics2D.OverlapCircleAll(transform.position, m_circlecollider2D.radius, 1 << LayerMask.NameToLayer("Enemy"));
        for(int i = 0;i<t_collisions.Length;i++)
        {
            if(t_collisions[i].gameObject.tag == "Enemy")
            {
                t_collisions[i].gameObject.GetComponent<Enemy>().Death();
            }
            else
            {
                Debug.Log("NO ENEMIES");
            }
        }
    }

    public void ExplosionFinished()
    {
        Destroy(gameObject);
    }


}
