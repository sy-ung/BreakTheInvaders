using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    //Components created in Code
    private AudioSource m_ballaudiosystem;
    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriterenderer;
    private Vector2 m_originalscale;

    public Ball()
    {
        m_ballaudiosystem = gameObject.AddComponent<AudioSource>();
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Awake ()
    {
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("PlayerBall");
        m_rigidbody2D.gravityScale = 0;
        m_rigidbody2D.mass = 1;


        // This is to make sure that the scale of the ball is compliant with the size of the paddle, on start of the game.
        Vector2 t_PlayerSize = PlayerManager.m_Instance.GetPlayerSize();
        float t_ScaleFactor = t_PlayerSize.y/ m_spriterenderer.bounds.size.y;
        m_originalscale = transform.localScale * (t_ScaleFactor * 1.5f);
        transform.localScale = m_originalscale;
        
            
	}

    void Start()
    {
    }

    public void ChangeBallSprite(Sprite p_NewBallSprite)
    {
        m_spriterenderer.sprite = p_NewBallSprite;
    }

	
	// Update is called once per frame
	void Update () {
        
    }
}
