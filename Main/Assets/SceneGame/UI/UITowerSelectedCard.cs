using UnityEngine;
using System.Collections;

public class UITowerSelectedCard : GUITextureHelper {
	public static UITowerSelectedCard MyInstance;
	public static readonly Rect onScreen = new Rect(-502,-380,85,125);
	string tower;
	int level;
	public static UITowerSelectedCard Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UITowerSelectedCard)FindObjectOfType(typeof(UITowerSelectedCard));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
	
	public void showAndSet(string tower, int level) {
		this.tower = tower;
		this.level = level;
		guiTexture.texture = PrefabManager.PrefabCards[tower+level.ToString()];
		guiTexture.pixelInset = onScreen;
		needsResizing = true;
	}
	public void upgrade() {
		showAndSet(tower,Mathf.Clamp(level+1,1,3));	
	}
	public void hide() {
		guiTexture.pixelInset = Constants.offScreenRect;	
	}
}
