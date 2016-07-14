using UnityEngine;


public class UIElement : MonoBehaviour {

    // Use this for initialization
    protected SpriteRenderer m_spriterenderer;

    private Vector2 m_halfsize;
    public Vector2 m_HalfSize
    {
        get { return m_halfsize; }
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
        transform.localScale = transform.localScale * p_ScaleFactor;
        m_halfsize = (m_spriterenderer.bounds.size / 2);
    }
	
	// Update is called once per frame
	protected void Update ()
    {
	
	}
}
