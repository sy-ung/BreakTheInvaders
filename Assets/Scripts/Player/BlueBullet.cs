using UnityEngine;
using System.Collections;

public class BlueBullet : Bullet {

    private Animator m_animator;

    void Awake()
    {
        m_speed = 5.0f;
        m_lifetime = 10.0f;
        base.Awake();

        m_animator = gameObject.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
        gameObject.transform.localScale *= 2.5f;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if (m_alive)
        {
            if (p_Collision.collider.tag == "Enemy")
            {
                Vector2 t_spawnpos = new Vector2(transform.position.x, p_Collision.collider.transform.position.y);
                Instantiate(AssetManager.m_Instance.GetPrefab("BulletBlueExplosion"), t_spawnpos, Quaternion.identity);
                //p_Collision.collider.GetComponent<Enemy>().Death();
                base.OnCollisionEnter2D(p_Collision);
            }
        }
    }

    public void StartAnimFinished()
    {
        m_RigidBody2D.velocity *= 3.0f;
        m_animator.SetBool("InFlight", true);
    }

}
