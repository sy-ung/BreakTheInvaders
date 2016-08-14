using UnityEngine;
using System.Collections;

public class EnemyBullet : Bullet {


    void Awake()
    {
        m_speed = -5.0f;
        m_lifetime = 6.0f;
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        gameObject.layer = 13;
    }
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        Debug.Log(p_Collision.gameObject.name);
        base.OnCollisionEnter2D(p_Collision);
    }
}
