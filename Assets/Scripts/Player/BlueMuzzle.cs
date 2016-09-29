using UnityEngine;
using System.Collections;

public class BlueMuzzle : Muzzle {

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletBlue");
        m_spriteRenderer.color = Color.blue;
        m_soundClipName = new string[1] { "BlueMuzzleFire" };
        m_muzzleshakestrength = 0.05f;

        m_firingrate = 0.2f;
        SetMaxAmmoCount(12);
        m_ammobar.m_ammoprefab = AssetManager.m_Instance.GetPrefab("BlueAmmoIcon");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

}
