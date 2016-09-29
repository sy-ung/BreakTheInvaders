using UnityEngine;
using System.Collections;

public class LerpingBehaviour : MonoBehaviour {

    // Use this for initialization

    private Vector2 m_newposition;
    private bool m_finishedlerping;
    private float m_lerpvalue = 0.05f;

    private GameObject m_target;

	void Start ()
    {
        enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject == null)
            return;
        

        if(m_target !=null)
        {
            m_newposition = m_target.transform.position;
            if ((Vector2)transform.position != m_newposition)
            {
                transform.position = Vector2.Lerp(transform.position, m_newposition, m_lerpvalue *= 1.1f);
                transform.rotation = Quaternion.FromToRotation((Vector2)transform.position + (-Vector2.up), m_target.transform.position);
            }
        }
	}

    public void LerpToGameObject(GameObject p_Target)
    {
        m_target = p_Target;
        enabled = true;
    }
}
