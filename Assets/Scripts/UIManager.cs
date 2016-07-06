using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    private UIManager m_instance;

    public UIManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                if(m_instance == null)
                {
                    m_instance = (new GameObject("UIManager").AddComponent<UIManager>());
                }
            }
            return m_instance;
        }
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
