using UnityEngine;
using System.Collections;

public class ResolutionFix : MonoBehaviour {

    Vector2 m_screensizeworldpoint;

    public Vector2 m_ScreenSizeWorldPoint
    {
        get
        {
            return m_screensizeworldpoint;
        }
    }


    // Use this for initialization
    void Awake()
    {
        m_screensizeworldpoint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //Debug.Log(Camera.main.WorldToViewportPoint(new Vector2(0,0)));
        //Debug.Log("Camera Scale " + m_cameraviewscale);
        //Debug.Log("Width " + Screen.currentResolution.width);
        //Debug.Log("Height " + Screen.currentResolution.height);
        
    }
    void Start () {
        
    
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShakeCamera()
    {

    }
}
