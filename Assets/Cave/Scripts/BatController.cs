using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour {

	CaveGameManager gameManager;
	public AudioSource flapSound;
	public GameObject wavePrefab;
	private Rigidbody2D body2D;
	public Vector2 waveOffset;
	public float screamingPeriod;
	float screamingTimer;
	public GameObject shadow;
	SpriteRenderer shadowRender;
	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> (); 
		gameManager = GameObject.FindObjectOfType<CaveGameManager> ();
		shadowRender = shadow.GetComponent<SpriteRenderer> ();
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Rock")
			gameManager.GameOver ();
	}

	// Update is called once per frame
	void Update () {
		if (screamingTimer > 0) {
			screamingTimer -= Time.deltaTime;
		} else {
			Scream ();
		}
			
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.touches.Length>0 && Input.touches[0].phase == TouchPhase.Began) {
			if (gameManager.gameStarted)
				Rise ();
			else {
				body2D.isKinematic = false;
				gameManager.StartGame ();
			}
		}
	}

	void Rise()
	{
		if (!gameManager.gameStarted)
			return;
		if (!gameManager.gameIsOver) {
			body2D.velocity = new Vector2 (body2D.velocity.x, 4.2f);
			flapSound.PlayOneShot (flapSound.clip);
		}
	}

	void Scream(){
		if (gameManager.gameIsOver)
			return;
		shadowRender.color *= 0.95f;
		GenerateWave ();
		screamingTimer = screamingPeriod;
	}

	void GenerateWave()
	{
		Instantiate (wavePrefab, new Vector2(transform.position.x+waveOffset.x,transform.position.y+waveOffset.y), Quaternion.identity);
	}

	void FixedUpdate(){
		var angle = 0f;
		if (body2D.velocity.y < 0) {
			angle = Mathf.Lerp (0, -90, -body2D.velocity.y/10);
		}
		transform.rotation = Quaternion.Euler (0, 0, angle);
	}
}
