using UnityEngine;
using System.Collections;

public class IPButton : GameButton{
	public string key = "k";
	float increaseSize = 10f;
	float pressTime = 0.2f;
	// Use this for initialization
	bool pressed = false;
	float oldW, oldH,oldCentreX, oldCentreY;
	void Start() {
		oldW = guiTexture.pixelInset.width;
		oldH = guiTexture.pixelInset.height;
		oldCentreX = guiTexture.pixelInset.x + oldW/2f;
		oldCentreY = guiTexture.pixelInset.y + oldH/2f;
	}
	
	public override void ButtonDown() {
		if (ClientNetworkController.Instance.verifyKey(key) && !pressed) {
			pressed = true;
			iTween.ValueTo(gameObject,iTween.Hash(
			"from",0,
			"to",1,
			"onupdate","resize",
			"time",pressTime,
			"oncomplete","ScaleBack"));
		}
	}
	void resize(float n) {
		float increase = n * -increaseSize;
		float newW = oldW + increase;
		float newH = oldH + increase;
		guiTexture.pixelInset = Constants.getRectToScreen(oldCentreX - newW/2f,
			oldCentreY - newH/2f ,newW,newH);
	}
	
	void finished() {
		pressed = false;	
	}
	
	void ScaleBack() {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",1,
			"to",0,
			"onupdate","resize",
			"time",pressTime,
			"oncomplete","finished"));
	}
}
