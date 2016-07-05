using UnityEngine;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

	// Use this for initialization
	void Start () {

        PlayerManager.m_Instance.RespawnPlayer();
        BallManager.m_Instance.RespawnBall(new Vector2(0,1));
        EnemyManager.m_Instance.SpawnEnemies(5,11);
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();

    }

    float t_rot = 0;

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Player t_player = PlayerManager.m_Instance.m_Player;
            if (t_player.m_CurrentControlMode == Player.ControlMode.BATTING)
            {
                t_player.m_CurrentControlMode = Player.ControlMode.DRAG;
            }
            else
            {
                t_player.m_CurrentControlMode = Player.ControlMode.BATTING;
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

        }
    }
}
