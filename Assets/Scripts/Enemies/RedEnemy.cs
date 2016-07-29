using UnityEngine;

class RedEnemy:Enemy
{
    void Awake()
    {
        //m_spritename = "EnemyOne";
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.red;
        m_movementspeed = Vector2.zero;

    }

    void Start()
    {
       
    }

    void Update()
    {
        base.Update();
    }
}

