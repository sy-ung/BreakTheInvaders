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

    void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if (m_alive)
        {
            if (p_Collider.tag == "Enemy")
            {
                GameObject t_explosion = Instantiate(AssetManager.m_Instance.GetPrefab("BulletDefaultExplosion"), transform.position, Quaternion.identity) as GameObject;
                t_explosion.transform.localScale *= 2.0f; 
                p_Collider.GetComponent<Enemy>().Death();
                base.OnTriggerEnter2D(p_Collider);
            }
        }
    }
}

