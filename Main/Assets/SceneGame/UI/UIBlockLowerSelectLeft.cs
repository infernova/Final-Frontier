using UnityEngine;
using System.Collections;

public class UIBlockLowerSelectLeft : GUITextureHelper {

	public static UIBlockLowerSelectLeft MyInstance;
	public static UIBlockLowerSelectLeft Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UIBlockLowerSelectLeft)FindObjectOfType(typeof(UIBlockLowerSelectLeft));
			}
			return MyInstance;
		}
	}
	string tower;
	int level;
	void Start() {
		MyInstance = this;
	}
	public void showAndSet(string tower, int level) {
		this.tower = tower;
		this.level = level;
		guiTexture.texture = PrefabManager.SellDetails[tower+level.ToString()];
	}
	public void upgrade() {
		showAndSet(tower,Mathf.Clamp(level+1,1,3));	
	}
	public void hide() {
		guiTexture.texture = PrefabManager.Invisible;	
	}
}
