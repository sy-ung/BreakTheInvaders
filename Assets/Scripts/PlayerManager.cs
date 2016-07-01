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



    private Player m_Player;


    void Awake()
    {
        
    }

    public void RespawnPlayer(Vector2 p_NewPosition)
    {
        if (m_Player == null)
        {
            //m_Player = GameObject.FindGameObjectWithTag("Player");
            m_Player = FindObjectOfType<Player>();
            if(m_Player == null)
            {
                GameObject t_PlayerObject = new GameObject("Player");
                m_Player = t_PlayerObject.AddComponent<Player>();
                m_Player.tag = "Player";
            }
        }

        m_Player.transform.position = new Vector3(p_NewPosition.x, p_NewPosition.y, 0);
        
    }
    
    public void SetPlayerSprite(Sprite p_NewSprite)
    {
        if(m_Player == null)
        {
            Debug.Log("Player is null for PlayerManager.SetPlayerSprite");
        }
        else
        {
            m_Player.ChangePlayerSprite(p_NewSprite);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
