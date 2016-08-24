using UnityEngine;
using UnityEngine.UI;

class UIControlBar : UIElement {

    private Rigidbody2D m_rigidbody;
    private BoxCollider2D m_boxcollider2D;

    private Player m_player;

    private Vector2 m_previoustouchposition;

    void Awake()
    {
        base.Awake();

        Initialize();

        m_boxcollider2D.size = m_spriterenderer.bounds.size;
        m_rigidbody.isKinematic = true;
        m_player = PlayerManager.m_Instance.m_Player;
        SetScale(1);

        gameObject.tag = "ControlBox";
    }

    void Initialize()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_boxcollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

	// Update is called once per frame
	void Update () {

    }

    public void OnTouch(Vector3 p_TouchScreenPosition, TouchPhase p_Phase)
    {
        if(p_Phase == TouchPhase.Began)
            m_previoustouchposition = Camera.main.ScreenToWorldPoint(p_TouchScreenPosition);

        MovePlayer(p_TouchScreenPosition);
    }
    void DebugStuff(string p_Message)
    {
        GameObject.FindGameObjectWithTag("DebugBox").GetComponent<Text>().text = p_Message;
    }

    void MovePlayer(Vector2 p_TouchScreenCoord)
    {
        Vector2 t_currenttouchposition = Camera.main.ScreenToWorldPoint(p_TouchScreenCoord);
        m_player.MovePlayer(new Vector2((t_currenttouchposition.x - m_previoustouchposition.x) * 10, 0));
        m_previoustouchposition = t_currenttouchposition;
    }
}
