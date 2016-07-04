using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0.0f, 0.0f);

	// Use this for initialization
	void Start () {

        PlayerManager.m_Instance.RespawnPlayer(Spawnpoint);
        BallManager.m_Instance.RespawnBall(Spawnpoint);
        //PlayerManager.m_Instance.RespawnPlayer(new Vector2(0.5f, 0.5f));
        //PlayerManager.m_Instance.SetPlayerSprite(AssetManager.m_Instance.GetSprite("PlayerBall"));
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
            PlayerManager.m_Instance.SetPlayerPosition(t_MousePOS);
        }
    }
}
