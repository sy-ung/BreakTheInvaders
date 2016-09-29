using UnityEngine;
using System.Collections;

public class GreenMuzzle : Muzzle {


    private bool m_projectileready;

    GreenBullet m_currentspawnedprojectile;

    void Awake()
    {
        base.Awake();
        m_currentBullet = AssetManager.m_Instance.GetPrefab("BulletGreen");
        m_spriteRenderer.color = Color.green;
        m_projectileready = false;

        m_firingrate = 0f;
        m_firingtimer = m_firingrate + 1;
        m_muzzleshakestrength = 0.045f;

        SetMaxAmmoCount(20);
        m_ammobar.m_ammoprefab = AssetManager.m_Instance.GetPrefab("GreenAmmoIcon");
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.m_GameStarted)
        {
            if (m_currentammocount <= 0)
            {
                ShowReload();
                return;
            }
            else if (m_currentammocount > 0)
                HideReload();
        }

        if (m_StartFiring)
        {

            if(m_firingtimer > m_firingrate)
            { 
                if(m_currentspawnedprojectile == null)
                {
                    SpawnProjectile();
                }
            }
            else
            {
                m_firingtimer += Time.deltaTime;
            }


        }

        if (!m_StartFiring)
        {
            m_firingtimer = m_firingrate + 1.0f;
            if (m_currentspawnedprojectile != null)
            {
                {
                    Destroy(m_currentspawnedprojectile.gameObject);
                    GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
                }
            }
        }


        if (m_projectileready)
        {
            LaunchProjectile();
            m_firingtimer = 0;
        }


    }

    void LaunchProjectile()
    {
        m_currentspawnedprojectile.Launch(30f);
        m_currentspawnedprojectile = null;
        m_currentammocount--;
        m_ammobar.EjectCurrentAmmo();

        CameraShake();

        GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
        GameAudioManager.m_Instance.PlaySound("GreenMuzzleLaunch", false, 1.0f, m_muzzlevolume);
        m_projectileready = false;
    }

    void SpawnProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        m_currentspawnedprojectile = Instantiate(m_currentBullet).GetComponent<GreenBullet>();
        m_currentspawnedprojectile.m_GreenMuzzle = this;

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (m_currentspawnedprojectile.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        m_currentspawnedprojectile.transform.position = t_startPosition;

        //m_currentspawnedprojectile.transform.rotation = Quaternion.FromToRotation(m_currentspawnedprojectile.transform.right, t_firedirection);

        GameAudioManager.m_Instance.PlaySound("GreenMuzzleCharge", true, 1.0f, m_muzzlevolume);

    }

    public void ReadyToFire(bool p_Value)
    {
        m_projectileready = p_Value;
        if (p_Value)
        {
            GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
        }
    }

    public override void DestroyMuzzle()
    {
        GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
        if (m_currentspawnedprojectile != null)
        {
            Destroy(m_currentspawnedprojectile.gameObject);
        }
        base.DestroyMuzzle();
    }

}
