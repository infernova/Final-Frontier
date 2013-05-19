using UnityEngine;
using System.Collections;

public class UITowerUpgradeCard : GUITextureHelper {
	public static UITowerUpgradeCard MyInstance;
	public static readonly Rect onScreen = new Rect(10,-380,85,125);
	string tower;
	int level;
	public static UITowerUpgradeCard Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UITowerUpgradeCard)FindObjectOfType(typeof(UITowerUpgradeCard));
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
		string upgradeTex = (level == 3)? "NoUpgrade" : tower+(level+1).ToString();
		guiTexture.texture = PrefabManager.PrefabCards[upgradeTex];
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
