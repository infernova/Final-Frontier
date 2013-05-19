using UnityEngine;
using System.Collections;

public class UIPauseMenuQuit : GameButton {
	public static UIPauseMenuQuit MyInstance;
	public static UIPauseMenuQuit Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIPauseMenuQuit)FindObjectOfType(typeof(UIPauseMenuQuit));
			}
			return MyInstance;
		}
	}
	public Rect baseRect;
	void Start() {
		MyInstance = this;
		baseRect = guiTexture.pixelInset;
		needsResizing = false;
		guiTexture.pixelInset = Constants.offScreenRect;
	}
	public override void ButtonDown() {
		Time.timeScale = 1;
		Constants.GoToScene("menu");
	}
}
