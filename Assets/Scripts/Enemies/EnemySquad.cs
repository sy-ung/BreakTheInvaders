using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySquad : MonoBehaviour {

    private List<Enemy> m_enemies = new List<Enemy>();
    private bool m_killall = false;
    private int m_spawnedenemiescount;

    private bool m_issquaddead = false;
    public bool m_IsSquadDead
    {
        get { return m_issquaddead; }
    }

    public bool m_squadinboundsX = false;
    public bool m_squadinboundsY = false;

    private bool m_inplay = false;

    private float m_movementtime;
    private float m_movementtimer;

    private string[] m_movementsounds;

    public bool m_MoveSquadDown;

    private bool m_changedirection;
    public bool m_ChangeDirection
    {
        get { return m_changedirection; }
    }

    private bool m_playframe;
    public bool m_PlayFrame
    {
        get { return m_playframe; }
    }

    private Vector2 m_movementspeed;

    private bool m_playaudiomove1;

    void Awake()
    {
        m_movementsounds = new string[3] { "EnemyMove1", "EnemyMove2", "EnemySpecialMove" };
        m_movementtime = 0.05f;
        m_movementspeed = new Vector2(1f, 0.5f);
        m_playaudiomove1 = true;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_inplay)
        { 
            SquadBoundryCheck();
        }
        else
        {
            m_movementtimer += Time.deltaTime;
            if (m_movementtimer > m_movementtime)
            {
                if (!m_MoveSquadDown)
                    MoveSquad();
                else
                    MoveSquadDown();

                m_playframe = true;

                m_movementtimer = 0;
            }
            else
                m_playframe = false;
        }
    }
    void LateUpdate()
    {

    }

    void SquadBoundryCheck()
    {

        if (!m_squadinboundsX)
        {
            for (int i = 0; i <m_enemies.Count; i++)
            {

                if (!m_enemies[i].m_InBoundsX)
                {
                    m_squadinboundsX = false;
                    break;
                }
                else
                    m_squadinboundsX = true;
            }
        }

        if (!m_squadinboundsY)
        {
            for (int i = 0; i < m_enemies.Count; i++)
            {

                if (!m_enemies[i].m_InBoundsY)
                {
                    m_squadinboundsY = false;
                    break;
                }
                else
                    m_squadinboundsY = true;
            }
        }


        if (!m_inplay)
        { 
            if (!m_squadinboundsX)
            {
                MoveSquad();
            }

            if (!m_squadinboundsY)
            {
                MoveSquadDown();
            }

            if (m_squadinboundsX && m_squadinboundsY)
            {
                m_inplay = true;
                ResetSquadSpeed();
            }
        }


    }

    //When creating a squad, they will always originally move to the right, use ChangeDirection on enemy to get it moveing to the left
    public void CreateSquad(string p_EnemyPrefabName, int p_Rows, int p_Columns)
    {
        Vector2 t_screensize = Camera.main.GetComponent<ResolutionFix>().m_ScreenSizeWorldPoint;

        GameObject t_EnemyPrefab = AssetManager.m_Instance.GetPrefab(p_EnemyPrefabName);

        bool t_switchside = false;



        Vector2 t_origin = new Vector2(0, Random.Range(0 + t_screensize.y/2, t_screensize.y * 2.0f));

        if (t_origin.y < t_screensize.y)
        {
            if (Random.Range(0, 2) == 0)
            {
                t_origin = new Vector2(Random.Range(-t_screensize.x * 2.0f, -t_screensize.x * 2.0f), t_origin.y);
            }
            else
            {
                t_origin = new Vector2(Random.Range(t_screensize.x * 2.0f, t_screensize.x * 2.0f), t_origin.y);
            }
        }
        else
            t_origin = new Vector2(Random.Range(-t_screensize.x * 2.0f, t_screensize.x * 2.0f), t_origin.y);

        //Set squad transform to starting spawn point
        transform.position = t_origin;

        if (t_origin.x > 0)
            ChangeDirection();


        Vector2 t_currentspawnPOS;
        Vector2 t_previousspawnPOS = Vector2.zero;

        float t_paddingx = 0.01f;
        float t_paddingy = 0f;

        m_spawnedenemiescount = p_Rows * p_Columns;

        for (int i = 0; i < p_Rows; i++)
        {
            for (int j = 0; j < p_Columns; j++)
            {
                Enemy t_enemy = (Instantiate(t_EnemyPrefab)).GetComponent<Enemy>();
                t_enemy.tag = "Enemy";

                t_enemy.transform.SetParent(transform);

                float t_posx = t_enemy.GetComponent<SpriteRenderer>().bounds.size.x;


                float t_posy = t_previousspawnPOS.y - ((t_enemy.GetComponent<SpriteRenderer>().bounds.size.y) * i) - (i * t_paddingy);

                if (t_switchside)
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x + (t_posx * j) + (j * t_paddingx)), t_posy);
                    t_switchside = false;
                }
                else
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x - (t_posx * j) - (j * t_paddingx)), t_posy);
                    t_switchside = true;
                }

                t_enemy.transform.localPosition = t_currentspawnPOS;
                t_previousspawnPOS = new Vector2(t_currentspawnPOS.x, t_previousspawnPOS.y);


                if (p_Rows == 1 && p_Columns == 1)
                {
                    t_enemy.m_SoleSurvivor = true;
                }

                t_enemy.m_CurrentEnemySquad = this;
                m_enemies.Add(t_enemy);


            }

            t_previousspawnPOS = new Vector2(0, t_previousspawnPOS.y);
        }
        transform.localScale *= 0.5f;
    }

    public void MoveSquadDown()
    {
        //for (int i = 0; i < m_enemies.Count; i++)
        //{
        //    if (m_killall)
        //        break;
        //    else
        //    {
        //        m_enemies[i].m_movedown = p_Value;
        //    }
        //}

        if (m_inplay)
            PlayMoveAudio();

        transform.position += new Vector3(0, -m_enemies[0].m_HalfSize.y * m_movementspeed.y);
        m_MoveSquadDown = false;
        ChangeDirection();
    }

    public void MoveSquad()
    {
        //for (int i = 0; i < m_enemies.Count; i++)
        //{
        //    if (m_killall)
        //        break;
        //    else
        //    {
        //        m_enemies[i].m_move = p_Value;
        //        m_enemies[i].MoveEnemy();
        //    }
        //}
        if(m_ChangeDirection)
            transform.position += new Vector3(-m_enemies[0].m_HalfSize.x * m_movementspeed.x, 0);
        else
            transform.position += new Vector3(m_enemies[0].m_HalfSize.x * m_movementspeed.x, 0);

        if (m_inplay)
            PlayMoveAudio();

    }

    void PlayMoveAudio()
    {
        if(m_playaudiomove1)
        { 
            GameAudioManager.m_Instance.PlaySound("EnemyMove1", false, 1);
            m_playaudiomove1 = false;
        }
        else
        {
            GameAudioManager.m_Instance.PlaySound("EnemyMove2", false, 1);
            m_playaudiomove1 = true;
        }
    }

    public void ResetSquadSpeed()
    {

    }

    public void IncreaseSquadSpeed()
    {

    }

    public void ChangeDirection()
    {
        if (m_changedirection)
            m_changedirection = false;
        else if (!m_changedirection)
            m_changedirection = true;
    }

    public void RemoveEnemyFromSquad(Enemy p_DeadEnemy)
    {
        if(m_enemies.Count == 1)
        {
            SpawnPowerUp();
        }

        Destroy(p_DeadEnemy.gameObject);
        m_enemies.Remove(p_DeadEnemy);
        SquadCheck();
    }

    public void KillAllInSquad()
    {
        m_killall = true;
        for (int i = 0; i < m_enemies.Count; i++)
        {
            Destroy(m_enemies[i].gameObject);
        }
        SquadCheck();
    }

    void SquadCheck()
    {
        if (m_enemies.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        if (m_enemies.Count == 1)
        {

        }

        if (m_killall)
        { 
            Destroy(gameObject);
            return;
        }

        for(int i = 0; i <m_enemies.Count;i++)
        {
            if (m_enemies[i] != null)
                return;
        }
        Destroy(gameObject);
    }

    void SpawnPowerUp()
    {

        int t_powerupchoice = Random.Range(1, 4);

        if (t_powerupchoice == 1)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("BluePower"), m_enemies[0].transform.position, Quaternion.identity);
        }

        if (t_powerupchoice == 2)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("GreenPower"), m_enemies[0].transform.position, Quaternion.identity);
        }

        if (t_powerupchoice == 3)
        {
            Instantiate(AssetManager.m_Instance.GetPrefab("RedPower"), m_enemies[0].transform.position, Quaternion.identity);
        }
        
    }


}


