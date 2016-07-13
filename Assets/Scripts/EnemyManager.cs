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

    }

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

	}

    public void SpawnEnemies(int p_Rows, int p_Columns, string p_EnemyTypeName)
    {
        Vector2 t_screensize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        GameObject t_enemyobject = AssetManager.m_Instance.GetPrefab(p_EnemyTypeName);


        bool t_switchside = false;
        Vector2 t_currentspawnPOS;
        Vector2 t_previousspawnPOS = Vector2.zero - new Vector2(0,0);
        float t_paddingx = 0.05f;
        float t_paddingy = 0.1f;
        for (int i = 0; i < p_Rows;i++)
        { 
            for (int j = 0; j < p_Columns; j++)
            {
                Enemy t_enemy = (Instantiate(t_enemyobject)).GetComponent<Enemy>();
                t_enemy.tag = "Enemy";

                Vector2 t_enemyhalfsize = t_enemy.m_HalfSize;

                float t_posy = ((t_screensize.y - t_enemyhalfsize.y)) - ((t_enemyhalfsize.y * 2) * i) - (i*t_paddingy);
                float t_posx = ((t_enemyhalfsize.x * 2) + t_paddingx);

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
            }
            t_previousspawnPOS = Vector2.zero;
        }
    }

    public void KillAllEnemies()
    {
        Enemy[] t_allenemies = GameObject.FindObjectsOfType<Enemy>();

        for(int i = 0; i<t_allenemies.Length; i++)
        {
            Destroy(t_allenemies[i].gameObject);
        }

    }
}
