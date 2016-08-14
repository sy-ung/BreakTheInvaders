using UnityEngine;
using System.Collections;

public class DefaultBall : Ball {

    void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}

    void LateUpdate()
    {
        base.LateUpdate();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
    }
}
