using UnityEngine;
using System.Collections;

public class RedBeamMuzzle : Muzzle {

    private RedBeamBullet m_currentbeam;

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletRedBeam");
        m_spriteRenderer.color = Color.red;

        SpawnBeam();
        SetMaxAmmoCount(-1);
        m_ammobar.m_ammoprefab = AssetManager.m_Instance.GetPrefab("RedBeamAmmoIcon");

    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
        if(m_currentbeam != null)
        { 
            m_currentbeam.SetBeamActivation(m_StartFiring);
        }


    }

    void SpawnBeam()
    {
        Vector2 t_firedirection = Vector3.up;

        GameObject t_beam = Instantiate(m_currentBullet) as GameObject;

        t_beam.GetComponent<RedBeamBullet>().m_RedBeamMuzzle = this;

        t_beam.transform.localScale = new Vector2(1, 1);
        t_beam.transform.localPosition = Vector2.zero;
        //t_beam.transform.localPosition = new Vector2(0, m_SpriteRenderer.bounds.size.y + t_beam.GetComponent<SpriteRenderer>().bounds.size.y);

        m_currentbeam = t_beam.GetComponent<RedBeamBullet>();

    }

    public override void DestroyMuzzle()
    {
        if(m_currentbeam!=null)
        {
            m_currentbeam.StopAllSounds();
            m_currentbeam.Expired();
        }
        base.DestroyMuzzle();
    }
}
