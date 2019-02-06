using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaveGameManager : MonoBehaviour {

	public GameObject blank;
	public GameObject gameoverPanel;
	SpriteRenderer blankRenderer;
	public float alphaSpeed = 0.01f;
	private CaveRocksGen rocksGen;
	public bool gameIsOver;
	public bool gameStarted = false;
	public Text infoText;
	int score;
	int bestScore;
	float scoreTimer;
	public Text scoreText;
	public Text finalScoreText;
	public Text bestText;
	// Use this for initialization
	void Start () {
		rocksGen = GameObject.FindObjectOfType<CaveRocksGen> ();
		blankRenderer = blank.GetComponent<SpriteRenderer> ();

		if (!PlayerPrefs.HasKey ("SavedHiScore"))
			PlayerPrefs.SetInt ("SavedHiScore", bestScore);
		else
			bestScore = PlayerPrefs.GetInt ("SavedHiScore");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameIsOver && blankRenderer.color.a < 1) {
			blankRenderer.color = new Color (1, 1, 1, blankRenderer.color.a + alphaSpeed);
		}
		if (gameIsOver || !gameStarted)
			return;
		scoreTimer += Time.deltaTime;
		score = (int)(scoreTimer * 2);
		scoreText.text = score.ToString ();
	}

	public void OnButtonRetryDown()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	public void OnButtonHomeDow()
	{
		SceneManager.LoadScene("Home", LoadSceneMode.Single);
	}

	public void ShowGameOver()
	{
		gameoverPanel.SetActive (true);
	}

	public void GameOver()
	{
		if (gameIsOver)
			return;
		gameIsOver = true;
		rocksGen.StopGenerating ();
		if (score > bestScore) {
			bestScore = score;
			bestText.text = bestScore.ToString ();
			PlayerPrefs.SetInt ("SavedHiScore", bestScore);
		} else
			bestText.text = bestScore.ToString ();
		finalScoreText.text = scoreText.text;
		ShowGameOver ();
	}

	public void StartGame(){
		Destroy (infoText);
		gameStarted = true;
	}
}
