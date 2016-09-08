using UnityEngine;
using System.Collections;

public class EnemyDeath : Death {

	// Use this for initialization

    void Awake()
    {
        base.Awake();
        m_DeathsoundClipName = new string[2] { "EnemyDeath1", "EnemyDeath2" };
    }

	void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}
}
