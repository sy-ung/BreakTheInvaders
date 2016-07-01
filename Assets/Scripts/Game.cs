using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    Vector2 Spawnpoint = new Vector2(0, 0);

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
        if(Input.GetKey(KeyCode.A))
        {
            PlayerManager.m_Instance.SetPlayerSprite(AssetManager.m_Instance.GetSprite("PlayerBall"));
        }

        if (Input.GetKey(KeyCode.S))
        {
            PlayerManager.m_Instance.SetPlayerSprite(AssetManager.m_Instance.GetSprite("Player"));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Spawnpoint = Spawnpoint + new Vector2(0.01f, 0);
            PlayerManager.m_Instance.RespawnPlayer(Spawnpoint);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Spawnpoint = Spawnpoint + new Vector2(0, 0.01f);
            PlayerManager.m_Instance.RespawnPlayer(Spawnpoint);
        }

    }
}
