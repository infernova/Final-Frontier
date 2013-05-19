using UnityEngine;
using System.Collections;

public class UIPauseMenuResume : GameButton {
	public static UIPauseMenuResume MyInstance;
	public static UIPauseMenuResume Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIPauseMenuResume)FindObjectOfType(typeof(UIPauseMenuResume));
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
		ButtonPauseMenu.Instance.ButtonDown();
	}
}
