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
    private int m_fullpowerlevel = 0;

    private float m_soundcliplength;


    void Awake()
    {
        base.Awake();

        m_RigidBody2D.freezeRotation = true;
        m_speed = 0;
        m_animator = gameObject.GetComponent<Animator>();
        m_originalscale = m_newscale = transform.localScale;

    }

	// Use this for initialization
	void Start ()
    {
        base.Start();

        m_soundcliplength = GameAudioManager.m_Instance.GetSoundClip("BeamFireChargeUp").length;
        m_fullpowertimeinterval = 1.0f;
        m_SpriteRenderer.sortingLayerName = "Beam";
    }
	
	// Update is called once per frame
	void Update ()
    {
        //base.Update();
        ScaleLerp();

        if (m_animator.GetBool("FullPower"))
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

    void OnTriggerEnter2D(Collider2D p_Collider)
    {

        if (m_animator.GetBool("FullPower"))
        {
            if (p_Collider.tag == "Enemy")
            {
                p_Collider.GetComponent<Enemy>().Death();
            }
        }
    }

    void OnTriggerStay2D(Collider2D p_Collider)
    {
        if (m_animator.GetBool("FullPower"))
        {
            if (p_Collider.tag == "Enemy")
            {
                p_Collider.GetComponent<Enemy>().Death();
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
        PlayFullPowerSounds();
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
                PlayFullPowerSounds();
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

    public void PlayActivatedSound()
    {

        if(m_animator.GetBool("Activated"))
        { 
            GameAudioManager.m_Instance.PlaySound("BeamFireChargeUp", false, m_soundcliplength / m_animator.GetCurrentAnimatorStateInfo(0).length,false);
            GameAudioManager.m_Instance.StopSound("BeamFireChargeDown");
        }
        //if (m_audiosource.isPlaying)
        //{ 
        //    if (m_audiosource.clip != m_BeamSounds[0])
        //        m_audiosource.Stop();
        //}

        //if (!m_audiosource.isPlaying)
        //{ 
        //    m_audiosource.loop = false;
        //    float t_pitch = m_BeamSounds[0].length / m_animator.GetCurrentAnimatorStateInfo(0).length;
        //    m_audiosource.pitch = t_pitch;
        //    m_audiosource.clip = m_BeamSounds[0];
        //    m_audiosource.Play();
        //}

    }

    public void PlayFullPowerSounds()
    {

        if(m_fullpowerlevel == 1)
            GameAudioManager.m_Instance.PlaySound("BeamFire1", true, 1.0f,false);
        else if (m_fullpowerlevel == 2)
        {
            GameAudioManager.m_Instance.StopSound("BeamFire1");
            GameAudioManager.m_Instance.PlaySound("BeamFire2", true, 1.0f,false);
        }
        else if (m_fullpowerlevel == 3)
        {
            GameAudioManager.m_Instance.StopSound("BeamFire2");
            GameAudioManager.m_Instance.PlaySound("BeamFire3", true, 1.0f,false);
        }

        //if (!m_audiosource.loop)
        //{
        //    if (m_audiosource.isPlaying)
        //        m_audiosource.Stop();
        //    m_audiosource.loop = true;
        //    m_audiosource.pitch = 1;
        //}

        //if(m_audiosource.clip != m_BeamSounds[m_fullpowerlevel])
        //{ 
        //    m_audiosource.clip = m_BeamSounds[m_fullpowerlevel];
        //    m_audiosource.Play();
        //}
    }

    public void PlayDeactivatedSound()
    {
        if(m_animator.GetBool("Deactivated"))
        { 
            GameAudioManager.m_Instance.StopSound("BeamFire1");
            GameAudioManager.m_Instance.StopSound("BeamFire2");
            GameAudioManager.m_Instance.StopSound("BeamFire3");
            GameAudioManager.m_Instance.StopSound("BeamFireChargeUp");

            float  t_length = GameAudioManager.m_Instance.GetSoundClip("BeamFireChargeDown").length;
            GameAudioManager.m_Instance.PlaySound("BeamFireChargeDown", false, t_length / m_animator.GetCurrentAnimatorStateInfo(0).length,false);
        }

        //if (m_audiosource.isPlaying)
        //{
        //    if (m_audiosource.clip != m_BeamSounds[4])
        //        m_audiosource.Stop();
        //}

        //if (!m_audiosource.isPlaying)
        //{
        //    m_audiosource.loop = false;
        //    float t_pitch = m_BeamSounds[4].length / m_animator.GetCurrentAnimatorStateInfo(0).length;
        //    m_audiosource.pitch = t_pitch;
        //    m_audiosource.clip = m_BeamSounds[4];
        //    m_audiosource.Play();
        //}
    }

    public void StopAllSounds()
    {
        GameAudioManager.m_Instance.StopSound("BeamFire1");
        GameAudioManager.m_Instance.StopSound("BeamFire2");
        GameAudioManager.m_Instance.StopSound("BeamFire3");
        GameAudioManager.m_Instance.StopSound("BeamFireChargeUp");
        GameAudioManager.m_Instance.StopSound("BeamFireChargeDown");

    }
}
