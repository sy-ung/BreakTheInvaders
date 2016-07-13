using UnityEngine;

class BallBullet : Bullet
{
    void Awake()
    {
        p_SpriteName = "BallBullet";
        base.Awake();
    }
}

