using UnityEngine;
using System.Collections;

public class Infinity : MonoBehaviour {

    SpriteRenderer m_spriterenderer;

    Color m_transparent;
    Color m_opaque;

    bool m_show;
    float m_flashspeed;

    void Awake()
    {
        m_spriterenderer = GetComponent<SpriteRenderer>();
        m_transparent = new Color(0, 0, 1, 0.8f);
        m_opaque = new Color(0f, 1, 1, 1);
    }

	// Use this for initialization
	void Start ()
    {
        m_spriterenderer.color = m_opaque;
        m_flashspeed = 0.7f;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(m_show)
        { 
	        if(m_spriterenderer.color != m_opaque)
            {
                m_spriterenderer.color = Color.Lerp(m_spriterenderer.color, m_opaque, m_flashspeed);
            }
            else
            {
                m_show = false;
            }
        }

        if(!m_show)
        {
            if(m_spriterenderer.color != m_transparent)
            {
                m_spriterenderer.color = Color.Lerp(m_spriterenderer.color, m_transparent, m_flashspeed);
            }
            else
            {
                m_show = true;
            }
        }
    }
}
