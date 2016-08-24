﻿using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

    Text DebugText;
    Text m_pointstext;
    private int m_points = 0;

    float m_spawnenemytime = 1.0f;
    float m_spawnenemytimer = 0;

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
        if(m_spawnenemytimer>m_spawnenemytime)
        {
            //SpawnEnemies();
            m_spawnenemytimer = 0;
        }
        else
        {
            m_spawnenemytimer += Time.deltaTime;
        }

        CheckInput();
        MouseInput();

    }
    void SpawnEnemies()
    {
        //EnemyManager.m_Instance.SpawnEnemies(5, 5, "BlueEnemy)
        EnemyManager.m_Instance.SpawnSquad();
    }

    void TouchInput()
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

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(PlayerManager.m_Instance.m_Player != null)
                PlayerManager.m_Instance.m_Player.TakeDamage(10.0f);
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            EnemyManager.m_Instance.SpawnSpecialEnemy();
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
