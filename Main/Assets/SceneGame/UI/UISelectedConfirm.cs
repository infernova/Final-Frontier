using UnityEngine;
using System.Collections;

public class UISelectedConfirm : MonoBehaviour {
	float width = 90;
	float height = 90;
	
	void LateUpdate () {
		SelectableObject obj = GameSession.Instance.selectedObject;
		ButtonCard card = GameSession.Instance.selectedCard;
		if (obj != null) {
			// In confirm state
			Vector3 pos = Camera.main.WorldToScreenPoint(obj.transform.position);
			float fov = Camera.main.fieldOfView;
			float maxFov = CameraController.maxFov;
			float fovScale = maxFov / fov;
			guiTexture.pixelInset = new Rect(pos.x - Screen.width/2f - fovScale * width/2f, 
												pos.y - Screen.height/2f - fovScale *height/2f, 
												fovScale * width, fovScale * height);
			if (GameSession.Instance.isDefender && card != null) {
				guiTexture.texture = PrefabManager.UIConfirm;
			} else {
				guiTexture.texture = PrefabManager.UISelected;
			}
			
		} else {
			guiTexture.pixelInset = Constants.offScreenRect;
		}
	}
}
