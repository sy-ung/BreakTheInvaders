using UnityEngine;
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
        m_circlecollider2D.isTrigger = true;

        m_rigidbody.isKinematic = true;

        SetScale(1.5f);
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
                        if (Input.GetTouch(i).phase == TouchPhase.Began)
                        {
                            m_player.SetMuzzleToFire(true);
                        }
                        
                        if (Input.GetTouch(i).phase == TouchPhase.Ended)
                        {
                            m_player.SetMuzzleToFire(false);
                        }
                        
                    }
                }
            }
        }
    }


}
