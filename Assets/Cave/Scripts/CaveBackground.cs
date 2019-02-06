using UnityEngine;
using System.Collections;

public class CaveBackground : MonoBehaviour {
	Renderer rend;
	float offset;
	public float speed;
	// Use this for initialization
	void Start () {
		rend = GetComponent <Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		offset += speed;
		rend.material.mainTextureOffset = new Vector2 (offset, 0);
	}
}
