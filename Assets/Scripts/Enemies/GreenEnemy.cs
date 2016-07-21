using UnityEngine;

public class GreenEnemy : Enemy {

    void Awake()
    {
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.green;
        m_movementspeedx = 0.5f;
        m_movementtimer = -1f;

    }
    
	// Use this for initialization
	void Start ()
    {
        SetScale(1);
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


}
