using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GreenBall : Ball {

    void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CheckTouchInput();
        //CheckMouseInput();
        base.Update();
        
	}

    //void CheckTouchInput()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        for (int i = 0; i < Input.touchCount; i++)
    //        {
    //            Ray t_raycast = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
    //            RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

    //            if(t_rayhit.collider != null)
    //            { 
    //                if (t_rayhit.collider.gameObject.tag != "ControlBox" && t_rayhit.collider.gameObject.tag != "ShootButton")
    //                {
    //                    if (Input.GetTouch(i).phase == TouchPhase.Began)
    //                    { 
    //                        Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
    //                        m_Direction = (t_touchpos - (Vector2)transform.position).normalized;

    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (Input.GetTouch(i).phase == TouchPhase.Began)
    //                {
    //                    Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
    //                    m_Direction = (t_touchpos - (Vector2)transform.position).normalized;
    //                }
    //            }

    //        }
    //    }
    //}

    //void CheckMouseInput()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        Ray t_raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

    //        if(t_rayhit.collider != null)
    //        { 
    //            if (t_rayhit.collider.gameObject.tag != "ControlBox" && t_rayhit.collider.gameObject.tag != "ShootButton")
    //            { 
    //                Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                m_Direction = (t_touchpos - (Vector2)transform.position).normalized;
    //            }
    //        }
    //        else
    //        {
    //            Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //            m_Direction = (t_touchpos - (Vector2)transform.position).normalized;
    //        }
    //    }
    //}

    void LateUpdate()
    {
        base.LateUpdate();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
    }

    public override void Death()
    {
        (Instantiate(AssetManager.m_Instance.GetPrefab("BallDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = Color.green;
        base.Death();
    }

}
