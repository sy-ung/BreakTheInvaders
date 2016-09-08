using UnityEngine;
using System.Collections;

public class RedBeamMuzzle : Muzzle {

    private RedBeamBullet m_currentbeam;

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletRedBeam");
        m_spriteRenderer.color = Color.red;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        SpawnBeam();
        SetMaxAmmoCount(-1);
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
        if(m_currentbeam != null)
            m_currentbeam.SetBeamActivation(m_StartFiring);
    }

    void SpawnBeam()
    {
        Vector2 t_firedirection = Vector3.up;

        GameObject t_beam = Instantiate(m_currentBullet) as GameObject;
        m_currentbeam = t_beam.GetComponent<RedBeamBullet>();
        m_currentbeam.m_Muzzle = this;

        t_beam.transform.localScale /= 1.5f;

    }

    public override void DestroyMuzzle()
    {
        if(m_currentbeam!=null)
        {
            m_currentbeam.StopAllSounds();
            Destroy(m_currentbeam.gameObject);
        }
        base.DestroyMuzzle();
    }
}
