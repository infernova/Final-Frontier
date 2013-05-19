using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public Transform followTarget;
	public bool initialized = false; // set this to true to 'activate'
	float width = 25;
	float height = 8;
	float border = 2;
	
	public void Init(Transform target) {
		initialized = true;
		followTarget = target;
	}
	
	void Update () {
		if (initialized) {
			if (followTarget == null) {
				Destroy(gameObject);	
			} else {
				// Follow enemy
				Vector3 pos = Camera.main.WorldToScreenPoint(followTarget.position);
				float fov = Camera.main.fieldOfView;
				float maxFov = CameraController.maxFov;
				float fovScale = maxFov / fov;
				Alien alien = followTarget.GetComponent<Alien>(); 
				float heightAbove = alien.iconHeightOffset;
				float distToRight = alien.iconWidthOffset;
				float fractionHP = (float)alien.HP / (float)alien.MaxHP;
				GUITexture healthBarScale = transform.FindChild("PrefabHealthBarScale").guiTexture;
				guiTexture.pixelInset = new Rect(pos.x - Screen.width/2f - width/2f + fovScale * distToRight, 
												pos.y - Screen.height/2f + fovScale * heightAbove - height/2f, 
												fovScale * width, fovScale * height);
				healthBarScale.pixelInset = 
					new Rect(pos.x - Screen.width/2f - width/2f + fovScale * border + fovScale * distToRight, 
					pos.y - Screen.height/2f + fovScale * heightAbove - height/2f+ fovScale * border, 
					fractionHP * fovScale *(width - border - border), fovScale *(height - border - border));
				// Change color
				if (fractionHP < 0.2f) {
					healthBarScale.color = Color.red;	
				}else if (fractionHP < 0.5f) {
					healthBarScale.color = Color.yellow;
				} else {
					healthBarScale.color = Color.green;
				}
				
			}
		}
	}
}
