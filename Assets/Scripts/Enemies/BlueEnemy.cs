using UnityEngine;
using System.Collections;

public class BlueEnemy : Enemy {

    void Awake()
    {
        base.Awake();
        SetSprite("EnemyTwo");
        m_spriterenderer.color = Color.blue;
        m_movementspeed = new Vector2(1, 1);
        m_origmovementtimer = 0.1f;
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle1");
        m_points = 110;
        m_stayinbounds = true;
    }

	// Use this for initialization
	void Start ()
    {
        SetScale(1);
        m_deathparticle.GetComponent<ParticleSystem>().startColor = Color.blue;
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
