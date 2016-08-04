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


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SquadBoundryCheck();
    }

    void SquadBoundryCheck()
    {

        if (!m_squadinboundsX || !m_squadinboundsY)
        { 
            for(int i = 0; i <m_enemies.Count; i++)
            {
                if(!m_squadinboundsX)
                { 
                    if(m_enemies[i].m_InBoundsX)
                    {
                        m_squadinboundsX = true;
                        Debug.Log("Squad is within X boundry");
                        break;
                    }
                }

                if (!m_squadinboundsY)
                {
                    if(m_enemies[i].m_InBoundsY)
                    {
                        m_squadinboundsY = true;
                        Debug.Log("Squad is within Y boundry");
                        break;
                    }
                }
            }
            if (!m_squadinboundsX)
            {
                MoveSquad(true);
            }

            if (!m_squadinboundsY)
            {
                MoveSquadDown(true);
            }
        }
    }

    public void CreateSquad(string p_EnemyPrefabName, int p_Rows, int p_Columns)
    {
        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        GameObject t_EnemyPrefab = AssetManager.m_Instance.GetPrefab(p_EnemyPrefabName);

        bool t_switchside = false;

        Vector2 t_origin = Vector2.zero + new Vector2(-7, 7);
        Vector2 t_currentspawnPOS;
        Vector2 t_previousspawnPOS = t_origin;

        float t_paddingx = 0f;
        float t_paddingy = 0.1f;

        m_spawnedenemiescount = p_Rows * p_Columns;
        for (int i = 0; i < p_Rows; i++)
        {
            for (int j = 0; j < p_Columns; j++)
            {
                Enemy t_enemy = (Instantiate(t_EnemyPrefab)).GetComponent<Enemy>();
                t_enemy.tag = "Enemy";

                float t_posx = t_enemy.m_HalfSize.x * 2;
                float t_posy = t_previousspawnPOS.y - ((t_enemy.m_HalfSize.y * 2) * i) - (i * t_paddingy);

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

                t_enemy.transform.position = t_currentspawnPOS;
                t_previousspawnPOS = new Vector2(t_currentspawnPOS.x, t_previousspawnPOS.y);


                if (p_Rows == 1 && p_Columns == 1)
                {
                    //t_enemy.m_SoleSurvivor = true;
                }

                t_enemy.m_CurrentEnemySquad = this;
                m_enemies.Add(t_enemy);
               
                
            }

            t_previousspawnPOS = new Vector2(t_origin.x, t_previousspawnPOS.y);
        }
    }

    public void MoveSquadDown(bool p_Value)
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_killall)
                break;
            else
            {
                m_enemies[i].m_movedown = p_Value;
            }
        }
    }

    public void MoveSquad(bool p_Value)
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_killall)
                break;
            else
            {
                m_enemies[i].m_move = p_Value;
            }
        }
    }

    public void IncreaseSquadSpeed()
    {
        float t_newmovementtimer = (float)(m_enemies.Count - 1) / m_spawnedenemiescount;
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_killall)
                break;
            else
            {
                
                if (m_enemies.Count > 1)
                {
                    if(m_enemies[i].m_Alive)
                    {
                        m_enemies[i].m_movementtimer *= t_newmovementtimer;
                        m_enemies[i].m_timer = m_enemies[i].m_movementtimer;
                    }
                }
                else
                {
                    m_enemies[i].BecomeSoleSurvivor();
                }
            }
        }
    }

    public void RemoveEnemyFromSquad(Enemy p_DeadEnemy)
    {
        m_enemies.Remove(p_DeadEnemy);
        SquadCheck();
    }

    public void KillAllInSquad()
    {
        m_killall = true;
        for(int i = 0;i<m_enemies.Count;i++)
        {
            Destroy(m_enemies[i].gameObject);
        }
        SquadCheck();
    }

    void SquadCheck()
    {
        if (m_enemies.Count == 0)
        {
            m_issquaddead = true;
            Destroy(gameObject);
        }
    }
}


