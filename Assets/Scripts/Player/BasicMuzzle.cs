using UnityEngine;
using System.Collections;

public class BasicMuzzle : Muzzle {




    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletDefault");
        m_soundClipName = new string[3] { "BasicMuzzleFire1", "BasicMuzzleFire2", "BasicMuzzleFire3" };


    }

	// Use this for initialization
	void Start ()
    {
        base.Start();
        m_firingrate = 0.125f;
        SetMaxAmmoCount(20);
        m_ammobar.m_ammoprefab = AssetManager.m_Instance.GetPrefab("BasicAmmoIcon");
    }
	


	// Update is called once per frame
	void Update ()
    {
        base.Update();
    }






}
