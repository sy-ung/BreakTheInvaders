using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {


    private bool m_killall = false;

    int m_spawnedenemiescount;

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

    //Will spawn a random enemy squad at random sizes
    public void SpawnSquad(float p_DifficultyLevel)
    {
        EnemySquad t_es = new GameObject("ENEMY SQUAD").AddComponent<EnemySquad>();
        int t_randenemy = Random.Range(1, 8);
        t_es.CreateSquad("EnemyType" + t_randenemy, Random.Range(2, 5), Random.Range(2, 6), p_DifficultyLevel);
    }

    public void SpawnSpecialEnemy()
    {
        EnemySquad t_es = new GameObject("ENEMY SQUAD").AddComponent<EnemySquad>();
        t_es.CreateSpecialEnemy();
    }


    public void SpawnSquad(string p_EnemyPrefabName, int p_Rows, int p_Coloumns, float p_DifficultyLevel)
    {
        EnemySquad t_es = new GameObject("EnemySquad").AddComponent<EnemySquad>();
        t_es.CreateSquad(p_EnemyPrefabName, p_Rows, p_Coloumns, p_DifficultyLevel);
    }


    public void KillAllEnemies()
    {
        Enemy[] t_allenemies = GameObject.FindObjectsOfType<Enemy>();
        m_killall = true;
        for(int i = 0; i<t_allenemies.Length; i++)
        {
            Destroy(t_allenemies[i].gameObject);
        }

    }
}
