using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public int m_Order;

    public Material m_BackgroundMaterial;

    private bool m_HorizontalScrollLeft;
    private bool m_HorizontalScrollRight;

    private bool m_VerticalScrollUp;
    private bool m_VerticalScrollDown;

    private float m_maxSpeed;
    private float m_newscrollingspeed;
    private float m_ScrollingSpeed;
    private float m_interpspeed;

    private int m_maxorder = 500;

    void Awake()
    {
        m_BackgroundMaterial = GetComponent<MeshRenderer>().material;
        gameObject.tag = "Background";
        m_maxSpeed = 15.0f;

    }

    public void Resize(int p_ScaleFactor)
    {
        float t_camsize = Camera.main.orthographicSize;
        transform.localScale = new Vector2(1, 1) * (t_camsize * p_ScaleFactor);
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    public void GoIntoLayer(int p_OrderLayer)
    {
        transform.position = new Vector3(0,0, m_maxorder - p_OrderLayer);
    }
	
	// Update is called once per frame
	void Update ()
    {
        BackgroundMovement();
    }

    public void ScrollUp()
    {
        m_VerticalScrollUp = true;
        m_VerticalScrollDown = false;
    }

    public void ScrollDown()
    {
        m_VerticalScrollUp = false;
        m_VerticalScrollDown = true;
    }

    public void ScrollLeft()
    {
        m_HorizontalScrollLeft = true;
        m_HorizontalScrollRight = false;
    }

    public void ScrollRight()
    {
        m_HorizontalScrollLeft = false;
        m_HorizontalScrollRight = true;
    }

    public void SetScrollingSpeed(float p_NewSpeed, float p_InterpSpeed)
    {
        m_interpspeed = p_InterpSpeed;
        if (p_NewSpeed > m_maxSpeed)
            m_newscrollingspeed = m_maxSpeed;
        else
            m_newscrollingspeed = p_NewSpeed;
        
    }



    void BackgroundMovement()
    {

        if (m_BackgroundMaterial.mainTextureOffset.x >= 1 || m_BackgroundMaterial.mainTextureOffset.x <= -1)
            m_BackgroundMaterial.mainTextureOffset = new Vector2(0, m_BackgroundMaterial.mainTextureOffset.y);

        if (m_BackgroundMaterial.mainTextureOffset.y >= 1 || m_BackgroundMaterial.mainTextureOffset.y <= -1)
            m_BackgroundMaterial.mainTextureOffset = new Vector2(m_BackgroundMaterial.mainTextureOffset.x, 0);


        if (m_ScrollingSpeed != m_newscrollingspeed)
            m_ScrollingSpeed = Mathf.Lerp(m_ScrollingSpeed, m_newscrollingspeed, m_interpspeed);

        if (m_HorizontalScrollLeft)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(m_ScrollingSpeed * Time.deltaTime, 0);
        }
        else if (m_HorizontalScrollRight)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(-m_ScrollingSpeed * Time.deltaTime, 0);
        }

        if (m_VerticalScrollUp)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(0, -m_ScrollingSpeed * Time.deltaTime);
        }
        else if(m_VerticalScrollDown)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(0, m_ScrollingSpeed * Time.deltaTime);
        }
    }

}
