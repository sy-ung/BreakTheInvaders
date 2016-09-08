using UnityEngine;
using System.Collections;

public class LerpingBehaviour : MonoBehaviour {

    // Use this for initialization

    private Vector2 m_newposition;
    private bool m_finishedlerping;
    private float m_lerpvalue = 0.05f;
    public bool m_FinishedLerping
    {
        get { return m_finishedlerping; }
    }

	void Start ()
    {
        enabled = false;
        m_finishedlerping = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if((Vector2)transform.position != m_newposition)
        {
            transform.position = Vector2.Lerp(transform.position,m_newposition, m_lerpvalue*=1.1f);
            m_finishedlerping = false;
        }
        else
        {
            m_finishedlerping = true;
        }
	}

    public void LerpToPosition(Vector2 p_NewPosition)
    {
        m_newposition = p_NewPosition;
        enabled = true;
    }
}
