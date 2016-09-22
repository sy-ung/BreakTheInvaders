using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GreenBall : Ball {

    void Awake()
    {
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
        CheckMouseInput();
        base.Update();
        
	}

    void CheckMouseInput()
    {

    }

    void LateUpdate()
    {
        base.LateUpdate();
    }

    void OnTriggerExit2D(Collider2D p_Collider)
    {
        if (p_Collider.tag == "Player")
        {
            p_Collider.GetComponent<Player>().m_Muzzle.Reload();
            p_Collider.GetComponent<Player>().Heal(25);
        }
    }

    public override void Death()
    {
        (Instantiate(AssetManager.m_Instance.GetPrefab("BallDeathParticle"), transform.position, Quaternion.identity) as GameObject).GetComponent<ParticleSystem>().startColor = Color.green;
        base.Death();
    }

}
