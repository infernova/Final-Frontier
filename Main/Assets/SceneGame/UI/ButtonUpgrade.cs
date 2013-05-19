using UnityEngine;
using System.Collections;

public class ButtonUpgrade : PausableButton {
	private Rect onScreen;
	
	void Start() {
		onScreen = Constants.getRectToScreen(402,-370,90,110);
	}
	
	public override void ButtonDown() {
		if(((Tower)GameSession.Instance.selectedObject).Upgrade()) {
			UITowerSelectedCard.Instance.upgrade();
			UITowerUpgradeCard.Instance.upgrade();
			UIBlockLowerSelectLeft.Instance.upgrade();
			UIBlockLowerSelectRight.Instance.upgrade();
		}
	}
	
	protected override void UpdateAddOn() {
		if (GameSession.Instance.selectedObject is Tower) {
			guiTexture.pixelInset = onScreen;
			if(((Tower)GameSession.Instance.selectedObject).Level == 3 ||
				((Tower)GameSession.Instance.selectedObject).UpgradeCost > GameSession.Instance.credits)
				guiTexture.color = Constants.desaturate;
			else
				guiTexture.color = Constants.clear;
		} else {
			guiTexture.pixelInset = Constants.offScreenRect;
		}
	}
}

