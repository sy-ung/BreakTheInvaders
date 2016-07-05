using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    private Enemy m_enemytype1;

    private static EnemyManager m_instance;
    public static EnemyManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<EnemyManager>();
                if(m_instance == null)
                {
                    GameObject t_enemymanager = new GameObject("EnemyManager");
                    m_instance = t_enemymanager.AddComponent<EnemyManager>();
                }
            }

            return m_instance;
        }
    }

    void Awake()
    {
        //GameObject t_enemy = new GameObject("EnemyType1");
        //m_enemytype1 = t_enemy.AddComponent<Enemy>();
    }

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        Enemy t_enemycheck = FindObjectOfType<Enemy>();
        if(t_enemycheck == null)
        {
            SpawnEnemies(5, 11);
        }
	}

    public void SpawnEnemies(int p_Rows, int p_Columns)
    {
        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        bool t_switchside = false;
        Vector2 t_currentspawnPOS;
        Vector2 t_previousspawnPOS = Vector2.zero;
        float t_paddingx = 0.05f;
        float t_paddingy = 0.1f;
        for (int i = 0; i < p_Rows;i++)
        { 
            for (int j = 0; j < p_Columns; j++)
            {
                Enemy t_enemy = (new GameObject("EnemyType1")).AddComponent<Enemy>();
                t_enemy.tag = "Enemy";

                float t_posy = ((t_screensize.y - t_enemy.m_HalfSize.y)) - ((t_enemy.m_HalfSize.y * 2) * i) - (i*t_paddingy);
                float t_posx = ((t_enemy.m_HalfSize.x * 2) + t_paddingx);

                if (t_switchside)
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x + t_posx * j), t_posy);
                    //t_currentspawnPOS = new Vector2((t_previousspawnPOS.x + ((t_enemy.m_HalfSize.x * 2) + t_paddingx) * j), t_posy);
                    t_switchside = false;
                }
                else
                {
                    t_currentspawnPOS = new Vector2((t_previousspawnPOS.x - t_posx * j), t_posy);
                    //t_currentspawnPOS = new Vector2((t_previousspawnPOS.x - ((t_enemy.m_HalfSize.x * 2) + t_paddingx) * j), t_posy);
                    t_switchside = true;
                }

                t_enemy.transform.position = t_currentspawnPOS;
                t_previousspawnPOS = t_currentspawnPOS;
            }
            t_previousspawnPOS = Vector2.zero;
        }
    }
}
