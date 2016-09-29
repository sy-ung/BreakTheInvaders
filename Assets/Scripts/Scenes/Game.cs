using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

    Text DebugText;

    private int m_points = 0;

    float m_spawnenemytime = 2.0f;
    float m_spawnenemytimer = 1.0f;

    float m_gameovertextdisplaytime;
    float m_gameovertextdisplaytimer;
    float m_gameoverscenetime;

    float m_increasedifficultyscore;
    float m_difficultyinterval;

    int m_difficultylevel = 1;

    public bool m_GameOver;

    int m_numenemysquad = 4;

    Score m_score;

    GameObject m_pausemenu;

    Background m_bg;

    private static bool m_gamestarted;
    public static bool m_GameStarted
    {
        get { return m_gamestarted; }
    }


    private float m_gamestarttimer;

    // Use this for initialization
    void Start ()
    {
        InitEverything();


        m_GameOver = false;

        m_gameovertextdisplaytime = 2.0f;
        m_gameovertextdisplaytimer = 0.0f;
        m_gameoverscenetime = m_gameovertextdisplaytime + 2.5f;

        m_difficultyinterval = 3000;
        

        m_increasedifficultyscore = m_score.m_CurrentGameScore + m_difficultyinterval;

        m_gamestarted = false;

        m_gamestarttimer = 5.0f;

        GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().Resize(4);

    }

    void InitEverything()
    {
        PlayerManager.m_Instance.RespawnPlayer();


        Score t_score = FindObjectOfType<Score>();
        if (t_score == null)
            m_score = (Instantiate(AssetManager.m_Instance.GetPrefab("Score")) as GameObject).GetComponent<Score>();
        else
        {
            m_score = t_score;
            m_score.NewGame();
        }
        SetUpUI();


        GameAudioManager.m_Instance.FlushAllSoundsEffects();

        m_pausemenu = GameObject.FindGameObjectWithTag("Pause");
        m_pausemenu.SetActive(false);

        m_bg = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        m_bg.ScrollDown();
        m_bg.SetScrollingSpeed(0.5f,0.01f);
        //HidePauseMenu();
    }

    void IncreaseDifficulty()
    {
        m_spawnenemytime *= 0.9f;

        ++m_numenemysquad;

        m_difficultyinterval *= 1.25f;
        ++m_difficultylevel;
    }

    void Reset()
    {

    }

    void CountDown()
    {
        Text t_gamemsg = GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>();
        if (m_gamestarttimer <= -1)
        {
            m_gamestarted = true;
            BallManager.m_Instance.RespawnBall(new Vector2(0, 0));
            t_gamemsg.enabled = false;
        }
        else
        {
            int t_inttime = (int)(m_gamestarttimer);
            if (t_inttime <= 3)
            {

                
                t_gamemsg.fontSize = 100;

                if(t_inttime == 3)
                {
                    t_gamemsg.color = Color.green;
                }
                else if(t_inttime == 2)
                {
                    t_gamemsg.color = Color.yellow;
                }
                else if(t_inttime == 1)
                {
                    t_gamemsg.color = Color.red;
                }

                if(m_gamestarttimer<3.75f && m_gamestarttimer>1.25f)
                { 
                    m_bg.SetScrollingSpeed(20f, 0.075f);
                    Camera.main.GetComponent<CameraShake>().StartShake(0.01f, 0.35f);
                }

                t_gamemsg.text = t_inttime.ToString();

                if (t_inttime <= 0)
                {
                    t_gamemsg.color = Color.green;
                    t_gamemsg.text = "GO!";
                    

                    m_bg.SetScrollingSpeed(0.5f,0.04f);
                }

                if (!t_gamemsg.enabled)
                    t_gamemsg.enabled = true;
            }

            m_gamestarttimer -= Time.deltaTime;
        }
    }

    void SetUpUI()
    {
        UIManager.m_Instance.AddUIElementToScreen("ControlBar", UIManager.ScreenAnchor.LOWER_LEFT, new Vector2(0,0.05f));
        UIManager.m_Instance.AddUIElementToScreen("ShootButton", UIManager.ScreenAnchor.LOWER_RIGHT, new Vector2(0,0.05f));
    }

	// Update is called once per frame
	void Update ()
    {

        CheckInput();
        //MouseInput();
        TouchInput();

        if (!m_gamestarted)
        {
            CountDown();
            return;
        }

        if (m_GameOver)
        {
            GameOver();
            return;
        }

        if(m_score.m_CurrentGameScore > m_increasedifficultyscore)
        {
            IncreaseDifficulty();
            m_increasedifficultyscore = m_score.m_CurrentGameScore + m_difficultyinterval;
        }

        if(m_spawnenemytimer>m_spawnenemytime)
        {
            EnemySquad[] t_squads = FindObjectsOfType<EnemySquad>();

            if(t_squads.Length < m_numenemysquad)
            { 
                SpawnEnemies();
            }
            m_spawnenemytimer = 0;
        }
        else
        {
            m_spawnenemytimer += Time.deltaTime;
        }

    }



    void GameOver()
    {

        if (m_gameovertextdisplaytimer> m_gameovertextdisplaytime)
        {
            Text t_gamemsg = GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>();
            if (!t_gamemsg.enabled)
                t_gamemsg.enabled = true;

            t_gamemsg.fontSize = 70;
            t_gamemsg.text = "GAME OVER";
            t_gamemsg.color = Color.red;
        }
        m_gameovertextdisplaytimer += Time.deltaTime;

        if(m_gameovertextdisplaytimer > m_gameoverscenetime)
        {
            
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

    }
    void SpawnEnemies()
    {
        int m_specialchance = Random.Range(0, 70);
        if(m_specialchance == 4)
            EnemyManager.m_Instance.SpawnSpecialEnemy();
        else
        {
            EnemyManager.m_Instance.SpawnSquad(m_difficultylevel);
        }

        
    }

    void TouchInput()
    {
        if(!m_GameOver)
        {
            if(m_gamestarted)
            {  
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Ray t_raycast = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

                        if (t_rayhit)
                        {
                            if (t_rayhit.collider.gameObject.tag == "ControlBox")
                            {
                                t_rayhit.collider.gameObject.GetComponent<UIControlBar>().OnTouch(Input.GetTouch(i).position, Input.GetTouch(i).phase);
                            }

                            if(t_rayhit.collider.gameObject.tag == "ShootButton")
                            {
                                t_rayhit.collider.gameObject.GetComponent<UIShootButton>().PlayerFireWeapon(Input.GetTouch(i).phase);
                            }

                        }
                    }
                }
            }
        }
    }

    void MouseInput()
    {
        Ray t_raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D t_rayhit = Physics2D.Raycast(t_raycast.origin, t_raycast.direction, Mathf.Infinity);

        if (t_rayhit.collider != null)
        {

            if (t_rayhit.collider.gameObject.tag == "ControlBox")
            {
                if(Input.GetMouseButtonDown(0))
                { 
                    t_rayhit.collider.gameObject.GetComponent<UIControlBar>().OnTouch(Input.mousePosition,TouchPhase.Began);
                }
                else
                {
                    t_rayhit.collider.gameObject.GetComponent<UIControlBar>().OnTouch(Input.mousePosition, TouchPhase.Moved);
                }
            }

            if (t_rayhit.collider.gameObject.tag == "ShootButton")
            {
                if(m_gamestarted)
                { 
                    if (Input.GetMouseButton(0))
                    {
                        t_rayhit.collider.gameObject.GetComponent<UIShootButton>().PlayerFireWeapon(TouchPhase.Began);
                    }
                    else
                    {
                        t_rayhit.collider.gameObject.GetComponent<UIShootButton>().PlayerFireWeapon(TouchPhase.Ended);
                    }
                }
            }
        }
    }

    void CheckInput()
    {
        if(!m_GameOver)
        { 
            if (m_gamestarted)
            { 
                if (Input.GetKey(KeyCode.Space))
                {
                    PlayerManager.m_Instance.m_Player.SetMuzzleToFire(true);
                }
                if(Input.GetKeyUp(KeyCode.Space))
                {
                    PlayerManager.m_Instance.m_Player.SetMuzzleToFire(false);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(-0.5f, 0));
            }
            if(Input.GetKey(KeyCode.D))
            {
                PlayerManager.m_Instance.m_Player.MovePlayer(new Vector3(0.5f, 0));
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                PlayerManager.m_Instance.m_Player.TakeDamage(10.0f);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayerManager.m_Instance.m_Player.Heal(10.0f);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                PlayerManager.m_Instance.m_Player.ApplyMuzzlePowerUp(AssetManager.m_Instance.GetPrefab("RedBeamMuzzle"), false);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Camera.main.GetComponent<CameraShake>().SlowStop();
            }
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
        m_score.AddPoints(p_Points);
    }

    void SpawnPowerUp()
    {

        int t_powerupchoice = Random.Range(1, 4);

        if (t_powerupchoice == 1)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("BluePower"), new Vector2(0,3), Quaternion.identity);
        }

        if (t_powerupchoice == 2)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("GreenPower"),new Vector2(0,3), Quaternion.identity);
        }

        if (t_powerupchoice == 3)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("RedPower"), new Vector2(0, 3), Quaternion.identity);
        }

    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        m_pausemenu.SetActive(false);
        GameAudioManager.m_Instance.ResumeMusic();
    }


    public void ShowPauseMenu()
    {
        m_pausemenu.SetActive(true);
        GameAudioManager.m_Instance.PauseMusic();
        Time.timeScale = 0;
    }

    public void ShowHelpMenuInGame()
    {

        SceneManager.LoadScene("Help", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
