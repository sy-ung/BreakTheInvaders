using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // Use this for initialization

    Score m_score;
    
    void Awake()
    {
        m_score = FindObjectOfType<Score>();

    }

	void Start ()
    {

        m_score.ShowTopTen();
        m_score.SaveScoreToFile();
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
}
