using UnityEngine;
using System.Collections;

public abstract class Effect : PausableMonoBehaviour
{
	public float EffectTime{
		get;
		set;
	}
	
	protected virtual void EffectTimeOut(){
		Destroy(this);
	}
	
	protected virtual void Init(float time){
		EffectTime = time;
	}

	// Use this for initialization
	protected void Start ()
	{
	
	}
	
	// Update is called once per frame
	protected void Update ()
	{
	
	}
}

