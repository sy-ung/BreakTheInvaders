using UnityEngine;

public class BallManager : MonoBehaviour {


    public Bullet m_StandardBallBullet;

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
        //m_StandardBallBullet = 
    }



    public void RespawnBall(Vector2 p_Position)
    {

        if(m_playerball == null)
        {
            m_playerball = FindObjectOfType<Ball>();
            if(m_playerball == null)
            {
                GameObject t_PlayerBallObject = new GameObject("PlayerBall");
                m_playerball = t_PlayerBallObject.AddComponent<Ball>();

            }
        }

        m_playerball.transform.position = new Vector3(p_Position.x, p_Position.y, 0);
        SetBallCurrentBullet(Resources.Load<Bullet>("Prefabs/BallBulletRegular"));
        m_playerball.OnRespawn();

    }

    public void SetBallSprite(Sprite p_NewSprite)
    {
        if(m_playerball == null)
            Debug.Log("PlayerBall is null in BallManager.SetBallSprite");
        
        else
            m_playerball.ChangeBallSprite(p_NewSprite);
        
    }

    public void SetBallScale(float p_NewScaleMultiplier)
    {
        if (m_playerball == null)
            Debug.Log("m_PlayerBall is null in BallManager.SetBallScale");
        else
            m_playerball.ScaleBall(p_NewScaleMultiplier);
            
    }

    public void SetBallCurrentBullet(Bullet p_NewBullet)
    {
        if (m_playerball == null)
            Debug.Log("m_PlayerBall is null at BallManager.SetCurrentBullet");
        else
            m_playerball.m_CurrentBullet = p_NewBullet;
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
