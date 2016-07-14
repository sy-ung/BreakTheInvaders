using UnityEngine;

class RedEnemy:Enemy
{
    void Awake()
    {
        //m_spritename = "EnemyOne";
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.red;
        m_movementspeed = 0.5f;
        m_movementtimer = 0.5f;

    }

    void Start()
    {
       
    }

    void Update()
    {
        base.Update();
    }
}

