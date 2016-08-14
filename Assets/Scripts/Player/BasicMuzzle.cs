using UnityEngine;
using System.Collections;

public class BasicMuzzle : Muzzle {


    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletDefault");
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
