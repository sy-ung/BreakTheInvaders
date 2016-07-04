using UnityEngine;


//This class is persistent throughout the game as it has references to all the singletons
//As well as all the persistent data regarding
public class PointBlank : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
