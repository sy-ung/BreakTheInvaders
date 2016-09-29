using UnityEngine;
using System.Collections;

public class GreenPowerUp : PowerUp {

    void Awake()
    {

        m_muzzlepowerup = AssetManager.m_Instance.GetPrefab("GreenMuzzle");
        m_ballpowerup = AssetManager.m_Instance.GetPrefab("GreenBall");
        base.Awake();

        m_highlight.SetHighlightColor(new Color32(0, 204, 0, 255));
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



}
