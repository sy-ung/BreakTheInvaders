using UnityEngine;
using System.Collections;

public class EnemyType2 : Enemy {


    void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Initialize()
    {

        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle2");
        m_points = 100;
        m_stayinbounds = true;

        m_currentbullet = AssetManager.m_Instance.GetPrefab("EnemyBullet2");
        m_firingrate = 1.0f;

        SetColor(new Color32(100,255,102,255));

        m_firesoundname = "Enemy2Fire";
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
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
