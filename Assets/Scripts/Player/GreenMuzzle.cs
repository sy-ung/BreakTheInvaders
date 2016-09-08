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
        //base.Update();
        if (m_currentammocount <= 0)
            return;
        
        if(m_StartFiring)
        {
            if(m_currentspawnedprojectile == null)
            {
                SpawnProjectile();
            }
        }
        else if(!m_StartFiring)
        {
            if(m_currentspawnedprojectile != null)
            {
                if(m_projectileready)
                {
                    LaunchProjectile();
                }
                else
                {
                    Destroy(m_currentspawnedprojectile.gameObject);
                    GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
                }
            }
        }
        

    }

    void LaunchProjectile()
    {
        m_currentspawnedprojectile.Launch(30f);
        m_currentspawnedprojectile = null;
        m_currentammocount--;
        m_ammobar.EjectCurrentAmmo();

        GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
        GameAudioManager.m_Instance.PlaySound("GreenMuzzleLaunch", false, 1.0f);
    }

    void SpawnProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        m_currentspawnedprojectile = Instantiate(m_currentBullet).GetComponent<GreenBullet>();
        m_currentspawnedprojectile.m_GreenMuzzle = this;

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (m_currentspawnedprojectile.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        m_currentspawnedprojectile.transform.position = t_startPosition;

        m_currentspawnedprojectile.transform.rotation = Quaternion.FromToRotation(m_currentspawnedprojectile.transform.right, t_firedirection);

        GameAudioManager.m_Instance.PlaySound("GreenMuzzleCharge", true, 1.0f);

    }

    public void ReadyToFire(bool p_Value)
    {
        m_projectileready = p_Value;
        if (p_Value)
        {
            GameAudioManager.m_Instance.StopSound("GreenMuzzleCharge");
            GameAudioManager.m_Instance.PlaySound("GreenMuzzleCharge", true, 1.5f);
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
