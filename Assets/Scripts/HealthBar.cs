using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    private Player m_player;

    public Sprite m_GreenHealth;
    public Sprite m_OrangeHealth;
    public Sprite m_RedHealth;

    private SpriteRenderer m_spriterenderer;

    private float m_currentpercent;
    private float m_newpercent;

    void Awake()
    {
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_player = PlayerManager.m_Instance.m_Player;
        m_currentpercent = m_player.m_Health / 100.0f;
        transform.parent.parent.localScale /= 10.0f;
    }


	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        m_newpercent = m_player.m_Health / 100.0f;

        if (m_currentpercent != m_newpercent)
        {
            m_currentpercent = Mathf.Lerp(m_currentpercent, m_newpercent, 0.25f);
            ColorCheck();
            transform.parent.transform.localScale = new Vector2(m_currentpercent, 1);
        }

        transform.parent.parent.position = new Vector2(m_player.transform.position.x, transform.parent.parent.transform.position.y);
    }

    void LateUpdate()
    {

    }

    public void HideHealth()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.parent.parent.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ShowHealth()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.parent.GetComponent<SpriteRenderer>().enabled = true;
    }

    void ColorCheck()
    {
        if(m_currentpercent >= 1.0f && m_currentpercent > 0.7f)
        {
            if (m_spriterenderer.sprite != m_GreenHealth)
                m_spriterenderer.sprite = m_GreenHealth;
        }

        if(m_currentpercent < 0.7f && m_currentpercent> 0.4f)
        {
            if (m_spriterenderer.sprite != m_OrangeHealth)
                m_spriterenderer.sprite = m_OrangeHealth;
        }

        if(m_currentpercent < 0.4f)
        {
            if (m_spriterenderer.sprite != m_RedHealth)
                m_spriterenderer.sprite = m_RedHealth;
        }
    }

    public void Death()
    {
        Destroy(transform.parent.parent.gameObject);
    }

}
