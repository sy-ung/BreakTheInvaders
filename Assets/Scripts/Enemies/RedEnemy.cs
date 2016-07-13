using UnityEngine;

class RedEnemy:Enemy
{
    void Awake()
    {
        //m_spritename = "EnemyOne";
        base.Awake();
        SetSprite(AssetManager.m_Instance.GetSprite("EnemyOne"));
        m_spriterenderer.color = Color.red;
        //SetSprite(Resources.Load<Sprite>("Images/EnemyBasic"));

    }

    void Start()
    {
       
    }

    void Update()
    {
        base.Update();
    }
}

