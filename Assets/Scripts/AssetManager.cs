using UnityEngine;
//using UnityEditor;
using System.Collections;

public class AssetManager : MonoBehaviour {

    private static AssetManager m_instance;

    public static AssetManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<AssetManager>();
                if(m_instance == null)
                {
                    GameObject t_AssetManagerObject = new GameObject("AssetManagerSingleton");
                    m_instance = t_AssetManagerObject.AddComponent<AssetManager>();
                    LoadAllAssets();
                }
            }
            return m_instance;
        }
    }

    private static Hashtable m_prefabs;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private static void LoadAllAssets()
    {
        m_prefabs = new Hashtable();

        //Prefabs

        m_prefabs.Add("Player", Resources.Load<GameObject>("Prefabs/GameObjects/Player"));

        m_prefabs.Add("DefaultBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/DefaultBall"));
        m_prefabs.Add("GreenBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/GreenBall"));
        m_prefabs.Add("BlueBall", Resources.Load<GameObject>("Prefabs/Gameobjects/Balls/BlueBall"));
        m_prefabs.Add("RedBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/RedBall"));

        m_prefabs.Add("RedPower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/RedPowerUp"));
        m_prefabs.Add("GreenPower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/GreenPowerUp"));
        m_prefabs.Add("BluePower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/BluePowerUp"));

        m_prefabs.Add("EnemyType1", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/Enemy_Type1"));


        m_prefabs.Add("ControlWheel", Resources.Load<GameObject>("Prefabs/UIObjects/ControlWheel"));
        m_prefabs.Add("ControlBar", Resources.Load<GameObject>("Prefabs/UIObjects/ControlBar"));
        m_prefabs.Add("ShootButton", Resources.Load<GameObject>("Prefabs/UIObjects/ShootButton"));


        m_prefabs.Add("BasicMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/BasicMuzzle"));
        m_prefabs.Add("BulletDefault", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/BulletBasic"));
        m_prefabs.Add("BulletDefaultExplosion", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/BulletBasicExplosion"));

        m_prefabs.Add("BlueMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/BlueMuzzle"));
        m_prefabs.Add("BulletBlue", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/BulletBlue"));
        m_prefabs.Add("BulletBlueExplosion", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/BulletBlueExplosion"));

        m_prefabs.Add("GreenMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/GreenMuzzle"));
        m_prefabs.Add("BulletGreen", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/BulletGreen"));
        m_prefabs.Add("BulletGreenExplosion", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/BulletGreenExplosion"));

        m_prefabs.Add("RedBeamMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/RedBeamMuzzle"));
        m_prefabs.Add("BulletRedBeam", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/BulletRedBeam"));

        m_prefabs.Add("EnemyDeathParticle1", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticle1/EnemyDeathParticle1"));
        m_prefabs.Add("MuzzleFlashDefault", Resources.Load<GameObject>("Prefabs/Particles/MuzzleFlash/MuzzleFlash"));

        m_prefabs.Add("EnemyBullet", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/EnemyBullet"));
        

    }

    public GameObject GetPrefab(string p_PrefabName)
    {
        return (GameObject)m_prefabs[p_PrefabName];
    }
}
