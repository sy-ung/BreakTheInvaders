using UnityEngine;

public class EnemyType1 : Enemy {

    void Awake()
    {
        base.Awake();
        Initialize();

    }

    void Initialize()
    {
        m_deathparticle = AssetManager.m_Instance.GetPrefab("EnemyDeathParticle1");
        m_points = 100;
        m_stayinbounds = true;

        m_currentbullet = AssetManager.m_Instance.GetPrefab("EnemyBullet1");

        m_firesoundname = "Enemy1Fire";

        SetColor(new Color32(120,221,168,255));
    }

	// Use this for initialization
	void Start ()
    {
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
