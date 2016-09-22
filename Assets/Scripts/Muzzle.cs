using UnityEngine;
using System.Collections;

public class Muzzle : MonoBehaviour {

    protected SpriteRenderer m_spriteRenderer;
    public SpriteRenderer m_SpriteRenderer
    {
        get { return m_spriteRenderer; }
    }

    protected GameObject m_currentBullet;
    protected GameObject m_muzzleFlash;


    protected float m_firingrate = 0.1f;
    protected float m_firingtimer = 0;

    //if m_maxammocount is -1, it means inifinite ammo
    protected int m_maxammocount;
    public int m_MaxAmmoCount
    {
        get { return m_maxammocount; }
    }

    protected int m_currentammocount;
    public int m_CurrentAmmoCount
    {
        get { return m_currentammocount; }
    }

    public bool m_StartFiring;

    protected AmmoBar m_ammobar;

    protected string[] m_soundClipName;

    protected void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_firingtimer = m_firingrate;
    }



    // Use this for initialization
    protected void Start() {
        SetScale(1);
        Player t_player = PlayerManager.m_Instance.m_Player;
        transform.position = (Vector2)t_player.transform.position + new Vector2(0, t_player.m_PlayerSize.y / 2 + m_spriteRenderer.bounds.size.y / 2);

        m_spriteRenderer.sortingLayerName = "Player";
        //m_mfprefab = AssetManager.m_Instance.GetPrefab("MuzzleFlashDefault");

        //m_currentbullet = AssetManager.m_Instance.GetPrefab("BulletGreen");
        m_ammobar = Instantiate(AssetManager.m_Instance.GetPrefab("AmmoBar")).GetComponent<AmmoBar>();
        m_ammobar.transform.SetParent(transform);
        m_ammobar.transform.localScale = new Vector2(3, 1.75f);
        m_ammobar.transform.localPosition = new Vector2(-m_spriteRenderer.bounds.size.y * 9, -m_spriteRenderer.bounds.size.y * 9);

    }

    public  void Reload()
    {
        if(m_MaxAmmoCount != -1)
        {
            if (m_currentammocount != m_maxammocount)
                GameAudioManager.m_Instance.PlaySound("Reload", false, 1.0f,true);

            SetMaxAmmoCount(m_maxammocount);

            if(m_ammobar!=null)
                m_ammobar.Reload();


        }
    }

    //Set -1 for infinitammo
    public void SetMaxAmmoCount(int p_MaxAmmoCount)
    {
        m_maxammocount = p_MaxAmmoCount;
        m_currentammocount = m_maxammocount;
    }

    public void SetScale(float p_ScaleMultiplier)
    {
        transform.localScale = new Vector3(1, 1, 0) * p_ScaleMultiplier;
    }
    // Update is called once per frame
    protected void Update()
    {
        CheckFire();
    }

    public void FireProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        //BallBullet t_bullet = (new GameObject("BallBullet").AddComponent<BallBullet>());

        Bullet t_bullet = Instantiate(m_currentBullet).GetComponent<Bullet>();

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (t_bullet.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        t_bullet.transform.position = t_startPosition;

        t_bullet.transform.rotation = Quaternion.FromToRotation(t_bullet.transform.right, t_firedirection);

        if(m_maxammocount != -1)
        {
            m_currentammocount--;
            m_ammobar.EjectCurrentAmmo();
        }

        

        //Attach a particle system to player and let it play the muzzle flash
        //GameObject t_mf = Instantiate(m_mfprefab, transform.position, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        //t_mf.transform.parent = transform;
    }

    public virtual void DestroyMuzzle() 
    {

        if(!PlayerManager.m_Instance.m_Player.m_Alive)
        {
            (Instantiate(AssetManager.m_Instance.GetPrefab("MuzzleDeathParticle"), transform.position,Quaternion.Euler(new Vector3(-90,0,0))) as GameObject).GetComponent<ParticleSystem>().startColor = m_spriteRenderer.color;
        }
        if(m_ammobar!= null)
            m_ammobar.Death();
        Destroy(gameObject);
    }

    void CheckFire()
    {
        if (m_firingtimer > m_firingrate)
        {

            if (m_StartFiring)
            {
                if (m_currentammocount > 0 || m_MaxAmmoCount == -1)
                {
                    FireProjectile();
                    PlayWeaponFireSounds();
                }
                m_firingtimer = 0;
            }

        }
        else
        {
            m_firingtimer += Time.deltaTime;
        }
    }


    void PlayWeaponFireSounds()
    {
        //m_audiosource.PlayOneShot(m_WeaponFireSounds[Random.Range(0, m_WeaponFireSounds.Length)]);

        GameAudioManager.m_Instance.PlaySound(m_soundClipName[Random.Range(0,m_soundClipName.Length)], false, 1.0f,false);
    }



}
