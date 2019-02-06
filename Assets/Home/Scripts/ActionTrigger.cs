using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionTrigger : MonoBehaviour {


	public GameObject TriggerMesh;
	public GameObject ActionHelper;
	public string SceneName;

//	bool inTrigger = false;


/*	void OnTriggerEnter(Collider other)
	{
		if (!inTrigger && other.tag == "Player") {
			ActionHelper?.SetActive(true);
			Debug.Log("Enter trigger");
			inTrigger = true;
		}
	}*/

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
		}	
	}

/*	void OnTriggerExit(Collider other)
	{
		if (inTrigger && other.tag == "Player") {
			ActionHelper.SetActive(false);
			Debug.Log("Exit trigger");
		}
	}
*/	
}
