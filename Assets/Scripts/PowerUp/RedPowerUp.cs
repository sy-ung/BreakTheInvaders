using UnityEngine;
using System.Collections;

public class RedPowerUp : PowerUp {

    void Awake()
    {
        m_muzzlepowerup = AssetManager.m_Instance.GetPrefab("RedBeamMuzzle");
        m_ballpowerup = AssetManager.m_Instance.GetPrefab("RedBall");
        base.Awake();

        m_highlight.SetHighlightColor(Color.red);
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

    public override void ApplyMuzzlePowerUp(Collider2D p_PlayerCollider)
    {
        p_PlayerCollider.GetComponent<Player>().ApplyMuzzlePowerUp(m_muzzlepowerup,false);
        GameAudioManager.m_Instance.PlaySound("PowerUpHitPlayer", false, 1.0f, 1.0f);
        Destroy(gameObject);
    }

    public override void ApplyBallPowerUp()
    {
        BallManager.m_Instance.ApplyPowerUp(m_ballpowerup,false);
        GameAudioManager.m_Instance.PlaySound("PowerUpHitBall", false, 1.0f, 1.0f);
        Destroy(gameObject);
    }
}
