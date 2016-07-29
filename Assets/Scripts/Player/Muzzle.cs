using UnityEngine;
using System.Collections;

public class Muzzle : MonoBehaviour {

    private SpriteRenderer m_spriterenderer;

    private GameObject m_currentbullet;
    public GameObject m_CurrentBullet
    {
        set { m_currentbullet = value; }
    }


    private GameObject m_mfprefab;


    void Awake()
    {
        Initialize();
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("PlayerMuzzle");
    }

    void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
        
    }

	// Use this for initialization
	void Start () {
        SetScale(1);
        Player t_player = PlayerManager.m_Instance.m_Player;
        transform.position = (Vector2)t_player.transform.position + new Vector2(0,t_player.m_PlayerSize.y / 2 + m_spriterenderer.bounds.size.y / 2);

        m_mfprefab = AssetManager.m_Instance.GetPrefab("MuzzleFlashDefault");

        m_currentbullet = AssetManager.m_Instance.GetPrefab("BulletDefault");

    }
	void SetScale(float p_ScaleMultiplier)
    {
        transform.localScale = new Vector3(1, 1, 0) * p_ScaleMultiplier;
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void FireProjectile()
    {
        Vector2 t_firedirection = Vector3.up;

        //BallBullet t_bullet = (new GameObject("BallBullet").AddComponent<BallBullet>());

        Bullet t_bullet = Instantiate(m_currentbullet).GetComponent<Bullet>();

        Vector2 t_startPosition = (Vector2)transform.position + (t_firedirection * (t_bullet.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2));
        t_bullet.transform.position = t_startPosition;

        t_bullet.transform.rotation = Quaternion.FromToRotation(t_bullet.transform.right, t_firedirection);
        t_bullet.tag = "Bullet";


        //Attach a particle system to player and let it play the muzzle flash
        GameObject t_mf = Instantiate(m_mfprefab, transform.position, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        t_mf.transform.parent = transform;
    }
}
