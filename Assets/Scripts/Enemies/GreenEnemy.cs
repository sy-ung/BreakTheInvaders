using UnityEngine;

public class GreenEnemy : Enemy {

    void Awake()
    {
        base.Awake();
        SetSprite("EnemyOne");
        m_spriterenderer.color = Color.green;
        m_movementspeed = new Vector2(0.75f, 1.0f);
        m_origmovementtimer = 0.75f;
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle1");
        m_points = 100;
        m_stayinbounds = true;
    }

	// Use this for initialization
	void Start ()
    {
        SetScale(1);
        m_deathparticle.GetComponent<ParticleSystem>().startColor = Color.green;
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
