using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour {

    private static BallManager m_instance;

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

    private Ball m_PlayerBall;




    public void RespawnBall(Vector2 p_Position)
    {

        if(m_PlayerBall == null)
        {
            m_PlayerBall = FindObjectOfType<Ball>();
            if(m_PlayerBall == null)
            {
                GameObject t_PlayerBallObject = new GameObject("PlayerBall");
                m_PlayerBall = t_PlayerBallObject.AddComponent<Ball>();

            }
        }

        m_PlayerBall.transform.position = new Vector3(p_Position.x, p_Position.y, 0);

    }

    public void SetBallSprite(Sprite p_NewSprite)
    {
        if(m_PlayerBall == null)
        {
            Debug.Log("PlayerBall is null in BallManager.SetBallSprite");
        }
        else
        {
            m_PlayerBall.ChangeBallSprite(p_NewSprite);
        }
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
