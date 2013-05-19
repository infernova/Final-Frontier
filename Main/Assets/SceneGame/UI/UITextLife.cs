using UnityEngine;
using System.Collections;

public class UITextLife : UITopPanelText {
	int prevLives = 0;
	public static UITextLife MyInstance;
	public static UITextLife Instance {
		get {
			if (MyInstance == null) {
				MyInstance = (UITextLife)FindObjectOfType(typeof(UITextLife));
			}
			return MyInstance;
		}
	}
	void Awake() {
		MyInstance = this;	
	}
	float countDown = 0f;
	float redTime = 1f;
	
	void FixedUpdate() {
		int lives = GameSession.Instance.lives;
		if (lives < prevLives) {
			countDown = redTime;
		}
		prevLives = lives;
		countDown -= Time.deltaTime;
		if (countDown < 0f) {
			setColor(Constants.clear);
			countDown = 0f;
		} else {
			setColor(Color.red);
		}
	}
	
	public override void PreUpdate() {
		int lives = GameSession.Instance.lives;
		text = lives.ToString();
	}
	

}