using UnityEngine;
using System.Collections;

public class EnemyBullet4 : EnemyBullet
{
    float m_rotation = 0;

    void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.forward, m_rotation);
        m_rotation += 45.0f;
        
        base.Update();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
    }
}
