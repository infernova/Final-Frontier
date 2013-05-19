using UnityEngine;
using System.Collections;


public class TowerSpawnPoint : SelectableObject {
	public Transform firingRadius;
	public bool toInit = true;
	
	protected override void handleButtonDown() {
		GameSession session = GameSession.Instance;
		if (session.disableTouch)
			return;
		if (session.isDefender) {
			if (session.selectedObject != null && 
				session.selectedObject != this) {
				session.selectedObject.Deselect();	
			}
			
			if (session.selectedCard == null) {
				if (session.selectedObject == this) {
					// Double-tapping is a deselect
					Deselect();
				} else {
					// Just select
					Select();
				}
			} else {
				if (session.selectedObject == this) {
					// Build tower
					string ability = session.selectedCard.ability.ToString();
					if (Constants.TowerBuildCosts[ability] <= session.credits) {
						Tower tower = Tower.Create(ability, this);
						tower.Select();
					}else {
						// Do something here	
					}
				} else {
					// Change selection
					Select();
					showAll = false;
					session.showConfirmation();
				}
			}
		} /*else {
			ScreenDebugger.addText("Deselecting");
			session.refreshSelected();
		}*/
	}
	
	void Init(Transform firingRad) {
		firingRadius = firingRad;
		if (firingRadius != null) {
			firingRadius.position = new Vector3(transform.position.x, firingRadius.position.y, transform.position.z);
			firingRadius.renderer.enabled = false;
		}
	}
	
	void Start() {
		if (toInit) {
			//Spawn a selection ring
			GameObject obj = (GameObject)Instantiate(PrefabManager.PrefabUIRing,transform.position+new Vector3(0,-10,0),Quaternion.identity);
			UIRing uiRing = obj.GetComponent<UIRing>();
			uiRing.obj = this;
			ring = uiRing;
			uiRing.toInit = true;
			//Spawn a firing radius
			obj = (GameObject)Instantiate(PrefabManager.PrefabFiringRadius,transform.position+new Vector3(0,-30,0),Quaternion.identity);
			firingRadius = obj.transform;
		}
		
		
		if (firingRadius != null) {
			firingRadius.position = new Vector3(transform.position.x, firingRadius.position.y, transform.position.z);
			firingRadius.renderer.enabled = false;
		}
	}
	
	
	void Select() {
		GameSession session = GameSession.Instance;
		session.selectedObject = this;
		showRing = true;
	}
	
	protected override void handleDeselect() { 
		GameSession.Instance.selectedObject = null;
		if (firingRadius != null) {
			firingRadius.renderer.enabled = false;
		}
		showRing = false;	
		
	}
}
