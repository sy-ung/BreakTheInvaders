using UnityEngine;
using System.Collections;

public class NoAmmo : MonoBehaviour {

    SpriteRenderer m_spriterenderer;
    public Ammo m_ammoref;

    private float m_flashrate;
    private float m_flashtimer;
    private bool m_flashred;

    void Awake()
    {
        m_spriterenderer = GetComponent<SpriteRenderer>();
        m_spriterenderer.sortingLayerName = "UI";
        m_spriterenderer.sortingOrder = 1;
        m_flashrate = 0.075f;

    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        Flash();
	}
    
    void Flash()
    {
        if(m_flashtimer> m_flashrate)
        {
            if (m_flashred)
            { 
                m_spriterenderer.color = Color.white;
                m_flashred = false;
            }
            else
            { 
                m_spriterenderer.color = Color.black;
                m_flashred = true;
            }
            m_flashtimer = 0;
        }
        else
        {
            m_flashtimer += Time.deltaTime;
        }
    }

    
}
