using UnityEngine;
using System.Collections;

public class MapSelectButton : GameButton {
	public int targetMap = 1; 
	const float ExpandSpeedVar = 5;
	Rect inset;
	void Awake() {
		inset = guiTexture.pixelInset;
	}
	
	public override void ButtonDown ()
	{
		ServerNetworkController.Instance.selectedLevel = targetMap;
	}
	
	protected override void UpdateAddOn ()
	{
		if (ServerNetworkController.Instance.selectedLevel == targetMap) {
			float variance = 20 * (Mathf.Sin(ExpandSpeedVar * Time.time) + 1) / 2;
			guiTexture.pixelInset = Constants.getRectToScreen(inset.x-variance, inset.y-variance,inset.width+2*variance,inset.height+2*variance);	
		} else {
			guiTexture.pixelInset = Constants.getRectToScreen(inset);	
		}
	}
}
