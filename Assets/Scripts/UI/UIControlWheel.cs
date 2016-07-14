using UnityEngine;


class UIControlWheel : UIElement {

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        base.Awake();
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite("ControlWheel");
        SetScale(0.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();
	}
}
