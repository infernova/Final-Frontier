using UnityEngine;
using System.Collections;

public class AlienGrunt : Alien
{
	void Awake() {
		iconHeightOffset = 20;
		iconWidthOffset = 3;
	}
	// Use this for initialization
	void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
	}
	
	void LateUpdate () {
		base.LateUpdate();	
	}
	
	void FixedUpdate() 
	{
		base.FixedUpdate();
	}
}
