using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public enum ScreenAnchor
    {
        TOP_LEFT,       TOP_CENTER,     TOP_RIGHT,
        CENTER_LEFT,    CENTER,         CENTER_RIGHT,
        LOWER_LEFT,     LOWER_CENTER,   LOWER_RIGHT
    };

    private static UIManager m_instance;

    private Hashtable m_uielements;

    private Vector2 m_screensize = new Vector2(Screen.width, Screen.height);


    public static UIManager m_Instance
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

    public void AddUIElementToScreen(string p_UIElementPrefabName, Vector2 p_Position)
    {
        Instantiate(AssetManager.m_Instance.GetPrefab(p_UIElementPrefabName)).transform.position = p_Position;
    }
    
    //p_OffsetPrecent range is from 0.0 - 1.0 for 0 - 100 percent offset
    public void AddUIElementToScreen(string p_UIElementPrefabName, ScreenAnchor p_ScreenAnchor, Vector2 p_OffsetPercent)
    {

        UIElement t_NewElement = Instantiate(AssetManager.m_Instance.GetPrefab(p_UIElementPrefabName)).GetComponent<UIElement>();
        Vector2 t_sizeoffset = t_NewElement.m_HalfSize;
        Vector2 t_finalposition;
        Vector2 t_Offset = new Vector2(Screen.width * p_OffsetPercent.x,Screen.height * p_OffsetPercent.y );

        switch (p_ScreenAnchor)
        {
            case ScreenAnchor.TOP_LEFT:
                t_finalposition = Camera.main.ScreenToWorldPoint( new Vector2(0, m_screensize.y) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(t_sizeoffset.x,-t_sizeoffset.y);
                break;
            case ScreenAnchor.TOP_CENTER:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x/2, m_screensize.y) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(0, -t_sizeoffset.y);
                break;
            case ScreenAnchor.TOP_RIGHT:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x, m_screensize.y) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(-t_sizeoffset.x, -t_sizeoffset.y);
                break;

            case ScreenAnchor.CENTER_LEFT:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(0, m_screensize.y/2) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(t_sizeoffset.x, 0);
                break;
            case ScreenAnchor.CENTER:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x/2, m_screensize.y/2) + t_Offset);
                t_NewElement.transform.position = t_finalposition;
                break;
            case ScreenAnchor.CENTER_RIGHT:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x, m_screensize.y/2) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(-t_sizeoffset.x, 0);
                break;

            case ScreenAnchor.LOWER_LEFT:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(0, 0) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(t_sizeoffset.x, t_sizeoffset.y);
                break;
            case ScreenAnchor.LOWER_CENTER:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x/2, 0) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(0, t_sizeoffset.y);
                break;
            case ScreenAnchor.LOWER_RIGHT:
                t_finalposition = Camera.main.ScreenToWorldPoint(new Vector2(m_screensize.x, 0) + t_Offset);
                t_NewElement.transform.position = t_finalposition + new Vector2(-t_sizeoffset.x, t_sizeoffset.y);
                break;
            default:
                break;
        }
        
    }


    public void RemoveUIElementFromScreen(string p_UIElementPrefabName)
    {

    }

	// Use this for initialization
	void Start ()
    {
	    
	}

	// Update is called once per frame
	void Update () {
	
	}
}
