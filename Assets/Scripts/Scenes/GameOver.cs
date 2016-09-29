using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // Use this for initialization

    Score m_score;

    bool m_cleardoublecheck;

    void Awake()
    {
        m_score = FindObjectOfType<Score>();
        m_cleardoublecheck = false;
    }

	void Start ()
    {

        m_score.ShowTopTen();
        m_score.SaveScoreToFile();
        m_score.FindRank();

        GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().Resize(2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAgainClicked()
    {
        m_score.ResetCurrentScore();
        //m_score.ReadScoreFromFile();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void ClearScore()
    {
        Button t_btntext = GameObject.FindGameObjectWithTag("Clear").GetComponent<Button>();
        if(!m_cleardoublecheck)
        {
            m_cleardoublecheck = true;
            t_btntext.image.color = Color.red;
            t_btntext.GetComponentInChildren<Text>().text = "Sure?";
        }
        else
        {
            m_score.ClearAll();
            m_score.SaveScoreToFile();
            t_btntext.image.color = Color.white;
            t_btntext.GetComponentInChildren<Text>().text = "Clear";
            m_cleardoublecheck = false;
        }
    }
    
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
