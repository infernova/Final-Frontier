using UnityEngine;
using System.Collections;

public class ClickToContinue : GameButton {
	float transitionTime = 0.7f;
	public string level = "menu";
	bool ready = false;
	public float startDelay = 2f;
	// Use this for initialization
	void Awake() {
		Time.timeScale = 1;	
	}
	
	void Start () {
		guiTexture.color = new Color(0.5f,0.5f,0.5f,0f);
		Invoke("fadeIn",startDelay);
	}
	
	void fadeIn() {
		ready = true;
		iTween.ColorTo(gameObject,iTween.Hash("a",0.5f,
											"time",transitionTime,
											"oncomplete","fadeOut"));
	}
	void fadeOut() {
		iTween.ColorTo(gameObject,iTween.Hash("a",0f,
											"time",transitionTime,
											"oncomplete","fadeIn"));
	}
	
	public override void ButtonDown() {
		if (ready)
			Constants.GoToScene(level);
	}
}
