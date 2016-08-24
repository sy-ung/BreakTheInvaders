using UnityEngine;
using System.Collections;

public class NoAmmo : MonoBehaviour {

    SpriteRenderer m_spriterenderer;
    public Vector2 m_NewLocalPosition;
    public Ammo m_ammoref;

    void Awake()
    {
        m_spriterenderer = GetComponent<SpriteRenderer>();
        m_spriterenderer.sortingLayerName = "UI";
        m_spriterenderer.sortingOrder = 1;

    }

	// Use this for initialization
	void Start ()
    {
        transform.localScale = new Vector2(3f, 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_NewLocalPosition = m_ammoref.transform.localPosition;
	    if((Vector2)transform.localPosition != m_NewLocalPosition)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, m_NewLocalPosition, 0.33f);
        }
	}
}
