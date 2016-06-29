using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PlayerManager.getInstance().RespawnPlayer(new Vector2(0.2f,0.2f));
        Debug.Log("test");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
