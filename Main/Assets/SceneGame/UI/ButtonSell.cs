using UnityEngine;
using System.Collections;

public class ButtonSell : PausableButton {
	private Rect onScreen;
	
	void Start() {
		onScreen = Constants.getRectToScreen(-110,-370,90,110);
	}
	
	public override void ButtonDown() {
		ScreenDebugger.addText("De-wete");
		Tower tower = ((Tower)GameSession.Instance.selectedObject);
		GameSession.Instance.selectedObject.Deselect();
		tower.Sell();
		UIBlockLower.Instance.guiTexture.texture = PrefabManager.UIBlockLowerDefault;
		UITowerSelectedCard.Instance.hide();
		UITowerUpgradeCard.Instance.hide();
	}
	
	protected override void UpdateAddOn() {
		if (GameSession.Instance.selectedObject is Tower) {
			guiTexture.pixelInset = onScreen;
		} else {
			guiTexture.pixelInset = Constants.offScreenRect;
		}
	}
}

