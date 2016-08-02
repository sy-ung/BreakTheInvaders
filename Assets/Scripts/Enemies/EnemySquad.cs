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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void CreateSquad(string p_EnemyPrefabName, int p_Rows, int p_Columns)
    {
        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        GameObject t_EnemyPrefab = AssetManager.m_Instance.GetPrefab(p_EnemyPrefabName);

        bool t_switchside = false;
        Vector2 t_currentspawnPOS;
        Vector2 t_previousspawnPOS = Vector2.zero + new Vector2(0, 5);
        float t_paddingx = 0f;
        float t_paddingy = 0f;
        m_spawnedenemiescount = p_Rows * p_Columns;
        for (int i = 0; i < p_Rows; i++)
        {
            for (int j = 0; j < p_Columns; j++)
            {
                Enemy t_enemy = (Instantiate(t_EnemyPrefab)).GetComponent<Enemy>();
                t_enemy.tag = "Enemy";

                Vector2 t_enemyhalfsize = t_enemy.m_HalfSize;

                floa t_posy = ((t_screensize.y - t_enemyhalfsize.y)) + (((t_enemyhalfsize.y * 2) * i) - (i * t_enemy.m_HalfSize.y * t_paddingy));
                float t_posx = ((t_enemyhalfsize.x * 2) + t_enemy.m_HalfSize.y * t_paddingx);

                if (t_switchside)
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x + t_posx * j), t_posy);
                    t_switchside = false;
                }
                else
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x - t_posx * j), t_posy);
                    t_switchside = true;
                }

                t_enemy.transform.position = t_currentspawnPOS;
                t_previousspawnPOS = t_currentspawnPOS;

                if(p_Rows == 1 && p_Columns == 1)
                {
                    t_enemy.m_SoleSurvivor = true;
                }

                t_enemy.m_CurrentEnemySquad = this;
                m_enemies.Add(t_enemy);
               
                
            }
            t_previousspawnPOS = Vector2.zero;
        }
    }

    public void MoveSquadDown()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_killall)
                break;
            else
            {
                m_enemies[i].m_movedown = true;
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


