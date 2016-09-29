using UnityEngine;
using System.Collections.Generic;

public class AmmoBar : MonoBehaviour {


    public GameObject m_ammoprefab;
    private Infinity m_infinitysign;
    private NoAmmo m_noammosign;

    private List<GameObject> m_ammunition = new List<GameObject>();

    private Player m_player;

    private int m_maxammodisplay;

    private int m_numofammocheck;
    private float m_transparencyscale;

    private GameObject m_objectattached;

    private Vector2 m_newScale;

    void Awake()
    {
        m_player = PlayerManager.m_Instance.m_Player;
        m_maxammodisplay = 10;
        m_numofammocheck = m_maxammodisplay - 1;
        m_transparencyscale = (1.0f - ((float)m_numofammocheck / (float)m_maxammodisplay)) / 1.0f;
    }

    public void Initialize()
    {

    }


	void Start ()
    {
        //transform.localScale = new Vector2(2, 2);
    }

    public void Reload()
    {

        if (m_noammosign != null)
        {
            Destroy(m_noammosign.gameObject);
            m_noammosign = null;
        }

        if (m_ammunition.Count<=m_maxammodisplay)
        {
            int t_reloadamount;

            if (m_player.m_Muzzle.m_MaxAmmoCount < m_maxammodisplay)
            {
                t_reloadamount = m_player.m_Muzzle.m_MaxAmmoCount - m_ammunition.Count;
            }
            else
                t_reloadamount = m_maxammodisplay - m_ammunition.Count;

            for (int i = 0;i<t_reloadamount;i++)
            {
                LoadNewAmmo();
            }
        }
        else
        {
            HideLastAmmos();
        }

        OrganizeAmmo();
    }
	
    public void DisplayAmmo()
    {
        ClearAmmo();

        if(m_player.m_Muzzle.m_MaxAmmoCount == -1)
        {
            DisplayInfiniteAmmo();
        }
        else if(m_player.m_Muzzle.m_CurrentAmmoCount < m_maxammodisplay)
        {
            for(int i = 0; i <m_player.m_Muzzle.m_CurrentAmmoCount;i++)
            {
                LoadNewAmmo();
            }
            OrganizeAmmo();
        }
        else
        { 
            for(int i = 0;i<m_maxammodisplay;i++)
            {
                LoadNewAmmo();
            }
            OrganizeAmmo();
        }

        
    }

    void DisplayInfiniteAmmo()
    {
        Ammo t_ammo = LoadNewAmmo();

        t_ammo.m_NewLocalPosition = new Vector2(t_ammo.GetComponent<SpriteRenderer>().bounds.size.x  * 1.5f, 0);
        m_infinitysign = Instantiate(AssetManager.m_Instance.GetPrefab("InfinitySign")).GetComponent<Infinity>();
        m_infinitysign.transform.SetParent(m_ammunition[0].transform);
        m_infinitysign.transform.localScale = new Vector3(1, 1);
        m_infinitysign.transform.localScale *= m_ammunition[0].GetComponent<SpriteRenderer>().bounds.size.y / m_infinitysign.GetComponent<SpriteRenderer>().bounds.size.y;
        m_infinitysign.transform.localPosition = new Vector2(t_ammo.GetComponent<SpriteRenderer>().bounds.size.x + m_infinitysign.GetComponent<SpriteRenderer>().bounds.size.x/2f, 0);


    }

    void DisplayNoAmmo()
    {
        LoadNewAmmo().m_NewLocalPosition = new Vector2(PlayerManager.m_Instance.m_Player.GetComponent<SpriteRenderer>().bounds.size.x/2,0);
        m_noammosign = Instantiate(AssetManager.m_Instance.GetPrefab("NoAmmoSign")).GetComponent<NoAmmo>();
        m_noammosign.transform.SetParent(m_ammunition[0].transform);
        m_noammosign.transform.localScale = new Vector2(1f,1f);
        m_noammosign.transform.localPosition = Vector2.zero;
        m_noammosign.m_ammoref = m_ammunition[0].GetComponent<Ammo>();
    }

    void ClearAmmo()
    {
        int t_ammocount = m_ammunition.Count;

        for (int i = 0;i<t_ammocount;i++)
        {
            Destroy(m_ammunition[0].gameObject);
            m_ammunition.RemoveAt(0);
        }

        if (m_infinitysign != null)
        {
            Destroy(m_infinitysign.gameObject);
            m_infinitysign = null;
        }


    }

    public void EjectCurrentAmmo()
    {



        m_ammunition[0].GetComponent<Ammo>().Eject();
        m_ammunition.RemoveAt(0);

        if (m_player.m_Muzzle.m_CurrentAmmoCount == 0)
        {
            DisplayNoAmmo();
        }
        else
        { 

            if (m_player.m_Muzzle.m_CurrentAmmoCount >= m_maxammodisplay)
                LoadNewAmmo();

            OrganizeAmmo();
        }

    }

    void HideLastAmmos()
    {
        for (int i = 0; i < m_ammunition.Count; i++)
        {
            m_ammunition[i].GetComponent<Ammo>().SetTransparency(m_transparencyscale * (float)(m_maxammodisplay - i));
        }

    }
    void ShowAllAmmos()
    {
        for (int i = 0; i < m_ammunition.Count; i++)
        {
            m_ammunition[i].GetComponent<Ammo>().SetTransparency(1.0f);
        }
    }

    void OrganizeAmmo()
    {
        for(int i = 0;i<m_ammunition.Count;i++)
        {
            m_ammunition[i].transform.localScale = m_newScale;
            m_ammunition[i].GetComponent<Ammo>().m_NewLocalPosition = new Vector2(i * (m_ammunition[i].GetComponent<SpriteRenderer>().bounds.size.x) ,0);
        }

    }

    public void Resize(Vector2 p_NewScale)
    {
        m_newScale = p_NewScale;
        if(m_ammunition.Count>0)
        {
            OrganizeAmmo();
        }
    }

    //THIS IS BECAUSE OF UNITY'S SCALING WITH SPRITE RENDERER GIVING INCORRECT DATA SO I IMPLEMENTED A VERY SLOPPY WORK AROUND
    public void AttachTo(GameObject p_Object)
    {
        m_objectattached = p_Object;
    }

    Ammo LoadNewAmmo()
    {

        GameObject t_ammo = Instantiate(m_ammoprefab);

        t_ammo.transform.SetParent(transform);
        t_ammo.transform.localScale = new Vector2(1, 1);
        m_ammunition.Add(t_ammo);
        t_ammo.transform.localPosition = new Vector2((t_ammo.GetComponent<SpriteRenderer>().bounds.size.x) * (m_maxammodisplay), 0);

        int t_ammocount = m_player.m_Muzzle.m_CurrentAmmoCount;

        if (t_ammocount == -1 || t_ammocount == 0)
        {
            t_ammo.GetComponent<Ammo>().SetTransparency(1.0f);
        }

        if (t_ammocount > m_maxammodisplay)
        {
            HideLastAmmos();
        }
        else if(t_ammocount <= m_maxammodisplay)
        { 
            if(m_ammunition.Count <= m_maxammodisplay)
            {
                ShowAllAmmos();
            }
        }

        return t_ammo.GetComponent<Ammo>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update ()
    {
        transform.position = (Vector2)m_objectattached.transform.position - new Vector2(m_objectattached.GetComponent<SpriteRenderer>().bounds.size.x/2,m_ammunition[0].GetComponent<SpriteRenderer>().bounds.size.y);
    }



}
