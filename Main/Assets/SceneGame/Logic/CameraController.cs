using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float clickDragSpeed = 20f;
	public float touchDragSpeed = 1f;
	public float scrollwheelSensitivity = 14f;
	public static float minFov = 50f;
	public static float maxFov = 85f;
	public float cameraMinX = -100f;
	public float cameraMaxX = 100f;
	public float cameraY = 400f;
	public float cameraMinZ = -250f;
	public float cameraMaxZ = -95f;
	public float minPanMagnitude = 2f;
	
    private Vector3 dragOrigin;
	
	public float pinchZoomInSpeed = 0.1f;
	public float pinchZoomOutSpeed = 0.2f;
	public float minPinchSpeed = 5f; 
	public float varianceInDistances = Constants.ratio * 5f; 
	private float touchDelta = 0; 
	private Vector2 prevDist = new Vector2(0,0);
	private Vector2 curDist = new Vector2(0,0); 
	private float speedTouch0 = 0;
	private float speedTouch1 = 0;
	
	private Vector3 prevMousePos;
	
	
	void panTouch() {
		if (Input.touchCount == 1) {
			if(Input.GetTouch(0).phase == TouchPhase.Moved) {
	        	Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				if (touchDeltaPosition.magnitude >  minPanMagnitude) {
		        	transform.Translate(-touchDeltaPosition.x * touchDragSpeed,0, -touchDeltaPosition.y * touchDragSpeed);
					Vector3 pos = transform.position;
					transform.position = new Vector3(Mathf.Clamp(pos.x,cameraMinX, cameraMaxX),cameraY,Mathf.Clamp(pos.z,cameraMinZ, cameraMaxZ));
				}
			}
        }else if (Input.touchCount == 2) {
				if(Input.GetTouch(0).phase == TouchPhase.Moved && 
				Input.GetTouch(1).phase == TouchPhase.Moved) {
				ScreenDebugger.addText("Zooming");
		        curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
		        prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
		        touchDelta = curDist.magnitude - prevDist.magnitude;
		        speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
		        speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
				if (speedTouch0 > minPinchSpeed && 
					speedTouch1 > minPinchSpeed) {
					float fov = Camera.main.fieldOfView;
					if (touchDelta + varianceInDistances <= 1) {
						//zooming out
						ScreenDebugger.addText("Zooming out");
						fov -= touchDelta * pinchZoomOutSpeed;
					}else if (touchDelta + varianceInDistances > 1) {
						//zooming in
						fov -= touchDelta * pinchZoomInSpeed;
						ScreenDebugger.addText("Zooming in");
					}else { return; }
					
					fov = Mathf.Clamp(fov, minFov, maxFov);
					Camera.main.fieldOfView = fov;
				}
	    	}   
		}

	}
	void panClick() {
		if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButton(0) && (prevMousePos - Input.mousePosition).magnitude > 1f) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * -clickDragSpeed, 0, pos.y * -clickDragSpeed);
			transform.Translate(move, Space.World); 
			pos = transform.position;
			transform.position = new Vector3(Mathf.Clamp(pos.x,cameraMinX, cameraMaxX), cameraY,Mathf.Clamp(pos.z,cameraMinZ, cameraMaxZ));
		}	
		
		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis("Mouse ScrollWheel") * scrollwheelSensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;
		
		prevMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		// Panning View
		#if UNITY_ANDROID
		    panTouch();
		#elif UNITY_IPHONE
		    panTouch();
		#elif UNITY_STANDALONE_WIN
			panClick();
		#elif UNITY_STANDALONE_OSX
			panClick();
		#elif UNITY_WEBPLAYER
			panClick();
		#endif
		
	}
}
