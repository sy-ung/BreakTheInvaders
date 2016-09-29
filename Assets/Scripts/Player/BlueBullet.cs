using UnityEngine;
using System.Collections;

public class BlueBullet : Bullet {

    private Animator m_animator;


    void Awake()
    {
        m_speed = 4.0f;
        m_lifetime = 10.0f;
        base.Awake();

        m_animator = gameObject.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
        transform.localScale = new Vector2(3f, 3f);
        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}

    void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (m_alive)
        {
            if (p_Collider.tag == "Enemy")
            {
                Vector2 t_spawnpos = new Vector2(transform.position.x, p_Collider.transform.position.y);
                Explosion t_explosion = (Instantiate(AssetManager.m_Instance.GetPrefab("BulletBlueExplosion"), t_spawnpos, Quaternion.identity) as GameObject).GetComponent<Explosion>();
                t_explosion.transform.localScale *= 1.75f;
                t_explosion.SetAnimationSpeed(3.0f);
                //p_Collision.collider.GetComponent<Enemy>().Death();
                base.OnTriggerEnter2D(p_Collider);
            }
        }
    }

    public void StartAnimFinished()
    {
        m_RigidBody2D.velocity *= 7.0f;
        m_animator.SetBool("InFlight", true);
        GameAudioManager.m_Instance.PlaySound("BlueBulletBoost",false,1.0f,0.7f);
    }

}
