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

    private Vector2 m_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

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

    public void AddUIElementToScreen(string p_UIElementPrefabName, ScreenAnchor p_ScreenAnchor)
    {

        UIElement t_NewElement = Instantiate(AssetManager.m_Instance.GetPrefab(p_UIElementPrefabName)).GetComponent<UIElement>();
        Vector2 t_offset = t_NewElement.m_HalfSize;
        switch (p_ScreenAnchor)
        {
            case ScreenAnchor.TOP_LEFT:
                t_NewElement.transform.position = new Vector2(-m_screensize.x,m_screensize.y) + new Vector2(t_offset.x,-t_offset.y);
                break;
            case ScreenAnchor.TOP_CENTER:
                t_NewElement.transform.position = new Vector2(0, m_screensize.y) + new Vector2(0, -t_offset.y);
                break;
            case ScreenAnchor.TOP_RIGHT:
                t_NewElement.transform.position = new Vector2(m_screensize.x, m_screensize.y) + new Vector2(-t_offset.x, -t_offset.y);
                break;

            case ScreenAnchor.CENTER_LEFT:
                t_NewElement.transform.position = new Vector2(-m_screensize.x, 0) + new Vector2(t_offset.x, 0);
                break;
            case ScreenAnchor.CENTER:
                t_NewElement.transform.position = new Vector2(0, 0);
                break;
            case ScreenAnchor.CENTER_RIGHT:
                t_NewElement.transform.position = new Vector2(m_screensize.x, 0) + new Vector2(-t_offset.x, 0);
                break;

            case ScreenAnchor.LOWER_LEFT:
                t_NewElement.transform.position = new Vector2(-m_screensize.x, -m_screensize.y) + new Vector2(t_offset.x, +t_offset.y);
                break;
            case ScreenAnchor.LOWER_CENTER:
                t_NewElement.transform.position = new Vector2(0, -m_screensize.y) + new Vector2(0, +t_offset.y);
                break;
            case ScreenAnchor.LOWER_RIGHT:
                t_NewElement.transform.position = new Vector2(m_screensize.x, -m_screensize.y) + new Vector2(-t_offset.x, t_offset.y);
                break;

            default:
                break;
        }
        
    }


    public void RemoveUIElementFromScreen(string p_UIElementPrefabName)
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
