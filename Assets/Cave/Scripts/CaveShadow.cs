using UnityEngine;
using System.Collections;

public class CaveShadow : MonoBehaviour {

	SpriteRenderer srender;
	public float alphaSpeed;
	// Use this for initialization
	void Start () {
		srender = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (srender.color.a < 1)
			srender.color = new Color (1, 1, 1, srender.color.a + alphaSpeed);
	}
}
