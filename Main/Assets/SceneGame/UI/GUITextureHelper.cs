using UnityEngine;
using System.Collections;

public class GUITextureHelper : PausableMonoBehaviour {
	public bool needsResizing = true;
	void LateUpdate () {
		if (needsResizing) {
			guiTexture.pixelInset = Constants.getRectToScreen(guiTexture.pixelInset);
			needsResizing = false;	
		}
	}
}
