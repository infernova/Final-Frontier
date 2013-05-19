using UnityEngine;
using System.Collections;

public class SlowEffect : Effect
{
	public float SlowRate{
		get;
		set;
	}

	private GameObject glow;
	
	private IEnumerator SlowTarget(){
		gameObject.GetComponent<Alien>().movementSpeed *= SlowRate;
		
		while(EffectTime >= 0){
			yield return _sync();
			
			EffectTime -= Time.deltaTime;
		}
		
		EffectTimeOut();
	}
	
	void OnDestroy(){
		Object.Destroy(glow);
		
		gameObject.GetComponent<Alien>().movementSpeed /= SlowRate;
	}
		
	
	public void Init(float slowTime, float slowRate){
		SlowRate = slowRate;
		base.Init(slowTime);
	}
	
	// Use this for initialization
	void Start ()
	{
		glow = (GameObject) Object.Instantiate(PrefabManager.PrefabSlowEffectGlow, gameObject.transform.position, Quaternion.identity);
		glow.transform.position = gameObject.transform.position + new Vector3(0f, 0f, -15f);
		glow.transform.parent = gameObject.transform;
		ParticleSystem ps = glow.GetComponent<ParticleSystem>();
		ps.Play();
		
		StartCoroutine (SlowTarget ());
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		base.Update ();
	}
}

