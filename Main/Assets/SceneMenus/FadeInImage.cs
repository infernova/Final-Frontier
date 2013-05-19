using UnityEngine;
using System.Collections;

public class FadeInImage : GameButton {
	bool visible = false;
	protected float fadeInTime = 2f;
	protected override bool IsEnabled() {
		return visible;
	}
	
	// Use this for initialization
	void Start () {
		guiTexture.color = new Color(0.5f,0.5f,0.5f,0f);
		iTween.ValueTo(gameObject,iTween.Hash("from",0,
			"to",0.5f,
			"onupdate","adjustAlpha",
			"time",fadeInTime));
	}
	
	void adjustAlpha(float n) {
		guiTexture.color = new Color(0.5f,0.5f,0.5f,n);
		if (n > 0.04f)
			visible = true;
	}
}
