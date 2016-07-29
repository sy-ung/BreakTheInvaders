using UnityEngine;

class PlayerBullet : Bullet
{
    void Awake()
    {
        p_SpriteName = "Bullet";
        m_MuzzleFlash = AssetManager.m_Instance.GetPrefab("MuzzleFlashDefault");
        m_speed = 10.0f;
        base.Awake();
    }
}

