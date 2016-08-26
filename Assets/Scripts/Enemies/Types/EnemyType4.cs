﻿using UnityEngine;
using System.Collections;

public class EnemyType4 : Enemy {

    void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Initialize()
    {
        m_movementspeed = new Vector2(0.75f, 2.0f);
        m_origmovementtimer = 0.75f;
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle4");
        m_points = 100;
        m_stayinbounds = true;

        m_currentbullet = AssetManager.m_Instance.GetPrefab("EnemyBullet4");
        m_firingrate = Random.Range(2.0f, 5f);

        SetColor(new Color32(187,94,255,255));
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
    }

    public void Death()
    {
        base.Death();
    }
}