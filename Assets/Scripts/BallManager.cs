﻿using UnityEngine;

public class BallManager : MonoBehaviour {

    private static BallManager m_instance;

    private float m_poweruptimer;
    private float m_powerupduration;
    private bool m_powerupactivated;

    private GameObject m_defaultball;

    private bool m_ballpowerupoverwrite;
    public bool m_BallPowerupOverwrite
    {
        get { return m_ballpowerupoverwrite; }
    }

    //Creating the BallManager singleton and returning it
    public static BallManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<BallManager>();
                if(m_instance == null)
                {
                    GameObject t_BallManagerObject = new GameObject("BallManagerSingleton");
                    m_instance = t_BallManagerObject.AddComponent<BallManager>();
                }
            }
            return m_instance;
        }
    }

    private Ball m_playerball;
    public Ball m_PlayerBall
    {
        get
        {
            if (m_playerball != null)
                return m_playerball;
            else
            {
                return null;
            }
        }
    }

    void Awake()
    {
        m_powerupduration = 7f;
        m_defaultball = AssetManager.m_Instance.GetPrefab("DefaultBall");
        m_ballpowerupoverwrite = true;
    }



    public void RespawnBall(Vector2 p_Position)
    {

        if(m_playerball == null)
        {
            m_playerball = FindObjectOfType<Ball>();
            if(m_playerball == null)
            {
                m_playerball = Instantiate(m_defaultball).GetComponent<Ball>();
                m_playerball.StartLaunch();
            }
        }

        m_playerball.transform.position = new Vector3(p_Position.x, p_Position.y, 0);
    }

    public void ApplyPowerUp(GameObject p_NewBall, bool p_Overwritable)
    {
        m_ballpowerupoverwrite = p_Overwritable;
        ApplyPowerUp(p_NewBall);
    }

    public void ApplyPowerUp(GameObject p_NewBall)
    {
        ChangeBall(p_NewBall);
        m_powerupactivated = true;
        m_poweruptimer = 0;
    }

    void ChangeBall(GameObject p_NewBall)
    {
        if (m_playerball != null)
        {
            GameObject t_newball = Instantiate(p_NewBall, m_playerball.transform.position, m_playerball.transform.rotation) as GameObject;

            Vector2 t_storedVelocity = m_playerball.m_CurrentVelocity;

            m_playerball.DestroyBall();

            m_playerball = t_newball.GetComponent<Ball>();
            m_playerball.ChangeVelocity(t_storedVelocity);

        }
    }

    
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_powerupactivated)
        {
            m_poweruptimer += Time.deltaTime;

            if(m_poweruptimer>m_powerupduration)
            {
                ChangeBall(m_defaultball);
                m_powerupactivated = false;
                m_poweruptimer = 0;
                m_ballpowerupoverwrite = true;
            }
        }
	}
}
