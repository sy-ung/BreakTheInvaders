using UnityEngine;
using UnityEngine.UI;
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
    public float m_FiringRate
    {
        get { return m_firingrate; }
    }
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

    private float m_reloadmsgtimer;
    private float m_reloadmsgrate;
    private Text m_reloadmsg;

    protected float m_muzzlevolume;

    protected float m_muzzleshakestrength;

    public float m_MuzzleVolume
    {
        get { return m_muzzlevolume; }
    }

    protected void Awake()
    {
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Initialize();
    }

    public void Initialize()
    {
        m_firingtimer = m_firingrate;
        m_reloadmsgrate = 0.05f;
        m_muzzlevolume = 0.5f;
        m_muzzleshakestrength = 0.1f;

        m_spriteRenderer.sortingLayerName = "Player";

        m_reloadmsg = GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>();
        CreateAmmoBar();
    }


    void CreateAmmoBar()
    {
        Player t_player = PlayerManager.m_Instance.m_Player;
        m_ammobar = Instantiate(AssetManager.m_Instance.GetPrefab("AmmoBar")).GetComponent<AmmoBar>();

        m_ammobar.AttachTo(t_player.gameObject);
        m_ammobar.Resize(new Vector2(1, 1.1f));


    }

    public void Reload()
    {
        if(m_MaxAmmoCount != -1)
        {
            if (m_currentammocount != m_maxammocount)
                GameAudioManager.m_Instance.PlaySound("Reload", false, 1.0f,1.0f);

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
    // Update is called once per frame
    protected void Update()
    {



        CheckFire();

        if(Game.m_GameStarted)
        { 
            if (m_currentammocount <= 0)
                ShowReload();
            else if(m_currentammocount > 0)
                HideReload();
        }


    }

    public void FireProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        Bullet t_bullet = Instantiate(m_currentBullet).GetComponent<Bullet>();

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (t_bullet.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        t_bullet.transform.position = t_startPosition;

        if(m_maxammocount != -1)
        {
            m_currentammocount--;
            m_ammobar.EjectCurrentAmmo();
        }


        CameraShake();

    }

    protected void CameraShake()
    {
        CameraShake t_cs = Camera.main.GetComponent<CameraShake>();

        if (t_cs.m_Shaking)
            t_cs.IntensifyShake(m_muzzleshakestrength);
        else
            t_cs.StartShake(0.1f, m_muzzleshakestrength);
    }

    public virtual void DestroyMuzzle() 
    {
        HideReload();
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

        GameAudioManager.m_Instance.PlaySound(m_soundClipName[Random.Range(0,m_soundClipName.Length)], false, 1.0f,0.7f);
    }

    protected void HideReload()
    {
        if (m_reloadmsg.enabled)
            m_reloadmsg.enabled = false;
    }

    protected void ShowReload()
    {

        if (!m_reloadmsg.enabled)
        {
            m_reloadmsg.fontSize = 35;
            m_reloadmsg.enabled = true;
        }

        if (m_reloadmsg.text != "RELOAD")
            m_reloadmsg.text = "RELOAD";

        if(m_reloadmsgtimer>m_reloadmsgrate)
        {
            if (m_reloadmsg.color != Color.red)
                m_reloadmsg.color = Color.red;
            else
                m_reloadmsg.color = Color.yellow;

            m_reloadmsgtimer = 0;
        }
        else
        {
            m_reloadmsgtimer += Time.deltaTime;
        }
    }

    public void ShowAmmo()
    {
        m_ammobar.DisplayAmmo();
    }

    public void RescaleAmmoBar(float p_ScaleMultiplier)
    {
        m_ammobar.transform.localScale *= p_ScaleMultiplier;
    }

}
