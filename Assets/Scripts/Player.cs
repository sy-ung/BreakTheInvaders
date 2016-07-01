using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Rigidbody2D m_RigidBody2D;
    private SpriteRenderer m_SpriteRenderer;
    
    void Awake()
    {
        m_RigidBody2D = gameObject.AddComponent<Rigidbody2D>();
        m_RigidBody2D.gravityScale = 0;
        m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = AssetManager.m_Instance.GetSprite("Player");
        

    }

	// Use this for initialization
	void Start ()
    {
       

    }

    public void ChangePlayerSprite(Sprite p_NewPlayerSprite)
    {
        m_SpriteRenderer.sprite = p_NewPlayerSprite;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
