using UnityEngine;
using System.Collections;

public class TouchScreen : MonoBehaviour {

	protected GameObject touchedObject;
	private RaycastHit hit;

 // Update is called once per frame
 	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (UIBlockLower.Instance != null && ButtonPauseMenu.Instance != null &&
				!UIBlockLower.Instance.guiTexture.HitTest(Input.mousePosition) &&
				!ButtonPauseMenu.Instance.guiTexture.HitTest(Input.mousePosition)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		 		if (Physics.Raycast (ray, out hit)) {
		 			touchedObject = hit.transform.gameObject; 
		 			touchedObject.SendMessage("ButtonDown",SendMessageOptions.DontRequireReceiver);
	 			}
			}	 		
 		}
	}
}
