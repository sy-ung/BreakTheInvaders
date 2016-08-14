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
        //SpawnEnemies();

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
            //SpawnEnemies();
        }
        //Debug();
    }
    void SpawnEnemies()
    {
        //EnemyManager.m_Instance.SpawnEnemies(5, 5, "BlueEnemy)
        EnemyManager.m_Instance.SpawnSquad();
    }

    void CheckInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            PlayerManager.m_Instance.m_Player.SetMuzzleToFire(true);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            PlayerManager.m_Instance.m_Player.SetMuzzleToFire(false);
        }

        if(Input.GetKey(KeyCode.A))
        {
            PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(-0.5f, 0));
        }
        if(Input.GetKey(KeyCode.D))
        {
            PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(0.5f, 0));
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemies();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            EnemySquad[] t_squads = FindObjectsOfType<EnemySquad>();
            for(int i = 0; i < t_squads.Length ;i++)
            {
                t_squads[i].KillAllInSquad();
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerManager.m_Instance.ChangeMuzzle(AssetManager.m_Instance.GetPrefab("BasicMuzzle"));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerManager.m_Instance.ChangeMuzzle(AssetManager.m_Instance.GetPrefab("RedBeamMuzzle"));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerManager.m_Instance.ChangeMuzzle(AssetManager.m_Instance.GetPrefab("BlueMuzzle"));
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerManager.m_Instance.ChangeMuzzle(AssetManager.m_Instance.GetPrefab("GreenMuzzle"));
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
