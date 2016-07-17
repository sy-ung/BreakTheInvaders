using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

    Text DebugText;


	// Use this for initialization
	void Start () {

        PlayerManager.m_Instance.RespawnPlayer();
        BallManager.m_Instance.RespawnBall(new Vector2(0,1));

        DebugText = GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>();

        SetUpUI();
        SpawnEnemies();
    }
	
    void SetUpUI()
    {
        //UIManager.m_Instance.AddUIElementToScreen("ControlWheel", UIManager.ScreenAnchor.LOWER_LEFT, new Vector2(0, 0));
        UIManager.m_Instance.AddUIElementToScreen("ControlBar", UIManager.ScreenAnchor.LOWER_CENTER, new Vector2(0, 100));
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
        EnemyManager.m_Instance.SpawnEnemies(5, 5, "GreenEnemy");
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Player t_player = PlayerManager.m_Instance.m_Player;
            if (t_player.m_CurrentControlMode == Player.ControlMode.BATTING)
            {
                t_player.m_CurrentControlMode = Player.ControlMode.DRAG;
            }
            else
            {
                t_player.m_CurrentControlMode = Player.ControlMode.BATTING;
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

        }
    }

    void Debug()
    {
        if (Input.touchCount > 0)
        {
            DebugText.text = "Number of touches " + Input.touchCount;
        }
        else
            DebugText.text = "No touches";
    }
}
