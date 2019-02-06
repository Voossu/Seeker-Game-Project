using UnityEngine;
using System.Collections;

public class CaveRock : MonoBehaviour {
	public float speed;
	Rigidbody2D body2D;
	public CaveRockOutline outline;
	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		body2D.velocity = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "wave") {
			outline.Glow ();
		}
	}
}
