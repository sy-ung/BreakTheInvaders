using UnityEngine;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

	// Use this for initialization
	void Start () {

        PlayerManager.m_Instance.RespawnPlayer(new Vector2(0,-3));
        BallManager.m_Instance.RespawnBall(Spawnpoint);
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();

    }

    void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 t_MousePOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            t_MousePOS = new Vector2(t_MousePOS.x, -3);
            PlayerManager.m_Instance.m_Player.SetPlayerPosition(t_MousePOS);
        }

        if(Input.GetKey(KeyCode.W))
        {
            PlayerManager.m_Instance.m_Player.SetPlayerScale(1.05f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            PlayerManager.m_Instance.m_Player.SetPlayerScale(0.95f);
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 t_touchposition = Input.GetTouch(0).position;
            t_touchposition = Camera.main.ScreenToWorldPoint(t_touchposition);
            t_touchposition = new Vector2(t_touchposition.x, -3);
            PlayerManager.m_Instance.m_Player.SetPlayerPosition(t_touchposition);
        }
    }
}
