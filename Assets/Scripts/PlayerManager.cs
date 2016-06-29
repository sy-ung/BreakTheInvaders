using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager m_Instance;

    private GameObject m_Player;

    public static PlayerManager getInstance()
    {
        if(m_Instance == null)
        {
            m_Instance = new PlayerManager();
        }
        return m_Instance;
    }

    public void RespawnPlayer(Vector2 p_NewPosition)
    {
        Vector3 t_NewPosition = new Vector3(p_NewPosition.x, p_NewPosition.y, 0);
        if (m_Player == null)
        {
            m_Player = new GameObject("PlayerPaddle");
            m_Player.AddComponent<Player>();
            m_Player.GetComponent<Player>().m_DefaultPlayerTexture = AssetManager.getInstance().GetTexture("Player");
            m_Player.transform.position = t_NewPosition;
            //Instantiate(m_Player, t_NewPosition, Quaternion.identity);

        }
        else
        {
            m_Player.transform.position = t_NewPosition;
        }
    }

    private PlayerManager()
    {

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
