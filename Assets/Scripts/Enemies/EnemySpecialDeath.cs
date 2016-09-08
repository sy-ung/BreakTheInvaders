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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
