using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {

    private static BackgroundManager m_instance;

    private List<Background> m_backgrounds;


    void Awake()
    {
        m_backgrounds = new List<Background>();
        DontDestroyOnLoad(gameObject);
    }

    public static BackgroundManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<BackgroundManager>();
                if(m_instance == null)
                {
                    m_instance = new GameObject("BackgroundManger").AddComponent<BackgroundManager>();
                }
            }

            return m_instance;
        }
    }

    public void ClearAllBackgrounds()
    {
        foreach(Background t_bg in m_backgrounds)
        {
            Destroy(t_bg.gameObject);
        }
        m_backgrounds.Clear();
    }

    public void AddNewBackground(string p_Name)
    {
        AddNewBackground(AssetManager.m_Instance.GetPrefab(p_Name));
    }

    public void AddNewBackground(GameObject p_NewBackgroundPrefab)
    {
        m_backgrounds.Add(Instantiate(p_NewBackgroundPrefab).GetComponent<Background>());
        m_backgrounds[0].GoIntoLayer(m_backgrounds.Count - 1);
    }

    //p_Order, zero is the the most rear background
    public Background GetBackground(int p_Order)
    {
        return m_backgrounds[m_backgrounds.Count - 1 - p_Order];
    }

	// Use this for initialization
	void Start ()
    {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
