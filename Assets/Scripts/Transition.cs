using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

    public bool startDark = false;
    public float timerMultiplyer = 2f;
    public bool TransitionFinished{get; private set;}
    public bool TransitionStarted { get; private set; }
    public bool startTransitioningImmediately = true;
    public float minAlpha = 0;
    public float maxAlpha = 1;
    SpriteRenderer spriteRenderer;
    float timer = 0f;
    public Color startColor;
    public Color endColor;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = new Color(startColor.r, startColor.g, startColor.b, startDark ? maxAlpha : minAlpha);
        endColor = new Color(endColor.r, endColor.g, endColor.b, startDark ? minAlpha : maxAlpha);
        //HACK .. I Dunno Why This Is Happening -_-
        if (startDark)
            spriteRenderer.color = startColor;
        else spriteRenderer.material.color = startColor;

        if (startTransitioningImmediately)
            TransitionStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!TransitionStarted || TransitionFinished) return;
        timer += Time.deltaTime* timerMultiplyer;
        spriteRenderer.material.color = Color.Lerp(startColor, endColor, timer);
        if ((startDark && spriteRenderer.material.color.a <= minAlpha) || (!startDark && spriteRenderer.material.color.a >= maxAlpha))
            TransitionFinished = true;
    }

    public void Reset(bool startDark,Color startColor,Color endColor)
    {
        TransitionFinished = false;
        TransitionStarted = false;
        this.startDark = startDark;
        this.startColor = new Color(startColor.r, startColor.g, startColor.b, startDark ? maxAlpha : minAlpha);
        this.endColor = new Color(endColor.r, endColor.g, endColor.b, startDark ? minAlpha : maxAlpha);
        spriteRenderer.material.color = startColor;
        timer = 0;
    }

    public void StartTransition()
    {
        TransitionStarted = true;
    }
}
