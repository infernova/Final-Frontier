using UnityEngine;
using System.Collections;

public class UIPauseMenu : GUITextureHelper {
	public bool visible = false;
	public static UIPauseMenu MyInstance;
	public static UIPauseMenu Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIPauseMenu)FindObjectOfType(typeof(UIPauseMenu));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
		guiTexture.pixelInset = Constants.offScreenRect;
	}
	
}
