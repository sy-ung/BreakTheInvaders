using UnityEngine;
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
        m_dospeedcheck = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckMouseInput();
        base.Update();
        
	}

    void CheckTouchInput()
    {

    }

    void CheckMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 t_touchpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_Direction = (t_touchpos - (Vector2)transform.position).normalized;
        }
    }

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
