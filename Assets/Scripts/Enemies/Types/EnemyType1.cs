﻿using UnityEngine;

public class EnemyType1 : Enemy {

    void Awake()
    {
        base.Awake();
        Initialize();

    }

    void Initialize()
    {
        m_movementspeed = new Vector2(0.75f, 2.0f);
        m_origmovementtimer = 0.75f;
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle1");
        m_points = 100;
        m_stayinbounds = true;

        m_currentbullet = AssetManager.m_Instance.GetPrefab("EnemyBullet1");


        SetColor(new Color32(120,221,168,255));
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
