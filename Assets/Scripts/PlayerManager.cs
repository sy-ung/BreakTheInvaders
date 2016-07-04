using UnityEngine;
using System.Collections;

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


    void Awake()
    {
        
    }

    public void RespawnPlayer(Vector2 p_NewPosition)
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

        m_player.transform.position = new Vector3(p_NewPosition.x, p_NewPosition.y, 0);
        
    }

    public void MovePlayer(Vector2 p_DeltaPosition)
    {
        if (m_player == null)
        { 
            RespawnPlayer(p_DeltaPosition);
            Debug.Log("m_Player was null at PlayerManger.MovePlayer");
        }

        m_player.MovePlayer(p_DeltaPosition);
    }

    public void SetPlayerPosition(Vector2 p_NewPosition)
    {
        if (m_player == null)
        {
            RespawnPlayer(p_NewPosition);
            Debug.Log("m_Player was null at PlayerManger.MovePlayer");
        }

        m_player.SetPlayerPosition(p_NewPosition);
    }

    public void SetPlayerSprite(Sprite p_NewSprite)
    {
        if(m_player == null)
        {
            Debug.Log("Player is null at PlayerManager.SetPlayerSprite");
        }
        else
        {
            m_player.ChangePlayerSprite(p_NewSprite);
        }
    }

    public Vector2 GetPlayerVelocity()
    {
        if(m_player == null)
        {
            Debug.Log("Player is null at PlayerManager.GetPlayerVelocity");
            return Vector2.zero;
        }
        else
        {
            return m_player.m_Velocity;
        }
    }

    public Vector2 GetPlayerSize()
    {
        if(m_player == null)
        {
            Debug.Log("Player is null at PlayerManager.GetPlayerSize");
            return Vector2.zero;
        }
        else
        {
           return m_player.m_SpriteRenderer.bounds.size;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
