using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {



    private SpriteRenderer m_spriterenderer;
    public SpriteRenderer m_SpriteRenderer
    {
        get {return m_spriterenderer;}
    }



    private Vector2 m_velocity;
    public Vector2 m_Velocity
    {
        get {return m_velocity;}
        private set { m_velocity = value;}
    }

    private Vector2 m_newposition;
    private Rigidbody2D m_rigidbody2D;
    private Vector2 m_originalscale;

    public Player()
    {
        m_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
    }
    
    void Awake()
    {
        m_rigidbody2D.gravityScale = 0;
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("Player");
        m_rigidbody2D.mass = 1;

        //This is to calculate the scale of the player paddle in propotion to the screen size
        float t_AspectRatio = (float)Camera.main.pixelHeight / (float)Camera.main.pixelWidth;
        m_originalscale = (transform.localScale * t_AspectRatio);
        transform.localScale = m_originalscale;

        
    }

	// Use this for initialization
	void Start ()
    {
        m_newposition = transform.position;

    }

    public void ChangePlayerSprite(Sprite p_NewPlayerSprite)
    {
        m_spriterenderer.sprite = p_NewPlayerSprite;
    }
    public void MovePlayer(Vector2 p_DeltaMovement)
    {
        m_newposition = (Vector2)transform.position + p_DeltaMovement;
    }
    public void SetPlayerPosition(Vector2 p_NewPlayerPosition)
    {
        m_newposition = p_NewPlayerPosition;
    }

	// Update is called once per frame
	void Update ()
    {
        MoveCheck();
	}

    private void MoveCheck()
    {
        if(m_newposition != (Vector2)transform.position)
        {
            Vector2 t_PreviousPosition = transform.position;
            transform.position = Vector2.Lerp(transform.position, m_newposition, 0.2f);
            m_Velocity = ((Vector2)transform.position - t_PreviousPosition) / Time.deltaTime;

        }

        //Debug.Log(m_Velocity);
    }
}
