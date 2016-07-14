using UnityEngine;

public class GreenEnemy : Enemy {

    void Awake()
    {
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.green;
        m_movementspeed = 0.5f;
        m_movementtimer = 0.75f;
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}


}
