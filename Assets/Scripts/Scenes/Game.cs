using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

    Text DebugText;
    Text m_pointstext;
    private int m_points = 0;
	// Use this for initialization
	void Start () {

        PlayerManager.m_Instance.RespawnPlayer();
        BallManager.m_Instance.RespawnBall(new Vector2(0,1));

        SetUpUI();
        SpawnEnemies();

        m_pointstext = GameObject.FindGameObjectWithTag("CurrentScoreText").GetComponent<Text>();

    }
	
    void SetUpUI()
    {
        //UIManager.m_Instance.AddUIElementToScreen("ControlWheel", UIManager.ScreenAnchor.LOWER_LEFT, new Vector2(0, 0));
        UIManager.m_Instance.AddUIElementToScreen("ControlBar", UIManager.ScreenAnchor.LOWER_LEFT, new Vector2(0,0.05f));
        UIManager.m_Instance.AddUIElementToScreen("ShootButton", UIManager.ScreenAnchor.LOWER_RIGHT, new Vector2(0,0.05f));
    }

	// Update is called once per frame
	void Update ()
    {
        CheckInput();

        Enemy t_enemycheck = FindObjectOfType<Enemy>();
        if (t_enemycheck == null)
        {
            SpawnEnemies();
        }
        //Debug();
    }
    void SpawnEnemies()
    {
        EnemyManager.m_Instance.SpawnEnemies(5, 5, "BlueEnemy");
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            PlayerManager.m_Instance.m_Player.m_startfiring = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            PlayerManager.m_Instance.m_Player.m_startfiring = false;
        }

        if(Input.GetKey(KeyCode.A))
        {
            PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(-0.5f, 0));
        }
        if(Input.GetKey(KeyCode.D))
        {
            PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(0.5f, 0));
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

        }
    }

    void DebugStuff()
    {
        if (Input.touchCount > 0)
        {
            DebugText.text = "Number of touches " + Input.touchCount;
        }
        else
            DebugText.text = "No touches";
    }

    public void AddPoints(int p_Points)
    {
        m_points += p_Points;
        m_pointstext.text = m_points.ToString();
    }
}
