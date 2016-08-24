using UnityEngine;
using System.Collections;

public class EnemyBullet : Bullet {


    protected void Awake()
    {
        m_speed = -5.0f;
        m_lifetime = 6.0f;
        base.Awake();
        gameObject.layer = 13;
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

        Debug.Log(p_Collision.gameObject.name);
        base.OnCollisionEnter2D(p_Collision);
    }
}
