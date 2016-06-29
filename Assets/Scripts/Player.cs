using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Texture2D m_DefaultPlayerTexture;

    private Rigidbody2D m_RigidBody2D;
    private SpriteRenderer m_SpriteRenderer;
    
    void Awake()
    {
        m_RigidBody2D = gameObject.AddComponent<Rigidbody2D>();
        m_RigidBody2D.gravityScale = 0;
        m_SpriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        
        

    }

	// Use this for initialization
	void Start ()
    {
        m_SpriteRenderer.materials.

    }

	// Update is called once per frame
	void Update () {
	
	}
}
