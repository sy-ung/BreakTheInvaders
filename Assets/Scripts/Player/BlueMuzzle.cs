using UnityEngine;
using System.Collections;

public class BlueMuzzle : Muzzle {

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletBlue");
        m_spriteRenderer.color = Color.blue;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        SetMaxAmmoCount(5);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
