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
        m_prefabs.Add("HealthBar", Resources.Load<GameObject>("Prefabs/GameObjects/HealthBox"));
        m_prefabs.Add("Ammo", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/Ammo"));
        m_prefabs.Add("AmmoBar", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/AmmoBar"));
        m_prefabs.Add("InfinitySign", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/Player/InfinitySign"));
        m_prefabs.Add("NoAmmoSign", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/Player/NoAmmoSign"));
        m_prefabs.Add("PlayerDeathParticle", Resources.Load<GameObject>("Prefabs/Particles/PlayerDeathParticle/PlayerDeathParticle"));

        m_prefabs.Add("DefaultBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/DefaultBall"));
        m_prefabs.Add("GreenBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/GreenBall"));
        m_prefabs.Add("BlueBall", Resources.Load<GameObject>("Prefabs/Gameobjects/Balls/BlueBall"));
        m_prefabs.Add("RedBall", Resources.Load<GameObject>("Prefabs/GameObjects/Balls/RedBall"));
        m_prefabs.Add("BallDeathParticle", Resources.Load<GameObject>("Prefabs/Particles/BallDeathParticle/BallDeathParticle"));

        m_prefabs.Add("RedPower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/RedPowerUp"));
        m_prefabs.Add("GreenPower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/GreenPowerUp"));
        m_prefabs.Add("BluePower", Resources.Load<GameObject>("Prefabs/GameObjects/PowerUps/BluePowerUp"));

        m_prefabs.Add("EnemyType1", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType1"));
        m_prefabs.Add("EnemyType2", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType2"));
        m_prefabs.Add("EnemyType3", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType3"));
        m_prefabs.Add("EnemyType4", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType4"));
        m_prefabs.Add("EnemyType5", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType5"));
        m_prefabs.Add("EnemyType6", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType6"));
        m_prefabs.Add("EnemyType7", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyType7"));
        m_prefabs.Add("EnemyTypeSpecial", Resources.Load<GameObject>("Prefabs/GameObjects/Enemies/EnemyTypeSpecial"));



        m_prefabs.Add("ControlWheel", Resources.Load<GameObject>("Prefabs/UIObjects/ControlWheel"));
        m_prefabs.Add("ControlBar", Resources.Load<GameObject>("Prefabs/UIObjects/ControlBar"));
        m_prefabs.Add("ShootButton", Resources.Load<GameObject>("Prefabs/UIObjects/ShootButton"));


        m_prefabs.Add("BasicMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/BasicMuzzle"));
        m_prefabs.Add("BulletDefault", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/BulletBasic"));
        m_prefabs.Add("BulletDefaultExplosion", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/Player/BulletBasicExplosion"));

        m_prefabs.Add("BlueMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/BlueMuzzle"));
        m_prefabs.Add("BulletBlue", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/BulletBlue"));
        m_prefabs.Add("BulletBlueExplosion", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/BulletBlueExplosion"));

        m_prefabs.Add("GreenMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/GreenMuzzle"));
        m_prefabs.Add("BulletGreen", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/BulletGreen"));
        m_prefabs.Add("BulletGreenExplosion", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Player/BulletGreenExplosion"));

        m_prefabs.Add("MuzzleDeathParticle", Resources.Load<GameObject>("Prefabs/Particles/MuzzleDeathParticle/MuzzleDeathParticle"));


        m_prefabs.Add("RedBeamMuzzle", Resources.Load<GameObject>("Prefabs/Gameobjects/Muzzles/RedBeamMuzzle"));
        m_prefabs.Add("BulletRedBeam", Resources.Load<GameObject>("Prefabs/Gameobjects/Bullets/Player/BulletRedBeam"));

        m_prefabs.Add("EnemyDeathParticle1", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle1/EnemyDeathParticle1"));
        m_prefabs.Add("EnemyDeathParticle2", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle2/EnemyDeathParticle2"));
        m_prefabs.Add("EnemyDeathParticle3", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle3/EnemyDeathParticle3"));
        m_prefabs.Add("EnemyDeathParticle4", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle4/EnemyDeathParticle4"));
        m_prefabs.Add("EnemyDeathParticle5", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle5/EnemyDeathParticle5"));
        m_prefabs.Add("EnemyDeathParticle6", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle6/EnemyDeathParticle6"));
        m_prefabs.Add("EnemyDeathParticle7", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticle7/EnemyDeathParticle7"));
        m_prefabs.Add("EnemyDeathParticleSpecial", Resources.Load<GameObject>("Prefabs/Particles/EnemyDeathParticles/DeathParticleSpecial/EnemyDeathParticleSpecial"));


        m_prefabs.Add("MuzzleFlashDefault", Resources.Load<GameObject>("Prefabs/Particles/MuzzleFlash/MuzzleFlash"));

        m_prefabs.Add("EnemyBullet1", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet1"));
        m_prefabs.Add("EnemyBullet2", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet2"));
        m_prefabs.Add("EnemyBullet3", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet3"));
        m_prefabs.Add("EnemyBullet4", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet4"));
        m_prefabs.Add("EnemyBullet5", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet5"));
        m_prefabs.Add("EnemyBullet6", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet6"));
        m_prefabs.Add("EnemyBullet7", Resources.Load<GameObject>("Prefabs/GameObjects/Bullets/Enemy/EnemyBullet7"));


    }

    public GameObject GetPrefab(string p_PrefabName)
    {
        return (GameObject)m_prefabs[p_PrefabName];
    }
}
