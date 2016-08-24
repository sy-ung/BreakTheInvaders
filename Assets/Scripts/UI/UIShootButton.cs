using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIShootButton : UIElement {

    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_circlecollider2D;

    private Player m_player;


    void Awake()
    {
        base.Awake();

        Initialize();

        m_circlecollider2D.radius = m_spriterenderer.bounds.size.x/2;
        m_rigidbody.isKinematic = true;

        SetScale(1.5f);

        m_player = PlayerManager.m_Instance.m_Player;

        gameObject.tag = "ShootButton";

    }

    void Initialize()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_circlecollider2D = gameObject.GetComponent<CircleCollider2D>();
    }

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerFireWeapon(TouchPhase p_Phase)
    {
        if (p_Phase == TouchPhase.Began || p_Phase == TouchPhase.Stationary)
        {
            m_spriterenderer.color = Color.red;
            m_player.SetMuzzleToFire(true);
        }

        if (p_Phase == TouchPhase.Ended)
        {
            m_spriterenderer.color = Color.white;
            m_player.SetMuzzleToFire(false);
        }
    }


    void DebugStuff(string p_Message)
    {
        GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>().text = p_Message;
    }

}
