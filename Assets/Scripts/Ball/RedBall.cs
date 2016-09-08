using UnityEngine;
using System.Collections;

public class RedBall : Ball {

    private Collider2D[] m_enemiessucked;

    void Awake()
    {
        base.Awake();
        m_piercing = true;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    void LateUpdate()
    {
        base.LateUpdate();
        Vacuum();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
        if (p_Collision.collider.tag == "Enemy")
        {
            
        }


    }

    void OnCollisionExit2D(Collision2D p_Collision)
    {
        base.OnCollisionExit2D(p_Collision);
    }

    void Vacuum()
    {
        Collider2D[] t_enemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 5, 1 << LayerMask.NameToLayer("Enemy"));

        for(int i = 0; i<t_enemies.Length;++i)
        {
            t_enemies[i].transform.SetParent(null);
            t_enemies[i].GetComponent<LerpingBehaviour>().LerpToPosition(transform.position);
        }

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
        base.DestroyBall();
    }

    public override void Death()
    {
        (Instantiate(AssetManager.m_Instance.GetPrefab("BallDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = Color.red;
        base.Death();
    }

}
