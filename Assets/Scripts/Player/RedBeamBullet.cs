using UnityEngine;
using System.Collections;

public class RedBeamBullet : Bullet {

    private Animator m_animator;
    private Vector2 m_newscale;

    public RedBeamMuzzle m_Muzzle;

    private Vector2 m_originalscale;
    private float m_interpstep;

    private bool m_beamactivated = false;

    private float m_fullpowertimer;
    private float m_fullpowertimeinterval;
    private float m_fullpowerlevel = 0;

    void Awake()
    {
        base.Awake();

        m_RigidBody2D.freezeRotation = true;
        m_speed = 0;
        m_animator = gameObject.GetComponent<Animator>();
        
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
        m_originalscale = m_newscale = transform.localScale;

        m_fullpowertimeinterval = 1.0f;
        m_SpriteRenderer.sortingLayerName = "Beam";
    }
	
	// Update is called once per frame
	void Update ()
    {
        //base.Update();
        ScaleLerp();
        //transform.position = new Vector2(m_Muzzle.transform.position.x, m_Muzzle.transform.position.y + m_SpriteRenderer.bounds.size.y / 2);
        //Debug.Log("Activated: " + m_animator.GetBool("Activated"));
        //Debug.Log("FullPower: " + m_animator.GetBool("FullPower"));
        //Debug.Log("Deactivated: " + m_animator.GetBool("Deactivated"));

        if(m_animator.GetBool("FullPower"))
        {
            FullPowerExtenstion();
        }
        else
        {
            m_fullpowertimer = 0;
            m_animator.speed = 1;
            m_fullpowerlevel = 1;
        }

        if(m_fullpowerlevel>3)
        {
            
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector2(m_Muzzle.transform.position.x, m_Muzzle.transform.position.y + m_SpriteRenderer.bounds.size.y / 2 + m_Muzzle.m_SpriteRenderer.bounds.size.y/2);

    }

    void ScaleLerp()
    {
        if ((Vector2)transform.localScale != m_newscale)
        {
            transform.localScale = Vector2.Lerp((Vector2)transform.localScale, m_newscale, m_interpstep);
            m_boxcollider2D.size = m_SpriteRenderer.sprite.bounds.size;
        }
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {

        if (m_animator.GetBool("FullPower"))
        {
            if (p_Collision.collider.tag == "Enemy")
            {
                p_Collision.collider.GetComponent<Enemy>().Death();
            }
        }
    }

    void OnCollisionStay2D(Collision2D p_Collision)
    {
        if (m_animator.GetBool("FullPower"))
        {
            if (p_Collision.collider.tag == "Enemy")
            {
                p_Collision.collider.GetComponent<Enemy>().Death();
            }
        }
    }



    public void SetNewScale(float p_ScaleFactor, float p_InterStep)
    {
        m_newscale = m_originalscale * p_ScaleFactor;
        m_interpstep = p_InterStep;
    }

    public void SetNewScale(Vector2 p_ScaleFactorMultiplier, float p_InterpStep)
    {
        m_newscale = new Vector2(m_originalscale.x * p_ScaleFactorMultiplier.x, m_originalscale.y * p_ScaleFactorMultiplier.y);
        m_interpstep = p_InterpStep;
    }

    public void IncreaseScale(Vector2 p_DeltaScaleFactorMultiplier, float p_InterStep)
    {
        m_newscale = new Vector2(m_newscale.x * p_DeltaScaleFactorMultiplier.x, m_newscale.y * p_DeltaScaleFactorMultiplier.y);
        m_interpstep = p_InterStep;
    }

    public void IncreaseScale(float p_DeltaScaleFactorMultiplier, float p_InterStep)
    {
        m_newscale *= p_DeltaScaleFactorMultiplier;
        m_interpstep = p_InterStep;
    }

    public void StartAnimFinished()
    {
        m_animator.SetBool("FullPower", true);
        SetNewScale(new Vector2(5, 10), 0.25f);

        m_animator.SetBool("Activated", false);
    }

    public void FullPowerExtenstion()
    {
        if (m_fullpowerlevel < 3)
        {
            m_fullpowertimer += Time.deltaTime;

            if(m_fullpowertimer> m_fullpowertimeinterval)
            {
                m_fullpowertimer = 0;

                    IncreaseScale(new Vector2(2.0f, 1), 0.5f);
                    m_animator.speed *= 1.25f;

                m_fullpowerlevel++;
            }
        }
    }

    public void EndAnimFinished()
    {
        SetNewScale(1, 1);
        m_animator.SetBool("Deactivated", false);
    }

    public void SetBeamActivation(bool p_value)
    {
        if(p_value)
        {
            if(!m_beamactivated)
            {
                ActivateBeam();
            }
        }
        else
        {
            if(m_beamactivated)
            {
                DeactivateBeam();
            }
        }
    }

    public void ActivateBeam()
    {
        m_beamactivated = true;
        m_animator.SetBool("Activated", true);

        if (m_animator.GetBool("Deactivated"))
            m_animator.SetBool("Deactivated", false);

        SetNewScale(new Vector2(2, 10), 0.075f);
    }

    public void DeactivateBeam()
    {
        m_beamactivated = false;
       
        if(m_animator.GetBool("Activated"))
        {
            m_animator.SetBool("Activated", false);

        }

        if (m_animator.GetBool("FullPower"))
        {
            m_animator.SetBool("FullPower", false);
            SetNewScale(new Vector2(1, m_newscale.y), 0.1f);

        }
        m_animator.SetBool("Deactivated", true);
    }

}
