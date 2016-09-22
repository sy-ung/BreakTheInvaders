using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public int m_Order;

    public Material m_BackgroundMaterial;

    private bool m_HorizontalScrollLeft;
    private bool m_HorizontalScrollRight;

    private bool m_VerticalScrollUp;
    private bool m_VerticalScrollDown;

    private float m_ScrollingSpeed;

    private int m_maxorder = 500;

    void Awake()
    {
        m_BackgroundMaterial = GetComponent<MeshRenderer>().material;
        Resize();
        m_ScrollingSpeed = 0.0075f;
    }

    void Resize()
    {
        float t_camsize = Camera.main.orthographicSize;
        transform.localScale = new Vector2(1, 1) * (t_camsize * 2);
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    public void GoIntoLayer(int p_OrderLayer)
    {
        Debug.Log("Moving into layer: " + p_OrderLayer);
        transform.position = new Vector3(0,0, m_maxorder - p_OrderLayer);
        Debug.Log(transform.position);
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



    void BackgroundMovement()
    {

        if (m_BackgroundMaterial.mainTextureOffset.x >= 1 || m_BackgroundMaterial.mainTextureOffset.x <= -1)
            m_BackgroundMaterial.mainTextureOffset = new Vector2(0, m_BackgroundMaterial.mainTextureOffset.y);

        if (m_BackgroundMaterial.mainTextureOffset.y >= 1 || m_BackgroundMaterial.mainTextureOffset.y <= -1)
            m_BackgroundMaterial.mainTextureOffset = new Vector2(m_BackgroundMaterial.mainTextureOffset.x, 0);


        if (m_HorizontalScrollLeft)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(m_ScrollingSpeed, 0);
        }
        else if (m_HorizontalScrollRight)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(-m_ScrollingSpeed, 0);
        }

        if (m_VerticalScrollUp)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(0, -m_ScrollingSpeed);
        }
        else if(m_VerticalScrollDown)
        {
            m_BackgroundMaterial.mainTextureOffset += new Vector2(0, m_ScrollingSpeed);
        }
    }

}
