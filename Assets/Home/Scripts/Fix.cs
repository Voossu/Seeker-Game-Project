using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.T)) {
			SceneManager.LoadScene("Tower", LoadSceneMode.Single);
			return;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.C)) 
		{
			SceneManager.LoadScene("Cave", LoadSceneMode.Single);
			return;
		}


	}
}
