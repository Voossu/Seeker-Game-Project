using UnityEngine;
using System.Collections;

public class CaveRocksGen : MonoBehaviour {

	public GameObject[] prefabsList;
	CaveGameManager gameManager;
	public float generationPeriod;
	float generationTimer;
	bool canGenerate = true;
	void Start () {
		gameManager = GameObject.FindObjectOfType<CaveGameManager> ();
	}

	void CreatePrefab(){
		if (!canGenerate)
			return;
		var prefabIndex = Random.Range (0, prefabsList.Length);
		GameObject clone = Instantiate(prefabsList[prefabIndex]) as GameObject;
		UnityEngine.Behaviour.Destroy (clone, 15);
		generationTimer = generationPeriod;
	}

	// Update is called once per frame
	void Update () {
		if (!gameManager.gameStarted)
			return;
		if (generationTimer > 0)
			generationTimer -= Time.deltaTime;
		else
			CreatePrefab ();
	}

	public void StopGenerating(){
		canGenerate = false;
	}
}