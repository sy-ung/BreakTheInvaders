using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    private Color m_CurrentHealthColor;
    private Color m_Green = Color.green;
    private Color m_Orange = new Color32(255, 153, 51, 255);
    private Color m_Red = Color.red;

    private SpriteRenderer m_spriterenderer;

    private float m_currentpercent;
    private float m_newpercent;

    private float m_healthdisplaytime;
    private float m_healthdisplaytimer;
    private bool m_showhealth;

    private float m_health;

    private GameObject m_target;
    private GameObject m_Box;
    private Transform m_ScalePointTransform;

    private float m_damageflashtimer;
    private float m_damageflashtime;

    private float m_damageflashlength;
    private float m_damageflashlengthtimer;

    private bool m_damageflashred;
    private bool m_damageflashing;


    private float m_healflashtimer;
    private float m_healflashtime;

    private float m_healflashlength;
    private float m_healflashlengthtimer;

    private bool m_healflashgreen;
    private bool m_healflashing;

    private float m_healsoundlength;

    private bool m_alive;
    public bool m_Alive
    {
        get { return m_alive; }
    }

    void Awake()
    {
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_alive = true;
        Initialize();
    }

    public void Initialize()
    {
        m_health = 100;
        m_currentpercent = m_health / 100.0f;
        m_healthdisplaytime = 2.0f;
        m_Box = transform.parent.parent.gameObject;

        m_ScalePointTransform = transform.parent.transform;
        m_ScalePointTransform.localPosition = new Vector2(-m_spriterenderer.sprite.bounds.size.x / 2, 0);

        transform.localPosition = new Vector2(m_spriterenderer.sprite.bounds.size.x / 2, 0);

        m_damageflashtime = m_healflashtime = 0.02f;
        m_damageflashlength = m_healflashlength = 0.25f;


        m_spriterenderer.color = m_Green;
        m_CurrentHealthColor = m_spriterenderer.color;

        m_healsoundlength = GameAudioManager.m_Instance.GetSoundClip("PlayerHeal").length;

        HideHealth();
    }

    public void SetTargetTransform(Transform p_TargetTransform)
    {


        m_Box.transform.SetParent(p_TargetTransform);
        m_Box.transform.localScale = new Vector2(1, 1);
        float t_scalefactor = p_TargetTransform.GetComponent<SpriteRenderer>().bounds.size.x / m_Box.GetComponent<SpriteRenderer>().bounds.size.x;
        m_Box.transform.localScale *= t_scalefactor;

        m_target = p_TargetTransform.gameObject;

    }

    public void ResizeToFit()
    {
        

    }

    // Update is called once per frame
    void Update ()
    {
        if (!m_alive)
            return;

        m_newpercent = m_health / 100.0f;

        if (m_currentpercent < 0.7f)
        {
            ShowHealth();
        }

        if (m_currentpercent != m_newpercent)
        {
            m_currentpercent = Mathf.LerpUnclamped(m_currentpercent, m_newpercent, 0.25f);

            m_ScalePointTransform.localScale = new Vector2(m_currentpercent, 1);
        }

        if (!m_damageflashing && !m_healflashing)
        {
            ColorCheck();
        }

        if (m_damageflashing)
            DamageFlash();
        else if (m_healflashing)
            HealFlash();

        if (m_health <= 0)
        {
            m_alive = false;
        }


                

        if (m_showhealth)
        {
            m_healthdisplaytimer += Time.deltaTime;
            if (m_healthdisplaytimer > m_healthdisplaytime)
            {
                HideHealth();
                m_healthdisplaytimer = 0;
            }
        }


    }

    void LateUpdate()
    {

    }

    void DamageFlash()
    {
        if(m_damageflashlengthtimer < m_damageflashlength)
        { 
            if(m_damageflashtimer>m_damageflashtime)
            {
                if(m_damageflashred)
                {
                    if (m_spriterenderer.color == m_Red)
                        m_spriterenderer.color = m_Orange;
                    else
                        m_spriterenderer.color = m_Red;

                    m_damageflashred = false;
                }
                else
                {
                    m_spriterenderer.color = m_CurrentHealthColor;
                    m_damageflashred = true;
                }
                m_damageflashtimer = 0;
            }
            else
            {
                m_damageflashtimer += Time.deltaTime;
            }
            m_damageflashlengthtimer += Time.deltaTime;
        }
        else
        {
            m_damageflashlengthtimer = 0;
            m_damageflashing = false;
            m_spriterenderer.color = m_CurrentHealthColor;
        }
        
    }

    void HealFlash()
    {
        if (m_healflashlengthtimer < m_healflashlength)
        {
            if (m_healflashtimer > m_healflashtime)
            {
                if (m_healflashgreen)
                {
                    if (m_spriterenderer.color == m_Green)
                        m_spriterenderer.color = Color.blue;
                    else
                        m_spriterenderer.color = m_Green;

                    m_healflashgreen = false;
                }
                else
                {
                    m_spriterenderer.color = m_CurrentHealthColor;
                    m_healflashgreen = true;
                }
                m_healflashtimer = 0;
            }
            else
            {
                m_healflashtimer += Time.deltaTime;
            }
            m_healflashlengthtimer += Time.deltaTime;
        }
        else
        {
            m_healflashlengthtimer = 0;
            m_healflashing = false;
            m_spriterenderer.color = m_CurrentHealthColor;
        }

    }

    public void TakeDamage(float p_DamageAmount)
    {
        if (m_health - p_DamageAmount <= 0)
            m_health = 0;
        else
            m_health -= p_DamageAmount;

        m_damageflashing = true;

        //Reset heal flashing if inturrpted by damage
        if(m_healflashing)
        {
            m_healflashing = false;
            m_healflashgreen = false;
            m_healflashlengthtimer = 0;
            m_healflashtimer = 0;
        }

        ShowHealth();
        GameAudioManager.m_Instance.PlaySound("PlayerHit", false, 1, 1.0f);
    }

    public void Heal(float p_HealAmount)
    {
        if (m_health + p_HealAmount >= 100)
            m_health = 100;
        else
            m_health += p_HealAmount;

        m_healflashing = true;

        //Reset damage flashing if inturrupted by healing
        if(m_damageflashing)
        {
            m_damageflashing = false;
            m_damageflashlengthtimer = 0;
            m_damageflashtimer = 0;
            m_damageflashred = false;
        }

        ShowHealth();
        GameAudioManager.m_Instance.PlaySound("PlayerHeal", false, m_healsoundlength  / m_healflashlength, 1.0f);
    }

    void HideHealth()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        m_Box.GetComponent<SpriteRenderer>().enabled = false;
        m_showhealth = false;
    }

    public void ShowHealth()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        m_Box.GetComponent<SpriteRenderer>().enabled = true;
        m_showhealth = true;
        m_healthdisplaytimer = 0;
    }

    void ColorCheck()
    {

        if (m_newpercent <= 1.0f && m_newpercent >= 0.7f)
        {
            if (m_spriterenderer.color != m_Green)
                m_spriterenderer.color = m_Green;
        }

        if(m_newpercent < 0.7f && m_newpercent >= 0.4f)
        {
            if (m_spriterenderer.color != m_Orange)
                m_spriterenderer.color = m_Orange;
        }

        if(m_newpercent < 0.4f)
        {
            if (m_spriterenderer.color != m_Red)
                m_spriterenderer.color = m_Red;
        }
        m_CurrentHealthColor = m_spriterenderer.color;
    }

    public void Death()
    {
        Destroy(m_Box);
    }

}
