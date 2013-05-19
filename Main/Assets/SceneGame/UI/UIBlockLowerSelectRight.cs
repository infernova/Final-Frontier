using UnityEngine;
using System.Collections;

public class UIBlockLowerSelectRight : GUITextureHelper {

	public static UIBlockLowerSelectRight MyInstance;
	public static UIBlockLowerSelectRight Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIBlockLowerSelectRight)FindObjectOfType(typeof(UIBlockLowerSelectRight));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
	string tower;
	int level;
	public void showAndSet(string tower, int level) {
		this.tower = tower;
		this.level = level;
		if (level == 3)
			guiTexture.texture = PrefabManager.UpgradeDetails["UpgradeNone"];
		else
			guiTexture.texture = PrefabManager.UpgradeDetails[tower+level.ToString()];
	}
	public void upgrade() {			
		showAndSet(tower,Mathf.Clamp(level+1,1,3));	
	}
	public void hide() {
		guiTexture.texture = PrefabManager.Invisible;		
	}
}
