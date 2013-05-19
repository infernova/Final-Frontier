using UnityEngine;
using System.Collections;

public class ButtonPauseMenu : GameButton {
	private static Rect wholeScreen;
	public bool processing = false;
	public static ButtonPauseMenu MyInstance;
	public static ButtonPauseMenu Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (ButtonPauseMenu)FindObjectOfType(typeof(ButtonPauseMenu));
			}
			return MyInstance;
		}
	}
	void Start() {
		MyInstance = this;
	}
	
	
	public override void ButtonDown() {
		GameSession session = GameSession.Instance;
		if (session.isNetworkGame) {
			if (!processing) {
				if (PausableMonoBehaviour.isPaused) {
					session.RequestResume();
				} else {
					session.RequestPause();
				}
			}
			
		} else {
			if (!processing) {
				if (!PausableMonoBehaviour.isPaused) {
					ScreenDebugger.addText("Paws");
					PausableMonoBehaviour.Pause();
				}
				else {
					ScreenDebugger.addText("Un-paws");
					PausableMonoBehaviour.Resume();
				}
			}
		}
		
	}
	
	public void show() {
		processing = false;
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x, pos.y,4);
	}
	
	public void hide() {
		processing = true;
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x, pos.y,1);
	}
	
	void notify(float n) {
		Color c = UICountDown.Instance.guiTexture.color;
		UICountDown.Instance.guiTexture.color = new Color(c.r,c.g,c.b,n/2f);
		const float minScale = 0.3f;
		float scale = minScale + (1-minScale) * n;
		UICountDown.Instance.guiTexture.pixelInset = Constants.getRectToScreen(scale * -512,scale * -384,scale * 1024,scale * 768);
	}
	const float appearTime = 0.6f;
	float fadeTime = 1-appearTime;
	
	void three() {
		UIPauseMenu.Instance.visible = false;
		UICountDown.Instance.guiTexture.texture = PrefabManager.UICountDown3;
		iTween.ValueTo(gameObject,iTween.Hash("from",0,
			"to",1,
			"onupdate","notify",
			"time",appearTime,
			"ignoretimescale",true,
			"oncomplete",
			"threeOut"));
	}
	
	void threeOut() {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",1,
			"to",0,
			"onupdate","notify",
			"time",fadeTime,
			"ignoretimescale",true,
			"oncomplete",
			"two"));
	}
	
	void two() {
		UICountDown.Instance.guiTexture.texture = PrefabManager.UICountDown2;
		iTween.ValueTo(gameObject,iTween.Hash("from",0,
			"to",1,
			"onupdate","notify",
			"time",appearTime,
			"ignoretimescale",true,
			"oncomplete",
			"twoOut"));	
	}
	
	void twoOut() {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",1,
			"to",0,
			"onupdate","notify",
			"time",fadeTime,
			"ignoretimescale",true,
			"oncomplete",
			"one"));
	}
	
	void one() {
		UICountDown.Instance.guiTexture.texture = PrefabManager.UICountDown1;
		iTween.ValueTo(gameObject,iTween.Hash("from",0,
			"to",1,
			"onupdate","notify",
			"time",appearTime,
			"ignoretimescale",true,
			"oncomplete","oneOut"));	
	}
	
	void oneOut() {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",1,
			"to",0,
			"onupdate","notify",
			"time",fadeTime,
			"ignoretimescale",true,
			"oncomplete", "zero"));	
	}
	
	void zero() {
		UICountDown.Instance.guiTexture.pixelInset = Constants.offScreenRect;
		PausableMonoBehaviour.Resume(false);
		show();
	}
	float menuTime = 0.6f;
	
	void lowerMenu(float n) {
		UIPauseMenu.Instance.guiTexture.pixelInset = Constants.getRectToScreen(-512,-384 + n * 768, 1024, 768);
		Rect r = UIPauseMenuQuit.Instance.baseRect;
		UIPauseMenuQuit.Instance.guiTexture.pixelInset = Constants.getRectToScreen(r.x,r.y + n * 768,r.width, r.height);
		r = UIPauseMenuResume.Instance.baseRect;
		UIPauseMenuResume.Instance.guiTexture.pixelInset = Constants.getRectToScreen(r.x,r.y + n * 768,r.width, r.height);
		
		UIPauseMenu.Instance.guiTexture.enabled = true;
		UIPauseMenuQuit.Instance.guiTexture.enabled = true;
		UIPauseMenuResume.Instance.guiTexture.enabled = true;
	}
	
	public void menuUp(bool callback) {
		if (callback) {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",0,
			"to",1,
			"ignoretimescale",true, 
			"time",menuTime,
			"onupdate","lowerMenu",
			"oncomplete","three"));
		} else {
			iTween.ValueTo(gameObject,iTween.Hash(
			"from",0,
			"to",1,
			"ignoretimescale",true, 
			"time",menuTime,
			"onupdate","lowerMenu"));
		}
	}
	
	public void menuDown() {
		iTween.ValueTo(gameObject,iTween.Hash(
			"from",1,
			"to",0,
			"onupdate","lowerMenu",
			"time",fadeTime,
			"ignoretimescale",true,
			"oncomplete", "menuIsDown"));
	}
	
	void menuIsDown() {
		UIPauseMenu.Instance.visible = true;
		ButtonPauseMenu.Instance.show ();
	}
	
	public void Resume() {
		hide();
		GameSession.Instance.disableTouch = false;
		if (UIPauseMenu.Instance.visible) {
			menuUp(true);
		} else {
			three ();
		}
	}
}
