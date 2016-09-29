using UnityEngine;
using System.Collections;

public class PowerUpHighlight : MonoBehaviour {

    // Use this for initialization

    float m_highlighttimer;
    float m_highlightrate;

    Color m_flashcolor;

    void Awake()
    {
        m_highlightrate = 0.05f;
    }

	void Start ()
    {
	
	}
	
    public void SetHighlightColor(Color p_NewColor)
    {
        m_flashcolor = p_NewColor;
    }


	// Update is called once per frame
	void Update ()
    {
        if (m_highlighttimer > m_highlightrate)
        {
            if (GetComponent<SpriteRenderer>().color != m_flashcolor)
                GetComponent<SpriteRenderer>().color = m_flashcolor;
            else
                GetComponent<SpriteRenderer>().color = Color.white;

            m_highlighttimer = 0;
        }
        else
            m_highlighttimer += Time.deltaTime;

	}
}
