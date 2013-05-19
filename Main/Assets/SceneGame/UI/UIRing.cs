using UnityEngine;
using System.Collections;

public class UIRing : MonoBehaviour {
	public SelectableObject obj = null;
	public bool toInit = false;
	const float radius = 9;
	const float variance = 2;
	const float ExpandSpeedVar = 2;
	const float rotSpeedVar = 2;
	void Awake() {
		renderer.enabled = false;			
	}
	
	void LateUpdate () {
		if (obj != null) {
			if (toInit) {
				toInit = false;
				obj.ring = this;
				transform.position = new Vector3(obj.transform.position.x, transform.position.y, obj.transform.position.z);
			}
			if (SelectableObject.showAll && obj is TowerSpawnPoint) {
				renderer.enabled = true;	
			}else {
				renderer.enabled = obj.showRing;
			}
		}
		float rad = radius + variance * (Mathf.Sin(ExpandSpeedVar * Time.time) + 1) / 2;
		transform.localScale = new Vector3(rad,1,rad);
		transform.RotateAroundLocal(transform.up,Time.deltaTime * rotSpeedVar);
	}
}
