using UnityEngine;
using System.Collections;

public class PlayerDeath : Death {

    void Awake()
    {
        base.Awake();
        m_DeathsoundClipName = new string[1] { "PlayerDeath" };
    }

    void Start()
    {
        //base.Start();
        m_particlesystem.loop = false;
        m_particlesystem.Play();
        GameAudioManager.m_Instance.PlaySound(m_DeathsoundClipName[Random.Range(0, m_DeathsoundClipName.Length)], false, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
