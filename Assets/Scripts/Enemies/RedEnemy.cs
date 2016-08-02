using UnityEngine;

class RedEnemy:Enemy
{
    void Awake()
    {
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.red;
        m_movementspeed = new Vector2(1.0f, 2.0f);
        m_origmovementtimer = 0.60f;
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle1");
        m_points = 125;
        m_stayinbounds = true;

    }

    void Start()
    {
        SetScale(1);
        m_deathparticle.GetComponent<ParticleSystem>().startColor = Color.red;
        base.Start();
    }


    void Update()
    {
        base.Update();
    }


    void LateUpdate()
    {
        base.LateUpdate();
    }

    public void Death()
    {
        base.Death();
    }
}

