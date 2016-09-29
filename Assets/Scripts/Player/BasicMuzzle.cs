using UnityEngine;
using System.Collections;

public class BasicMuzzle : Muzzle {

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletDefault");
        m_soundClipName = new string[3] { "BasicMuzzleFire1", "BasicMuzzleFire2", "BasicMuzzleFire3" };

        m_muzzleshakestrength = 0.0145f;

        //m_firingrate = 0.125f;
        m_firingrate = 0.05f;
        SetMaxAmmoCount(100);
        m_ammobar.m_ammoprefab = AssetManager.m_Instance.GetPrefab("BasicAmmoIcon");

    }


	// Update is called once per frame
	void Update ()
    {
        base.Update();
    }






}
