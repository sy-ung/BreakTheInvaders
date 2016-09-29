using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_circlecollider2D;
    private SpriteRenderer m_spriterenderer;

    [SerializeField]
    string m_AudioClipName;

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
    }

    public void SetAnimationSpeed(float p_Speed)
    {
        GetComponent<Animator>().speed = p_Speed;
    }

	// Use this for initialization
	void Start ()
    {
        Explode();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Explode()
    {
        if(PlayerManager.m_Instance.m_Player != null)
        { 
            if((PlayerManager.m_Instance.m_Player.transform.position - transform.position).magnitude < m_spriterenderer.bounds.size.x/2)
            {
                CameraShake t_cs = Camera.main.GetComponent<CameraShake>();
                t_cs.StartShake(GetComponent<Animator>().playbackTime, 0.3f);
            }
        }

        GameAudioManager.m_Instance.PlaySound(m_AudioClipName, false, 1.0f,0.6f);

        if(m_circlecollider2D!=null)
        { 
            Collider2D[] t_collisions = Physics2D.OverlapCircleAll(transform.position, m_spriterenderer.bounds.size.x / 2, 1 << LayerMask.NameToLayer("Enemy"));
            for(int i = 0;i<t_collisions.Length;i++)
            {
                if(t_collisions[i].gameObject.tag == "Enemy")
                {
                    t_collisions[i].gameObject.GetComponent<Enemy>().Death();
                }
            }
        }
    }

    public void ExplosionFinished()
    {
        Destroy(gameObject);
    }


}
