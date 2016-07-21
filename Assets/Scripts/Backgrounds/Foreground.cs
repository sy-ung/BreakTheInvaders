using UnityEngine;
using System.Collections;

public class Foreground : MonoBehaviour {


    public float m_Speed = 0.1f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 t_offset = new Vector2(Time.time * m_Speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = t_offset;
       
	}
}
