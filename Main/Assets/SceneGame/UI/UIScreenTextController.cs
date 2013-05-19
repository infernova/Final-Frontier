using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScreenTextController : PausableMonoBehaviour {
	public string text = "";
	public float duration = 1.5f;
	public Color color = Color.white;
	public static readonly Color red = new Color(120/255f,0,0,128/255f);
	public static readonly Color green = new Color(26/255f, 120/255f, 0, 128/255f);
	public static readonly Color yellow = new Color(120/255f, 120/255f, 0, 128/255f);
	public static readonly Color white = Color.white;
	private float width = 15f;
	private float height = 20f;
	
	public Vector3 position = Vector3.zero;
	
	List<GUITexture> tex = new List<GUITexture>();
	public bool toInit = false;
	private bool initialized = false;
	float totalTime = 1f;
	float currTime = 0f;
	float upIncrement = 50;
	float upOffset = 10f;
	
	float sideOffset = 0f;
	
	public float upDistance;
	
	public static void MakeDamageText(float upOff, string inText, Vector3 pos) {
		UIScreenTextController obj = Create();
		obj.Init(upOff, inText, pos, red);
	}
	
	public static void MakeHealText(float upOff, string inText, Vector3 pos) {
		UIScreenTextController obj = Create();
		obj.Init(upOff, inText, pos, green);
	}
	
	public static void MakeGoldText(float upOff, string inText, Vector3 pos) {
		UIScreenTextController obj = Create();
		obj.Init(upOff, inText, pos, yellow);
	}
	
	static UIScreenTextController Create() {
		return ((GameObject)Instantiate(PrefabManager.UIScreenTextController)).GetComponent<UIScreenTextController>();	
	}
	
	protected void Init(float upOff, string inText, Vector3 pos, Color inColor) {
		text = inText;
		upOffset = upOff;
		position = pos;
		color = inColor;
		sideOffset = UnityEngine.Random.Range(-20f,20f);
		toInit = true;
	}
	
	void Update () {
		if (toInit) {
			// spawn appropriate text and apply it
			initialized = true;
			for (int i=0;i<text.Length;i++) {
				GUITexture texture = ((GameObject)Instantiate(PrefabManager.UIScreenText)).GetComponent<GUITexture>();
				texture.texture = PrefabManager.PrefabScreenText[text[i]];
				if (text[i] != '#')
					texture.color = color;
				else
					texture.color = Color.white;
				tex.Add(texture);
			}
			toInit = false;
		}
		if (initialized) {
			currTime += Time.deltaTime;
			// Cleanup
			if (currTime > totalTime) {
				foreach(GUITexture t in tex) {
					Destroy(t.gameObject);	
				}
				Destroy(gameObject);
			}
			// Fade out
			float alpha = (totalTime - currTime) / totalTime;
			int j=0;
			foreach(GUITexture t in tex) {
				if (text[j] == '#')
					t.color = new Color(white.r, white.g, white.b, alpha);
				else
					t.color = new Color(color.r, color.g, color.b, alpha);
				j++;
			}		
			// Move up
			upDistance = (totalTime - currTime) / totalTime * upIncrement + upOffset - upIncrement;
			
			Vector3 pos = Camera.main.WorldToScreenPoint(position);
			float fov = Camera.main.fieldOfView;
			float maxFov = CameraController.maxFov;
			float fovScale = maxFov / fov;
			
			for(int i=0;i<tex.Count;i++) {
				tex[i].pixelInset = new Rect(pos.x - Screen.width/2f - fovScale * width  * tex.Count / 2f + i * fovScale * width + sideOffset, 
											pos.y - Screen.height/2f - height/2f - fovScale * upDistance, 
											fovScale * width, fovScale * height);
			}
			
		}
	}
}
