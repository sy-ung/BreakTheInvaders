using UnityEngine;
using System.Collections;

public class EnemySpecialDeath : Death {

    void Awake()
    {
        base.Awake();
        m_DeathsoundClipName = new string[1] { "EnemySpecialDeath" };
    }

    void Start()
    {
        //base.Start();
        m_particlesystem.loop = false;
        m_particlesystem.Play();

        if (m_DeathsoundClipName != null)
            GameAudioManager.m_Instance.PlaySound(m_DeathsoundClipName[Random.Range(0, m_DeathsoundClipName.Length)], false, 1.0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
