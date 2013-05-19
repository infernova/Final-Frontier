using UnityEngine;
using System.Collections;

public class GameButton : GUITextureHelper {
	bool prevMouseOver = false;
	// Update is called once per frame
	void Update () {		
		// Magic Mouse handling
		if (guiTexture.HitTest(Input.mousePosition)) {
			handleEnter();
			if (Input.GetMouseButtonDown(0)) {
				handleDown();
			}
		} else {
			handleExit();	
		}
		UpdateAddOn();
	}
	
	protected virtual void UpdateAddOn() {}
	
	protected virtual bool IsEnabled() {
		return true;
	}
	
	
	void handleEnter() {
		if (!prevMouseOver && IsEnabled())
			ButtonEnter();
		prevMouseOver = true;
	}
	
	void handleExit() {
		if (prevMouseOver && IsEnabled())
			ButtonExit();
		prevMouseOver = false;
	}
	
	public void handleDown() {
		if (IsEnabled())
			ButtonDown();
	}
	
	
	// Graphical things 
	public virtual void ButtonEnter() {}
	
	public virtual void ButtonExit() {}
	
	// Effect things
	public virtual void ButtonDown() {}
}
