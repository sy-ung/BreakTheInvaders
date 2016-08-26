using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Score : MonoBehaviour {

    // Use this for initialization


    Text m_pointstext;

    Text m_hiscoretext;
    Text m_hiscorestext;

    Text m_ranktext;

    Text m_currentscoretext;

    ScoreData m_scoredata;

   

    int m_currentgamescore;
    int m_currentplace;

    string m_savepath;

    float m_rankflashrate = 0.5f;
    float m_rankflashtimer = 0;
    bool m_flashoOn;

    void Awake()
    {
        m_savepath = "Assets/Resources/SavedData/scores.dat";
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        Initialize();
        Debug.Log("NEW GAME");
    }
    void Initialize()
    {
        m_flashoOn = false;
        m_pointstext = GameObject.FindGameObjectWithTag("CurrentScoreText").GetComponent<Text>();
        gameObject.tag = "Score";
        ReadScoreFromFile();
        m_hiscoretext = GameObject.FindGameObjectWithTag("HiScoreText").GetComponent<Text>();
        m_hiscoretext.text = m_scoredata.m_TopTen[0].ToString();
    }


    void Start ()
    {
        NewGame();
    }
	
    public void AddPoints(int p_Points)
    {
        m_currentgamescore += p_Points;
        m_pointstext.text = m_currentgamescore.ToString();
    }

    public int GetLastGameScore()
    {
        return m_currentgamescore;
    }



    public void ResetCurrentScore()
    {
        m_currentgamescore = 0;
    }

    public void SaveScoreToFile()
    {
        BinaryFormatter t_bf = new BinaryFormatter();

        if(File.Exists(m_savepath))
        {
            File.Delete(m_savepath);
        }

        FileStream t_file = File.Create(m_savepath);
        t_bf.Serialize(t_file, m_scoredata);
        t_file.Close();
    }

    public void ReadScoreFromFile()
    {

        if(File.Exists(m_savepath))
        {
            BinaryFormatter t_bf = new BinaryFormatter();
            FileStream t_file = File.Open(m_savepath, FileMode.Open);

            m_scoredata = (ScoreData)t_bf.Deserialize(t_file);

            t_file.Close();

        }
        else
        {
            m_scoredata = new ScoreData();
            m_scoredata.m_TopTen = new int[10];
            for(int i = 0;i<m_scoredata.m_TopTen.Length;i++)
            {
                m_scoredata.m_TopTen[i] = 0;
            }
        }
    }

    public void ShowTopTen()
    {
        ShowCurrentScore();
        m_hiscorestext = GameObject.FindGameObjectWithTag("HighScores").GetComponent<Text>();

        SortTopTen();

        Vector2 t_startPosition = m_hiscorestext.transform.position;
        Font t_font = Resources.Load<Font>("Font/VCR_OSD_MONO_1.001");
        for (int i = 0;i < m_scoredata.m_TopTen.Length; i++)
        {
            Text t_newscore = new GameObject(i + " place").AddComponent<Text>();
            t_newscore.transform.SetParent(m_hiscorestext.gameObject.transform);
            t_newscore.font = t_font;
            t_newscore.fontSize = 25;

            t_newscore.rectTransform.sizeDelta = new Vector2(100, t_newscore.fontSize);
            t_newscore.rectTransform.anchoredPosition = new Vector2(0, -m_hiscorestext.rectTransform.rect.height + ( i * -t_newscore.rectTransform.rect.height));
            t_newscore.alignment = TextAnchor.MiddleCenter;

            t_newscore.text = m_scoredata.m_TopTen[i].ToString();
        }
        FindRank();
    }

    void ShowCurrentScore()
    {
        if (m_currentscoretext == null)
            m_currentscoretext = GameObject.FindGameObjectWithTag("FinalScoreText").GetComponent<Text>();

        m_currentscoretext.text = m_currentgamescore.ToString();
    }

    void SortTopTen()
    {
        m_currentplace = -1;

        for(int i = m_scoredata.m_TopTen.Length - 1; i >= 0; i--)
        {
            if(i == m_scoredata.m_TopTen.Length - 1)
            {
                if (m_currentgamescore > m_scoredata.m_TopTen[i])
                { 
                    m_scoredata.m_TopTen[i] = m_currentgamescore;
                    m_currentplace = i;
                }
            }
            else if(m_currentgamescore > m_scoredata.m_TopTen[i])
            {
                m_scoredata.m_TopTen[i + 1] = m_scoredata.m_TopTen[i]; 
                m_scoredata.m_TopTen[i] = m_currentgamescore;
                m_currentplace = i;
            }
        }
    }

    void FindRank()
    {
        m_ranktext = null;
        if (m_currentplace != -1)
        {

            Text[] t_childrentext = m_hiscorestext.GetComponentsInChildren<Text>();
            m_ranktext = t_childrentext[m_currentplace+1];
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            FlashCurrentScore();
        }
	}
    void FlashCurrentScore()
    {
        if(m_currentplace != -1)
        {
            if(m_rankflashtimer>m_rankflashrate)
            {
                if (m_flashoOn)
                    m_flashoOn = false;
                else if (!m_flashoOn)
                    m_flashoOn = true;
                m_rankflashtimer = 0;


                if (m_flashoOn)
                {
                    m_currentscoretext.color = Color.red;
                    m_ranktext.color = Color.red;
                }
                else
                {
                    m_currentscoretext.color = Color.white;
                    m_ranktext.color = Color.white;
                }
            }
            else
            {
                m_rankflashtimer += Time.deltaTime;
            }


        }
    }
}

[Serializable]
class ScoreData
{
    public int[] m_TopTen;
}
