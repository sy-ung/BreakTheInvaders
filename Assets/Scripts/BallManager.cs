using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

    private static BallManager m_Instance;

    private GameObject m_PlayerBall;

    private BallManager()
    {

    }


    public static BallManager getInstance()
    {
        if (m_Instance == null)
            m_Instance = new BallManager();

        return m_Instance;
    }

    public void RespawnBall(Vector2 p_Position)
    {
        Vector3 t_NewPosition = new Vector3(p_Position.x, p_Position.y, 0);

        if(m_PlayerBall == null)
        {
            m_PlayerBall = new GameObject("PlayerBall");
            m_PlayerBall.AddComponent<Ball>();
            m_PlayerBall.GetComponent<Ball>().m_DefaultBallTexture = AssetManager.getInstance().GetTexture("Ball");
            Instantiate(m_PlayerBall, t_NewPosition, Quaternion.identity);
        }
        else
        {
            m_PlayerBall.transform.position = t_NewPosition;
        }
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
