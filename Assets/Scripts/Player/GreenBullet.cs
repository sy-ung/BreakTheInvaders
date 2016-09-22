using UnityEngine;
using System.Collections;

public class GreenBullet : Bullet {

    private Animator m_animator;
    public GreenMuzzle m_GreenMuzzle;

    public bool m_inflight;

    float m_xpos;

    void Awake()
    {
        base.Awake();
        m_speed = 13.0f;
        m_lifetime = 10.0f;


        m_animator = gameObject.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start ()
    {
        base.Start();
        m_GreenMuzzle.ReadyToFire(false);
        gameObject.transform.localScale *= 1.75f;
        m_RigidBody2D.velocity = Vector2.zero;
        m_RigidBody2D.freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_inflight)
        { 
            CheckBoundry();
            transform.position = new Vector2(m_xpos, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(m_GreenMuzzle.transform.position.x, transform.position.y);
        }
	}

    void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (m_alive)
        {
            if (p_Collider.tag == "Enemy")
            {
                GameObject t_explosion = Instantiate(AssetManager.m_Instance.GetPrefab("BulletGreenExplosion"), p_Collider.gameObject.transform.position, Quaternion.identity) as GameObject;
                t_explosion.transform.localScale *= 2;
                p_Collider.GetComponent<Enemy>().Death();
                m_RigidBody2D.velocity = Vector3.up * m_speed;
                //base.OnCollisionEnter2D(p_Collision);
            }
        }
    }

    public void Launch(float p_Speed)
    {
        m_inflight = true;
        m_speed = p_Speed;
        m_RigidBody2D.velocity = Vector3.up * (m_speed * m_SpriteRenderer.bounds.size.y/2);
        m_xpos = transform.position.x;
    }
    public void StartAnimFinished()
    {
        //m_RigidBody2D.velocity *= 3.0f;
        m_animator.SetBool("ChargedUp", true);
        m_GreenMuzzle.ReadyToFire(true);
    }
}
