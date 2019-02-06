using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public AudioSource BackgroundMusic;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (BackgroundMusic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonPlayGameDown(){
		SceneManager.LoadScene("Home", LoadSceneMode.Single);
	}

	public void OnButtonShowInfoDown(){
		SceneManager.LoadScene("Info", LoadSceneMode.Single);
	}

	public void OnButtonExitGameDown(){
		Application.Quit ();
	}
}
