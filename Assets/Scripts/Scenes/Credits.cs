using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {

    Text m_namecredittext;

    float m_letterrate;
    float m_lettertimer;

    string m_msg;
    int m_maxmsglettercount;
    int m_msglettercount;

    bool m_finishedspelling;

    Image m_portrait;
    Text m_emailtext;


    float m_portraitdelay;
    float m_portraittimer;

    float m_textcolorflashtimer;
    float m_textcolorflashrate;

    float m_finalmessagetimer;
    float m_finalmessagedelay;
    bool m_finaldisplayed;

    Button m_menubtn;

    void Awake()
    {
        m_letterrate = 0.1f;
        m_namecredittext = GameObject.FindGameObjectWithTag("CreditsText").GetComponent<Text>();
        m_msg = "Created By: Sonny Ung";

        m_maxmsglettercount = m_msg.Length;

        m_namecredittext.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        m_finishedspelling = false;

        m_portraitdelay = 2.0f;

        m_portrait = GameObject.FindGameObjectWithTag("Portrait").GetComponent<Image>();
        m_portrait.enabled = false;

        m_emailtext = GameObject.FindGameObjectWithTag("Email").GetComponent<Text>();
        m_emailtext.enabled = false;

        m_textcolorflashrate = 0.1f;

        m_finalmessagedelay = 5.0f;

        m_menubtn = GameObject.FindGameObjectWithTag("BackButton").GetComponent<Button>();
        m_menubtn.image.enabled = false;
        m_menubtn.GetComponentInChildren<Text>().enabled = false;
    }

	// Use this for initialization
	void Start ()
    {

    }

    void SpellItOut()
    {
        if (m_msglettercount < m_maxmsglettercount)
        {
            if (m_lettertimer > m_letterrate)
            {
                m_msglettercount++;
                m_namecredittext.text = m_msg.Substring(0, m_msglettercount);
                m_lettertimer = 0;

            }
            else
                m_lettertimer += Time.deltaTime;
        }
        else
        { 
            m_finishedspelling = true;
        }
    }

    void ShowPicture()
    {
        if (m_portraittimer > m_portraitdelay)
        {
            m_portrait.enabled = true;
            m_emailtext.enabled = true;
            m_namecredittext.alignment = TextAnchor.UpperCenter;
        }
        else
            m_portraittimer += Time.deltaTime;
    }
	

    void TextColorFlash()
    {
        if(m_textcolorflashtimer>m_textcolorflashrate)
        {
            //Color t_newcolor = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);

            if (m_namecredittext.color != Color.green)
                m_namecredittext.color = Color.green;
            else if(m_namecredittext.color != Color.blue)
                m_namecredittext.color = Color.blue;
            m_textcolorflashtimer = 0;
        }
        else
        {
            m_textcolorflashtimer += Time.deltaTime;
        }
    }

    void FinalMessage()
    {
        if(m_finalmessagetimer> m_finalmessagedelay)
        { 
            m_namecredittext.enabled = false;
            m_emailtext.text = "Thanks for playing!";
            m_emailtext.alignment = TextAnchor.MiddleCenter;
            m_portrait.sprite = Resources.Load<Sprite>("Images/Thanks");
            m_finaldisplayed = true;

            m_menubtn.image.enabled = true;
            m_menubtn.GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            m_finalmessagetimer += Time.deltaTime;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu",LoadSceneMode.Single);
    }

	// Update is called once per frame
	void Update ()
    {
        if(!m_finishedspelling)
            SpellItOut();
        else
        {
            if(!m_portrait.enabled)
            {
                ShowPicture();
            }
            else
            {
                if(!m_finaldisplayed)
                { 
                    TextColorFlash();
                    FinalMessage();
                }

            }
        }
	}



}
