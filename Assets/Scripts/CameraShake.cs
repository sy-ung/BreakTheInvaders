using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    Camera m_main;

    float m_shaketimer;
    float m_shakelength;

    bool m_shaking;
    public bool m_Shaking
    {
        get { return m_shaking; }
    }

    float m_shakestrength;

    bool m_slowstop;

    Vector3 m_origin;

    void Awake()
    {
        m_main = Camera.main;
        m_origin = transform.position;
    }

    public void StartShake(float p_Length, float p_ShakeStrength)
    {
        m_shakestrength = p_ShakeStrength;
        m_shakelength = p_Length;
        m_shaking = true;
    }

    public void SlowStop()
    {
        m_slowstop = true;
        m_shakestrength = Mathf.Lerp(m_shakestrength, 0, 0.05f);
        if (m_shakestrength <= 0.006)
        {
            InstantStop();
        }
    }
    public void IntensifyShake(float p_DeltaTime,float p_DeltaShakeStrength)
    {
        m_shakelength += p_DeltaTime;
        IntensifyShake(p_DeltaShakeStrength);
    }

    public void IntensifyShake(float p_DeltaShakeStrength)
    {
        m_shakestrength += p_DeltaShakeStrength;
    }

    public void InstantStop()
    {
        m_shaking = false;
        m_shaketimer = 0;
        transform.position = m_origin;
        m_shakestrength = 0;
        m_slowstop = false;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(m_slowstop)
        {
            SlowStop();
        }

        if(m_shaking)
            Shake();

        
	}

    void Shake()
    {
        if (m_shaketimer < m_shakelength)
        {
            m_shaketimer += Time.deltaTime;
        }
        else
        {
            if(!m_slowstop)
                SlowStop();
        }
        if(Time.timeScale != 0)
        { 
            Vector3 t_movement = ((Vector3)Random.insideUnitCircle * m_shakestrength) + m_origin;
            transform.position = t_movement;
        }

    }
}
