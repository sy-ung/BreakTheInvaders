using UnityEngine;
using System.Collections;

public class RedBall : Ball {

    private Collider2D[] m_enemiessucked;
    private Collider2D[] m_bulletssucked;

    float m_vacuumradius;

    void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        base.Start();

        m_vacuumradius = GetComponent<CircleCollider2D>().radius * 5;

        ParticleSystem.ShapeModule t_sm = GetComponent<ParticleSystem>().shape;
        t_sm.radius = m_vacuumradius;

        GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Vacuum();
    }

    void LateUpdate()
    {
        base.LateUpdate();

    }

    public override void OnTriggerEnter2D(Collider2D p_Collider) 
    {
        if (p_Collider.tag == "Enemy")
        {
            p_Collider.GetComponent<Enemy>().Death();
        }
        else if(p_Collider.tag == "EnemyBullet")
        {
            Destroy(p_Collider.gameObject);
        }
        else if (p_Collider.tag == "Player")
        {
            CheckCollision(p_Collider);
            p_Collider.gameObject.GetComponent<Player>().m_Muzzle.Reload();
        }
    }

    void Vacuum()
    {
        Collider2D[] t_enemies = Physics2D.OverlapCircleAll(transform.position, m_vacuumradius, 1 << LayerMask.NameToLayer("Enemy"));
        Collider2D[] t_enemybullets = Physics2D.OverlapCircleAll(transform.position, m_vacuumradius, 1 << LayerMask.NameToLayer("EnemyBullet"));


        for (int i = 0; i<t_enemies.Length;++i)
        {
            if(t_enemies[i]!=null)
            { 
                t_enemies[i].transform.SetParent(null);
                t_enemies[i].GetComponent<LerpingBehaviour>().LerpToGameObject(gameObject);
            }
        }

        for(int i = 0;i<t_enemybullets.Length;++i)
        {
            if (t_enemybullets[i] != null)
                t_enemybullets[i].GetComponent<LerpingBehaviour>().LerpToGameObject(gameObject);
        }

        m_bulletssucked = t_enemybullets;
        m_enemiessucked = t_enemies;
    }

    public override void DestroyBall()
    {
        if(m_enemiessucked!=null)
        { 
            for(int i = 0;i<m_enemiessucked.Length;i++)
            {
                if(m_enemiessucked[i] !=null)
                    m_enemiessucked[i].GetComponent<Enemy>().Death();
            }
        }

        if (m_bulletssucked != null)
        {
            for(int i = 0;i<m_bulletssucked.Length;i++)
            {
                if (m_bulletssucked[i] != null)
                    Destroy(m_bulletssucked[i].gameObject);
            }
        }

        base.DestroyBall();
    }

    public override void Death()
    {
        (Instantiate(AssetManager.m_Instance.GetPrefab("BallDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = Color.red;
        base.Death();
    }

}
