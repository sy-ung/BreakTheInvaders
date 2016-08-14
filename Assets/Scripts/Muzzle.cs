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
    private float m_firingtimer = 0;

    public bool m_StartFiring;

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


        //Attach a particle system to player and let it play the muzzle flash
        //GameObject t_mf = Instantiate(m_mfprefab, transform.position, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        //t_mf.transform.parent = transform;
    }

    public virtual void DestroyMuzzle() 
    {
        Destroy(gameObject);
    }

    void CheckFire()
    {
        if (m_firingtimer > m_firingrate)
        {

            if (m_StartFiring)
            {
                FireProjectile();
                m_firingtimer = 0;
            }

        }
        else
        {
            m_firingtimer += Time.deltaTime;
        }
    }
}
