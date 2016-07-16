using UnityEngine;


class UIControlWheel : UIElement {

    // Use this for initialization

    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_circlecollider2D;
    private Player m_player;
	void Start () {
	
	}

    private Vector2 m_previousmousepos;

    void Awake()
    {
        base.Awake();
        Initialize();
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("ControlWheel");

        m_circlecollider2D.radius = m_spriterenderer.bounds.size.x/2;
        m_circlecollider2D.isTrigger = true;
        m_rigidbody.isKinematic = true;
        m_player = PlayerManager.m_Instance.m_Player;
        SetScale(0.5f);
    }

    void Initialize()
    {
        m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_circlecollider2D = gameObject.AddComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();

        //Get first mouse location on touch/click

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {

                if (Input.GetTouch(i).position.y < m_player.GetTopYLine())
                {
                    if(Input.GetTouch(i).phase == TouchPhase.)

                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                        MovePlayer();

                }

            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    m_previousmousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}

        //if (Input.GetMouseButton(1))
        //{
        //    MovePlayer();
        //}
        
    }

    //p_TouchPosition is used for ray casting check
    void MovePlayer(Vector2 p_TouchPosition)
    {

        //Ray t_raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray t_raycast = Camera.main.ScreenPointToRay(p_TouchPosition);
        RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

        if (t_rayhit)
        {

            if (t_rayhit.collider.gameObject == gameObject)
            {
                Vector2 t_currentmousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Get a relative vector in regards to the origin
                Vector2 t_relativepreviouspos = m_previousmousepos - (Vector2)transform.position;
                Vector2 t_relativecurrentpos = t_currentmousepos - (Vector2)transform.position;

                //Calculate angle of previous and current vectors
                float t_previoupossangle = Mathf.Atan2(t_relativepreviouspos.y, t_relativepreviouspos.x) * Mathf.Rad2Deg;
                float t_currentposangle = Mathf.Atan2(t_relativecurrentpos.y, t_relativecurrentpos.x) * Mathf.Rad2Deg;

                //How much to rotate the wheel by
                float t_deltaangle = t_currentposangle - t_previoupossangle;

                transform.Rotate(Vector3.forward, t_deltaangle);

                //It bugs out when angles > 180 or < 180.
                if (t_deltaangle < 180 && t_deltaangle > -180)
                    m_player.MovePlayer(new Vector2(-t_deltaangle/8,0));

                m_previousmousepos = t_currentmousepos;

            }
        }
  

    }
}
