using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {



	// Use this for initialization
	void Start () {
        PlayMe();
    }
	
	// Update is called once per frame
	void Update () {
        //if (!GetComponent<ParticleSystem>().IsAlive())
        //{
        //    Destroy(gameObject);
        //}

    }

    public void PlayMe()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
