﻿using UnityEngine;
public class PlayerManager : MonoBehaviour
{


    private static PlayerManager m_instance;

    //Creating the PlayerManager singleton and returning it
    public static PlayerManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<PlayerManager>();
                if(m_instance == null)
                {
                    GameObject t_PlayerManagerObject = new GameObject("PlayerManagerSingleton");
                    m_instance = t_PlayerManagerObject.AddComponent<PlayerManager>();
                }
            }
            return m_instance;
        }
    }



    private Player m_player;
    public Player m_Player
    {
        get
        {
            if (m_player != null)
                return m_player;
            else
            {
                Debug.Log("m_player is null at get m_Player");
                return null;
            }
        }
    }


    void Awake()
    {
        
    }

    public void RespawnPlayer()
    {
        if (m_player == null)
        {
            //m_Player = GameObject.FindGameObjectWithTag("Player");
            m_player = FindObjectOfType<Player>();
            if(m_player == null)
            {
                GameObject t_PlayerObject = new GameObject("Player");
                m_player = t_PlayerObject.AddComponent<Player>();
                m_player.tag = "Player";
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
