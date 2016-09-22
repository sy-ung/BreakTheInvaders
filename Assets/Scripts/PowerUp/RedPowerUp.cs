using UnityEngine;
using System.Collections;

public class RedPowerUp : PowerUp {

    void Awake()
    {
        m_muzzlepowerup = AssetManager.m_Instance.GetPrefab("RedBeamMuzzle");
        m_ballpowerup = AssetManager.m_Instance.GetPrefab("RedBall");
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
}
