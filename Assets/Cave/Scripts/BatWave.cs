using UnityEngine;
using System.Collections;

public class BatWave : MonoBehaviour {

	public float speed;
	public float scaleSpeed;
	public float alphaSpeed;
	private SpriteRenderer WaveRenderer;
	private Rigidbody2D body2D;
	// Use this for initialization
	void Start () {
		WaveRenderer = GetComponent<SpriteRenderer> ();
		body2D = GetComponent<Rigidbody2D> ();
		body2D.velocity = new Vector2 (speed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector2 (transform.localScale.x, transform.localScale.y + scaleSpeed);
		WaveRenderer.color = new Color (0.7f, 0.7f, 0.7f, WaveRenderer.color.a - alphaSpeed);
		if (transform.position.x>=10)
			Destroy (this.gameObject);
	}
}
