using UnityEngine;
using System.Collections;

public class UIPauseMask : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		if (PausableMonoBehaviour.isPaused) {
			guiTexture.pixelInset = Constants.getRectToScreen(-512,-384,1024,768);
		} else {
			guiTexture.pixelInset = Constants.offScreenRect;	
		}
	}
}
