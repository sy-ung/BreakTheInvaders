﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private Image m_titleimage;

    private Image[] m_enemyimages;
    private bool m_animateframe1;
    private float m_animaterate;
    private float m_animatetimer;

    private bool m_flashwhite;
    private float m_flashrate;

    void Awake()
    {
        m_titleimage = GameObject.FindGameObjectWithTag("TitleImage").GetComponent<Image>();
        GameObject[] t_enemies = GameObject.FindGameObjectsWithTag("Enemy");
        m_enemyimages = new Image[2] { t_enemies[0].GetComponent<Image>(), t_enemies[1].GetComponent<Image>() };
        m_flashrate = 0.6f;
        m_animaterate = 0.25f;
    }


	//Use this for initialization
	void Start () {
        
	}

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

	
	// Update is called once per frame
	void Update ()
    {
        FlashingTitle();
        AnimateEnemy();
	}

    void AnimateEnemy()
    {
        if(m_animatetimer>m_animaterate)
        { 
            if(m_animateframe1)
            {
                m_enemyimages[0].enabled = true;
                m_enemyimages[1].enabled = false;
                m_animateframe1 = false;
            }
            else
            {
                m_enemyimages[0].enabled = false;
                m_enemyimages[1].enabled = true;
                m_animateframe1 = true;
            }
            m_animatetimer = 0;
        }
        else
        {
            m_animatetimer += Time.deltaTime;
        }
    }

    void FlashingTitle()
    {
        if(m_flashwhite)
        { 
            m_titleimage.color = Color.LerpUnclamped(m_titleimage.color, Color.white, m_flashrate);
            if (m_titleimage.color == Color.white)
                m_flashwhite = false;
        }
        else
        {
            m_titleimage.color = Color.LerpUnclamped(m_titleimage.color, Color.red, m_flashrate);
            if (m_titleimage.color == Color.red)
                m_flashwhite = true;
        }
    }
}
