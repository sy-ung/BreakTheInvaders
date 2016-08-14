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
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
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
                    m_currentspawnedprojectile.Launch(30f);
                    m_currentspawnedprojectile = null;
                }
                else
                {
                    Destroy(m_currentspawnedprojectile.gameObject);
                }
            }
        }
        

    }

    public void SpawnProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        m_currentspawnedprojectile = Instantiate(m_currentBullet).GetComponent<GreenBullet>();
        m_currentspawnedprojectile.m_GreenMuzzle = this;

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (m_currentspawnedprojectile.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        m_currentspawnedprojectile.transform.position = t_startPosition;

        m_currentspawnedprojectile.transform.rotation = Quaternion.FromToRotation(m_currentspawnedprojectile.transform.right, t_firedirection);

    }

    public void ReadyToFire(bool p_Value)
    {
        m_projectileready = p_Value;
    }

    public override void DestroyMuzzle()
    {
        if(m_currentspawnedprojectile != null)
        {
            Destroy(m_currentspawnedprojectile.gameObject);
        }
        base.DestroyMuzzle();
    }

}
