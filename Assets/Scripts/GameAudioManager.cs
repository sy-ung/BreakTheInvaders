using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameAudioManager : MonoBehaviour {



    private static GameAudioManager m_instance;

    private static Hashtable m_AudioClips;

    private static Dictionary<string,List<SoundClip>> m_currentsoundsplaying;


    int m_MaxAllowedSounds = 1;

    public static GameAudioManager m_Instance
    {
        get
        {
            if(m_instance == null)
            {
                GameAudioManager audioManager = FindObjectOfType<GameAudioManager>();
                if (audioManager == null)
                {
                    m_instance = new GameObject("GameAudioManager").AddComponent<GameAudioManager>();
                }
                else
                    m_instance = audioManager;
            }

            return m_instance;
        }
    }

    void Awake()
    {
        m_AudioClips = new Hashtable();

        m_currentsoundsplaying = new Dictionary<string, List<SoundClip>>();

        DontDestroyOnLoad(gameObject);

        LoadSoundAssets();
    }

    void LoadSoundAssets()
    {
        m_AudioClips.Add("BallPlayerHit", Resources.Load<AudioClip>("Audio/BallHit/PlayerBallHit"));
        m_AudioClips.Add("BallWallHit", Resources.Load<AudioClip>("Audio/BallHit/WallBallHit"));

        m_AudioClips.Add("EnemyDeath1", Resources.Load<AudioClip>("Audio/DeathSound/EnemyDeath1"));
        m_AudioClips.Add("EnemyDeath2", Resources.Load<AudioClip>("Audio/DeathSound/EnemyDeath2"));
        m_AudioClips.Add("EnemySpecialDeath", Resources.Load<AudioClip>("Audio/DeathSound/EnemySpecialDeath"));
        m_AudioClips.Add("PlayerDeath", Resources.Load<AudioClip>("Audio/DeathSound/PlayerDeath"));

        m_AudioClips.Add("EnemyMove1", Resources.Load<AudioClip>("Audio/EnemyMovement/Move1"));
        m_AudioClips.Add("EnemyMove2", Resources.Load<AudioClip>("Audio/EnemyMovement/Move2"));
        m_AudioClips.Add("EnemyMoveSpecial", Resources.Load<AudioClip>("Audio/EnemyMovement/MoveSpecial"));

        m_AudioClips.Add("PlayerHit", Resources.Load<AudioClip>("Audio/Player/PlayerHit"));
        m_AudioClips.Add("PlayerHeal", Resources.Load<AudioClip>("Audio/Player/PlayerHeal"));

        m_AudioClips.Add("PowerUpHitPlayer", Resources.Load<AudioClip>("Audio/PowerUpHit/PlayerPowerUpHit"));
        m_AudioClips.Add("PowerUpHitBall", Resources.Load<AudioClip>("Audio/PowerUpHit/BallPowerUpHit"));

        m_AudioClips.Add("Enemy1Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy1Fire"));
        m_AudioClips.Add("Enemy2Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy2Fire"));
        m_AudioClips.Add("Enemy3Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy3Fire"));
        m_AudioClips.Add("Enemy4Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy4Fire"));
        m_AudioClips.Add("Enemy5Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy5Fire"));
        m_AudioClips.Add("Enemy6Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy6Fire"));
        m_AudioClips.Add("Enemy7Fire", Resources.Load<AudioClip>("Audio/WeaponFire/Enemy/Enemy7Fire"));

        m_AudioClips.Add("BasicMuzzleFire1", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Basic/BasicFire1"));
        m_AudioClips.Add("BasicMuzzleFire2", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Basic/BasicFire2"));
        m_AudioClips.Add("BasicMuzzleFire3", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Basic/BasicFire3"));

        m_AudioClips.Add("BeamFireChargeUp", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Beam/ChargeUp"));
        m_AudioClips.Add("BeamFireChargeDown", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Beam/ChargeDown"));
        m_AudioClips.Add("BeamFire1", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Beam/Beam1"));
        m_AudioClips.Add("BeamFire2", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Beam/Beam2"));
        m_AudioClips.Add("BeamFire3", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Beam/Beam3"));

        m_AudioClips.Add("BlueMuzzleFire", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Blue/BlueFire"));
        m_AudioClips.Add("BlueBulletBoost", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Blue/BlueBoost"));
        m_AudioClips.Add("BlueBulletExpolsion", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Blue/BlueExplosion"));

        m_AudioClips.Add("GreenMuzzleCharge", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Green/ChargeLoop"));
        m_AudioClips.Add("GreenMuzzleLaunch", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Green/Launch"));

        m_AudioClips.Add("Reload", Resources.Load<AudioClip>("Audio/WeaponFire/Player/Reload"));
    }

    public AudioClip GetSoundClip(string p_Name)
    {
        return (AudioClip)m_AudioClips[p_Name];
    }


	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {
	}

    public void PlaySound(string p_SoundName, bool p_Loop, float p_Pitch, bool p_Important)
    {
        if(!m_currentsoundsplaying.ContainsKey(p_SoundName))
        {
            m_currentsoundsplaying.Add(p_SoundName, new List<SoundClip>());
        }

        if (m_currentsoundsplaying[p_SoundName].Count >= m_MaxAllowedSounds)
        {
            Destroy(m_currentsoundsplaying[p_SoundName][0].gameObject);
            SoundFinished(p_SoundName);
        }

        SoundClip t_newSound = new GameObject("Soundclip_" + p_SoundName).AddComponent<SoundClip>();

        float t_volume = 1.0f;
        if(!p_Important)
        {
            t_volume = 0.175f;
        }

        t_newSound.LoadClip(p_SoundName, GetSoundClip(p_SoundName), p_Loop, p_Pitch, t_volume);

        m_currentsoundsplaying[p_SoundName].Add(t_newSound);
        
    }

    public void SoundFinished(string p_SoundName)
    {
        m_currentsoundsplaying[p_SoundName].RemoveAt(0);
    }

    public void StopSound(string p_SoundName)
    {
        int t_index = 0;
        if (m_currentsoundsplaying.ContainsKey(p_SoundName))
            t_index = m_currentsoundsplaying[p_SoundName].Count;

        if (t_index>0)
        {
            m_currentsoundsplaying[p_SoundName][0].StopPlaying();
        }
    }


}
