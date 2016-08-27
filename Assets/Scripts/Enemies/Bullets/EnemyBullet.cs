using UnityEngine;
using System.Collections;

public class EnemyBullet : Bullet {


    protected void Awake()
    {
        m_speed = -2.5f;
        m_lifetime = 6.0f;
        base.Awake();
        gameObject.layer = 13;
        m_Damage = 5.0f;
    }

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        transform.localScale *= 2;

    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    protected void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if(p_Collision.gameObject.tag == "Player")
        {
            p_Collision.gameObject.GetComponent<Player>().TakeDamage(m_Damage);
        }
        base.OnCollisionEnter2D(p_Collision);
    }
}
