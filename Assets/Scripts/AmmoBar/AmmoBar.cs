using UnityEngine;
using System.Collections.Generic;

public class AmmoBar : MonoBehaviour {


    private GameObject m_ammoprefab;
    private Infinity m_infinitysign;
    private NoAmmo m_noammosign;

    private List<GameObject> m_ammunition = new List<GameObject>();

    private Player m_player;

    private int m_maxammodisplay;

    private int m_numofammocheck;
    private float m_transparencyscale;

    void Awake()
    {
        m_ammoprefab = AssetManager.m_Instance.GetPrefab("Ammo");
        m_player = PlayerManager.m_Instance.m_Player;
        m_numofammocheck = 8;
        m_maxammodisplay = 9;
        m_transparencyscale = (1.0f - ((float)m_numofammocheck / (float)m_maxammodisplay)) / 1.0f;
    }

	void Start ()
    {

        DisplayAmmo();
        transform.localScale = new Vector2(transform.localScale.x * 0.75f, transform.localScale.y * 0.5f);
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
        }
        else
        { 
            for(int i = 0;i<m_maxammodisplay;i++)
            {
                LoadNewAmmo();
            }
        }
    }

    void DisplayInfiniteAmmo()
    {
        LoadNewAmmo();
        m_infinitysign = Instantiate(AssetManager.m_Instance.GetPrefab("InfinitySign")).GetComponent<Infinity>();
        m_infinitysign.transform.SetParent(transform);

        Vector2 t_infinitysignbounds = m_infinitysign.GetComponent<SpriteRenderer>().bounds.size;
        float t_localstartliney = (m_player.GetComponent<SpriteRenderer>().bounds.size.y / 2) - m_ammoprefab.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        m_infinitysign.transform.localPosition = new Vector2(m_player.GetComponent<SpriteRenderer>().bounds.size.x + t_infinitysignbounds.x * 2, t_localstartliney);

        m_infinitysign.m_NewLocalPosition = new Vector2(t_infinitysignbounds.x, t_localstartliney);
    }

    void DisplayNoAmmo()
    {
        LoadNewAmmo();
        m_noammosign = Instantiate(AssetManager.m_Instance.GetPrefab("NoAmmoSign")).GetComponent<NoAmmo>();
        m_noammosign.transform.SetParent(transform);
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
            return;
        }

        //Shift ammunition
        for (int i = 0;i<m_ammunition.Count;i++)
        {
            m_ammunition[i].GetComponent<Ammo>().m_NewLocalPosition =
                new Vector2
                (
                m_ammunition[i].GetComponent<SpriteRenderer>().sprite.bounds.size.x * 2 * i,
                m_ammunition[i].transform.localPosition.y
                );
        }



        if (m_player.m_Muzzle.m_CurrentAmmoCount >= m_maxammodisplay)
            LoadNewAmmo();

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

    public void LoadNewAmmo()
    {

        GameObject t_ammo = Instantiate(m_ammoprefab);
        Vector2 t_ammobounds = t_ammo.GetComponent<SpriteRenderer>().bounds.size;

        float t_localstartliney = (m_player.GetComponent<SpriteRenderer>().bounds.size.y / 2) - (t_ammobounds.y / 2);

        t_ammo.transform.SetParent(transform);


        t_ammo.GetComponent<Ammo>().m_NewLocalPosition = new Vector2((t_ammobounds.x) * m_ammunition.Count, t_localstartliney);
        t_ammo.transform.localPosition = new Vector2((t_ammobounds.x) * (m_maxammodisplay + 1), t_localstartliney);


        m_ammunition.Add(t_ammo);

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
    }

    public void Death()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update ()
    {
        if(m_ammunition.Count != 0)
        { 
            transform.position = new Vector2(m_player.transform.position.x - (m_player.m_SpriteRenderer.bounds.size.x / 2 - m_ammunition[0].GetComponent<SpriteRenderer>().bounds.size.x/2), m_player.transform.position.y - m_player.m_SpriteRenderer.bounds.size.y * 1.5f);
        }
    }



}
