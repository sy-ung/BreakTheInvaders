using UnityEngine;

public class GreenEnemy : Enemy {

    void Awake()
    {
        base.Awake();
        SetSprite(AssetManager.m_Instance.GetSprite("EnemyOne"));
        m_spriterenderer.color = Color.green;
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
