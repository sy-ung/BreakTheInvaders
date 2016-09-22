using UnityEngine;


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
        m_spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        m_spriterenderer.sortingLayerName = "UI";
        m_spriterenderer.sortingOrder = 2;
        gameObject.layer = 5;
    }

	protected void Start ()
    {
	    
	}

    public void SetScale(float p_ScaleFactor)
    {
        transform.localScale *= p_ScaleFactor;
    }

    public void SetSprite(Sprite p_SpriteName)
    {
        m_spriterenderer.sprite = p_SpriteName;
    }
	
	// Update is called once per frame
	protected void Update ()
    {
	
	}
}
