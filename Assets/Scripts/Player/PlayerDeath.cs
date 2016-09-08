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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
