using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoSplash : MonoBehaviour {

    public float Timer = 0;
    public float TimeOut = 5;
    public Transition transition;
    bool transitionBegun = false;
    float transitionTimeOut;
	// Use this for initialization
	void Start () {
        transitionTimeOut = TimeOut - 2;
    }
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (!transitionBegun && Timer >= transitionTimeOut)
        {
            transition.Reset(false, Color.black, Color.black);
            transition.StartTransition();
            transitionBegun = true;
        }

        if (Timer < TimeOut) return;

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
