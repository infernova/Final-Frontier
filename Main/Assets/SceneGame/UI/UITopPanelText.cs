using UnityEngine;
using System.Collections;

public class UITopPanelText : MonoBehaviour {
	public string text = "";
	protected GUITexture [] slot = new GUITexture[4];
	// Use this for initialization
	void Start () {
		slot[0] = transform.FindChild("Slot1").guiTexture;
		slot[1] = transform.FindChild("Slot2").guiTexture;
		slot[2] = transform.FindChild("Slot3").guiTexture;
		slot[3] = transform.FindChild("Slot4").guiTexture;
		for (int i=0;i<4;i++) {
			slot[i].pixelInset = Constants.getRectToScreen(slot[i].pixelInset);
		}
	}
	
	public virtual void PreUpdate() {}
	
	// Update is called once per frame
	void Update () {
		PreUpdate();
		int i=0;
		for (;i<text.Length;i++) {
			if (char.IsDigit(text[i]) || text[i] == '/') {
				slot[i].texture = PrefabManager.PrefabTopPanelText[text[i]];
			} else {
				slot[i].texture = PrefabManager.PrefabTopPanelText[' '];
			}
		}
		for (;i<4;i++) {
			slot[i].texture = PrefabManager.PrefabTopPanelText[' '];
		}
	}
	public void setColor(Color c) {
		foreach(GUITexture tex in slot) {
			tex.color = c;
		}
	}
}
