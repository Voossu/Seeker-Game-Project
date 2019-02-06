using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	public string toScenes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(toScenes != null && Input.GetKeyDown(KeyCode.Backspace)) {
			SceneManager.LoadScene(toScenes, LoadSceneMode.Single);
		}
	}
}
