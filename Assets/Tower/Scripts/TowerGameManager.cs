using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerGameManager : MonoBehaviour {

	public GameObject endUI; // End UI
	public Text endMessage; // End of Game Message
	public static TowerGameManager instance;
	private EnemySpawner enemySpawner; // enemy incubator

	void Awake()
	{
		instance = this;
		enemySpawner = GetComponent<EnemySpawner>();
	}

	public void Win()
	{
		endUI.SetActive(true);
		endMessage.text = "Victory";
	}

	public void Failed()
	{
		endUI.SetActive(true);
		endMessage.text = "Lost";
		// stop generating enemies
		enemySpawner.Stop();
	}

	// Replay
	public void OnButtonRetryDown()
	{
		// reload the game scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	// menu
	public void OnButtonHomeDown()
	{
		// Load the menu scene
		SceneManager.LoadScene("Home", LoadSceneMode.Single);
	}

}
