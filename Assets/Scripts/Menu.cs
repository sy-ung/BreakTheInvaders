using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

    private PointBlank m_PointBlank;


    void Awake()
    {
        m_PointBlank = GameObject.FindGameObjectWithTag("tag_pointblank").GetComponent<PointBlank>();
    }


	//Use this for initialization
	void Start () {
        
	}

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
