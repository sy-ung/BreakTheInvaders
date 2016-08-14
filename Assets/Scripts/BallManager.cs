using UnityEngine;

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

    private Ball m_playerball;
    public Ball m_PlayerBall
    {
        get
        {
            if (m_playerball != null)
                return m_playerball;
            else
            {
                Debug.Log("m_playerball is null at get m_PlayerBall");
                return null;
            }
        }
    }

    void Awake()
    {

    }



    public void RespawnBall(Vector2 p_Position)
    {

        if(m_playerball == null)
        {
            m_playerball = FindObjectOfType<Ball>();
            if(m_playerball == null)
            {
                m_playerball = Instantiate(AssetManager.m_Instance.GetPrefab("GreenBall")).GetComponent<Ball>();
            }
        }

        m_playerball.transform.position = new Vector3(p_Position.x, p_Position.y, 0);
        m_playerball.LaunchBall();

    }
    public void ChangeBall(GameObject p_NewBall)
    {
        if (m_playerball != null)
        {
            GameObject t_newball = Instantiate(p_NewBall, m_playerball.transform.position, m_playerball.transform.rotation) as GameObject;

            Vector2 t_direction = m_playerball.m_Direction;
            Vector2 t_storedposition = m_playerball.m_StoredCurrentPosition;

            Destroy(m_playerball.gameObject);

            m_playerball = t_newball.GetComponent<Ball>();
            m_playerball.m_Direction = t_direction;
            m_playerball.m_StoredCurrentPosition = t_storedposition;

        }
        else
            Debug.Log("m_playerball in BallManager.ChangeBall is null");
    }
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
