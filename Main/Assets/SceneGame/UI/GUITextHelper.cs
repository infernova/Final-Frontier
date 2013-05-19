using UnityEngine;
using System.Collections;

public class GUITextHelper : MonoBehaviour {
	bool isResized = false;
	
	// Update is called once per frame
	void LateUpdate () {
		if (!isResized) {
			float ratio = Constants.ratio;
			Vector2 old = guiText.pixelOffset;
			guiText.pixelOffset = new Vector2(ratio*old.x,ratio*old.y);					
			isResized = true;	
		}
	}
}
