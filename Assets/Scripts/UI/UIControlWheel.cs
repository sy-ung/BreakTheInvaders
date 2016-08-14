using UnityEngine;
using UnityEngine.UI;

class UIControlWheel : UIElement {

    // Use this for initialization

    private Rigidbody2D m_rigidbody;
    private CircleCollider2D m_circlecollider2D;
    private Player m_player;
	void Start () {
        m_player = PlayerManager.m_Instance.m_Player;
    }

    private Vector2 m_previousmousepos;

    void Awake()
    {
        base.Awake();
        Initialize();

        m_circlecollider2D.radius = m_spriterenderer.bounds.size.x/2;
        m_circlecollider2D.isTrigger = true;
        m_rigidbody.isKinematic = true;

        SetScale(0.75f);
    }

    void Initialize()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_circlecollider2D = gameObject.GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();

        InputCheck();
        
    }

    void InputCheck()
    {
        //Get first mouse location on touch/click

        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                //if (Input.GetTouch(i).phase == TouchPhase.Began)
                //    m_previousmousepos = Camera.main.ScreenToWorldPoint( Input.GetTouch(i).position);

                Ray t_raycast = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

                if (t_rayhit)
                {

                    if (t_rayhit.collider.gameObject == gameObject)
                    {
                        MovePlayer(Input.GetTouch(i).position);
                    }
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

    void MovePlayer(Vector2 p_TouchScreenCoord)
    {

        Vector2 t_currentmousepos = Camera.main.ScreenToWorldPoint(p_TouchScreenCoord);

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
            m_player.MovePlayer(new Vector2(-t_deltaangle/12,0));

        m_previousmousepos = t_currentmousepos;
    }
}
