﻿using UnityEngine;
using System.Collections;

public class EnemyType5 : Enemy {

    void Awake()
    {
        base.Awake();
        Initialize();
    }

    void Initialize()
    {

        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle5");
        m_points = 100;
        m_stayinbounds = true;

        m_currentbullet = AssetManager.m_Instance.GetPrefab("EnemyBullet5");
        m_firingrate = Random.Range(2.0f, 5f);

        SetColor(new Color32(238,32,35,255));

        m_firesoundname = "Enemy5Fire";
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
