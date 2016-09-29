using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Help : MonoBehaviour {

    // Use this for initialization
    private Button m_backbtn;

    private Sprite m_ButtonFlash;
    private Sprite m_ButtonNoFlash;

    private bool m_flash;
    private float m_flashtimer;
    private float m_flashrate;

    private Text m_reloadtext;
    Color m_newreloadtextcolor;
    private float m_reloadtextflashtimer;
    private float m_reloadtextflashrate;
    
    void Awake()
    {
        m_backbtn = GameObject.FindGameObjectWithTag("BackButton").GetComponent<Button>();
        m_reloadtext = GameObject.FindGameObjectWithTag("ReloadText").GetComponent<Text>();
        m_ButtonFlash = Resources.Load<Sprite>("Images/ButtonFlash");
        m_ButtonNoFlash = m_backbtn.image.sprite;

        m_newreloadtextcolor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);

        m_flashrate = 0.125f;
        m_reloadtextflashrate = 0.02f;

        GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().Resize(2);

    }
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update()
    {
        ButtonFlash();

        ReloadTextColorChange();
	}

    void ButtonFlash()
    {
        if (m_flashtimer > m_flashrate)
        {
            if (m_flash)
            {
                m_backbtn.image.sprite = m_ButtonFlash;
                m_flash = false;
            }
            else
            {
                m_backbtn.image.sprite = m_ButtonNoFlash;
                m_flash = true;
            }

            m_flashtimer = 0;
        }
        else
        {
            m_flashtimer += 0.016f;
        }
    }

    void ReloadTextColorChange()
    {

        if (m_reloadtextflashtimer > m_reloadtextflashrate)
        {
            if (m_reloadtext.color != m_newreloadtextcolor)
            {
                m_reloadtext.color = m_newreloadtextcolor;
            }
            else
            {
                m_newreloadtextcolor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
            }
            m_reloadtextflashtimer = 0;
        }
        else
        {
            m_reloadtextflashtimer += 0.16f;
        }
        
    }

    public void GoBack()
    {
        Game t_ingame = FindObjectOfType<Game>();
        if(t_ingame == null)
            SceneManager.LoadScene("Menu",LoadSceneMode.Single);
        else
        {
            SceneManager.UnloadScene("Help");
        }
    }
}
