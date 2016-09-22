using UnityEngine;
using System.Collections;

public class EnemyTypeSpecial : Enemy {

    void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Initialize()
    {

        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticleSpecial");
        m_points = 100;
        m_firingrate = Random.Range(2.0f, 5f);
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        m_stayinbounds = false;

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if(m_spriterenderer.color != m_currentcolor)
        {
            m_spriterenderer.color = m_currentcolor;
        }
        else
        {
            m_currentcolor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f);
        }


        if (m_InBoundsX)
        {
            if (!m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x - m_HalfSize.x > Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x)
                {
                    Destroy(gameObject);

                }
            }
            else if (m_CurrentEnemySquad.m_ChangeDirection)
            {
                if (transform.position.x + m_HalfSize.x < -Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint.x)
                {
                    Destroy(gameObject);
                }
            }
        }
   }
    void LateUpdate()
    {
        base.LateUpdate();
    }

    public void Death()
    {
        base.Death();
    }

    public override void SpawnPowerUp()
    {
        Instantiate(AssetManager.m_Instance.GetPrefab("RedPower"), transform.position, Quaternion.identity);
    }
}
