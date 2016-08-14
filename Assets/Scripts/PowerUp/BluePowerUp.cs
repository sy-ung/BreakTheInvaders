using UnityEngine;
using System.Collections;

public class BluePowerUp : PowerUp
{
    void Awake()
    {
        m_muzzlepowerup = AssetManager.m_Instance.GetPrefab("BlueMuzzle");
        m_ballpowerup = AssetManager.m_Instance.GetPrefab("BlueBall");

        base.Awake();
    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
	}


    void Update()
    {
        base.Update();
    }

    void OnCollisionEnter2D(Collision2D p_Collision)
    {
        base.OnCollisionEnter2D(p_Collision);
    }
}
