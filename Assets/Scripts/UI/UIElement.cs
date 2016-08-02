﻿using UnityEngine;


public class UIElement : MonoBehaviour {

    // Use this for initialization
    protected SpriteRenderer m_spriterenderer;

    public Vector2 m_HalfSize
    {
        get { return m_spriterenderer.bounds.size/2; }
    }

    protected void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_spriterenderer = gameObject.AddComponent<SpriteRenderer>();
    }

	void Start ()
    {
	
	}

    public void SetScale(float p_ScaleFactor)
    {
        transform.localScale *= p_ScaleFactor;
    }

    public void SetSprite(string p_SpriteName)
    {
        m_spriterenderer.sprite = AssetManager.m_Instance.GetSprite(p_SpriteName);
    }
	
	// Update is called once per frame
	protected void Update ()
    {
	
	}
}