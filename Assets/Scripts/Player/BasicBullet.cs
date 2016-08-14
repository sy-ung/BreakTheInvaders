using UnityEngine;

class BasicBullet : Bullet
{
    void Awake()
    {
        m_MuzzleFlash = AssetManager.m_Instance.GetPrefab("MuzzleFlashDefault");
        m_speed = 7.0f;
        m_lifetime = 10.0f;
        base.Awake();
    }
    void Start()
    {

        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        if (m_alive)
        {
            if (p_Collision.collider.tag == "Enemy")
            {
                GameObject t_explosion = Instantiate(AssetManager.m_Instance.GetPrefab("BulletDefaultExplosion"), transform.position, Quaternion.identity) as GameObject;
                t_explosion.transform.localScale *= 2.0f; 
                p_Collision.collider.GetComponent<Enemy>().Death();
                base.OnCollisionEnter2D(p_Collision);
            }
        }
    }
}

