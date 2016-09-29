using UnityEngine;
using System.Collections;

public class BlueBall : Ball {

    void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }
    Vector2 POS;
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void OnTriggerEnter2D(Collider2D p_Collider)
    {
        if(p_Collider.tag == "Enemy")
        {
            p_Collider.GetComponent<Enemy>().Death();
        }
        else if(p_Collider.tag == "EnemyBullet")
        {
            Destroy(p_Collider.gameObject);
        }
        else if(p_Collider.tag == "Player")
        {
            CheckCollision(p_Collider);
            p_Collider.gameObject.GetComponent<Player>().m_Muzzle.Reload();
        }
    }

    public override void Death()
    {
        (Instantiate(AssetManager.m_Instance.GetPrefab("BallDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = Color.blue;
        base.Death();
    }
}
