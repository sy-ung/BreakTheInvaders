﻿using UnityEngine;
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
        m_piercing = true;
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

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
    }
}
