using UnityEngine;
using UnityEngine.UI;

class UIControlBar : UIElement {

    private Rigidbody2D m_rigidbody;
    private BoxCollider2D m_boxcollider2D;

    private Player m_player;

    private Vector2 m_previoustouchposition;

    void Awake()
    {
        base.Awake();
        Initialize();

        SetSprite("ControlBar");
        m_boxcollider2D.size = m_spriterenderer.bounds.size;
        m_boxcollider2D.isTrigger = true;
        m_rigidbody.isKinematic = true;
        m_player = PlayerManager.m_Instance.m_Player;
        SetScale(1);

    }

    void Initialize()
    {
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.AddComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        InputCheck();
	}

    void InputCheck()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Ray t_raycast = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

                if (t_rayhit)
                {

                    if (t_rayhit.collider.gameObject == gameObject)
                    {
                        if(Input.GetTouch(i).phase == TouchPhase.Began)
                            m_previoustouchposition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                        MovePlayer(Input.GetTouch(i).position);
                    }
                }
            }
        }
    }

    void MovePlayer(Vector2 p_TouchScreenCoord)
    {
        Vector2 t_currenttouchposition = Camera.main.ScreenToWorldPoint(p_TouchScreenCoord);
        m_player.MovePlayer(new Vector2((t_currenttouchposition.x - m_previoustouchposition.x) * 10, 0));
        m_previoustouchposition = t_currenttouchposition;
    }
}
