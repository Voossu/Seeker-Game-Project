using UnityEngine;
using System.Collections;

public class CaveRockOutline : MonoBehaviour {

	SpriteRenderer rend;
	private Color defaultColor;
	public float alphaSpeed;
	// Use this for initialization
	void Start () {
		defaultColor = new Color (0.5f, 0.5f, 0.5f);
		rend = GetComponent<SpriteRenderer> ();
		rend.color = defaultColor*0;
	}
	
	// Update is called once per frame
	void Update () {
		if (rend.color.a > 0)
			rend.color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, rend.color.a - alphaSpeed);
	}

	public void Glow(){
		rend.color = defaultColor;
	}
}
