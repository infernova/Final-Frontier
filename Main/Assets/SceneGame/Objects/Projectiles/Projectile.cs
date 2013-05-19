using UnityEngine;
using System.Collections;

public class Projectile : PausableMonoBehaviour {
	protected int areaOfEffect;
	protected int damage;
	protected Alien target;
	protected int speed;
	bool initialized = false;
	protected AudioSource hitSound;
	
	public virtual void Init(int AOE, Alien tgt, int velocity, int dmg){
		areaOfEffect = AOE;
		target = tgt;
		speed = velocity;
		damage = dmg;
		
		initialized = true;
		if (GameSession.Instance.isNetworkGame) {
			networkView.RPC("RPCInit",RPCMode.Others,tgt.networkView.viewID,velocity);
		}
	}
	
	[RPC]
	void RPCInit(NetworkViewID tgt, int velocity) {
		NetworkView view = NetworkView.Find(tgt);
		target = (view == null)? null :view.GetComponent<Alien>();
		speed = velocity;
		initialized = true;
	}
	
	
	private void OnCollisionEnter(Collision collision){
		if (!GameSession.Instance.isNetworkGame || GameSession.Instance.isDefender) {
			Alien hitAlien = collision.gameObject.GetComponent<Alien>();
			
			if(hitAlien == target){
				//hitSound.PlayOneShot(); //Get hit sound from constants
				if(this.GetType() == typeof(PoisonProjectile)){
					if (GameSession.Instance.isNetworkGame) {
						Network.Instantiate(PrefabManager.PrefabPoisonCloud, transform.position, Quaternion.identity,0);
					} else {
						Instantiate(PrefabManager.PrefabPoisonCloud, transform.position, Quaternion.identity);
					}
				}
				
				if(this.GetType() == typeof(AoeProjectile)){
					if (GameSession.Instance.isNetworkGame) {
						Network.Instantiate(PrefabManager.PrefabAoeShockwave, transform.position, Quaternion.identity,0);	
					}else {
						Object.Instantiate(PrefabManager.PrefabAoeShockwave, transform.position, Quaternion.identity);
					}
				}
				
				hitAlien.DamageOrHeal(-damage);
				ApplyHitEffects(hitAlien);
				
				if(areaOfEffect > 0){
					Collider[] aliensHit = Physics.OverlapSphere(hitAlien.transform.position, areaOfEffect, Constants.AliensLayer);
					Random.seed = System.DateTime.Now.Millisecond;
					
					foreach(Collider alien in aliensHit){
						Alien currentAlien = (Alien) alien.gameObject.GetComponent<Alien>();
						
						if(currentAlien != hitAlien){
							int hitDamage = Mathf.FloorToInt(Random.Range(0.8f, 1.0f) * (float) damage);
							currentAlien.DamageOrHeal(-hitDamage);
							ApplyHitEffects(currentAlien);
						}
					}
				}
				if (GameSession.Instance.isNetworkGame) {
					Network.Destroy(gameObject);	
				}else {
					Object.Destroy(gameObject);
				}
			}
		}
	}
	
	private IEnumerator MoveProjectile(){
		float moveTime = 0.0f;
		float totalTime = (target.gameObject.transform.position - transform.position).magnitude / (float) speed;
		
		while(true){
			if (initialized) {
				yield return _sync();
				
				if(target == null){
					if (GameSession.Instance.isNetworkGame) {
						Network.Destroy(gameObject);	
					}else {
						Object.Destroy(gameObject);
					}
					yield break;
				}
				
				moveTime += Time.deltaTime;
				transform.position = Vector3.Lerp(transform.position, target.gameObject.transform.position, moveTime/totalTime);
			
			/*Vector3 dirVec = target.gameObject.transform.position - transform.position;
			Vector3 normMoveVec = Vector3.Normalize(dirVec);
			float distMoved = (float) speed * Time.deltaTime;
			Vector3 moveVec = distMoved * normMoveVec;
			transform.position += moveVec;*/
			
			//rigidbody.velocity = Vector3.zero;
			//rigidbody.angularVelocity = Vector3.zero;
			
			/*if(dist < 0){
				hitAlien.HP -= damage;
				ApplyHitEffects(hitAlien);
				
				if(areaOfEffect > 0){
					Collider[] aliensHit = Physics.OverlapSphere(hitAlien.transform.position, areaOfEffect, Constants.AliensLayer);
					Random.seed = System.DateTime.Now.Millisecond;
					
					foreach(Collider alien in aliensHit){
						Alien currentAlien = (Alien) alien.gameObject.GetComponent<Alien>();
						
						if(currentAlien != hitAlien){
							int hitDamage = (int) (Random.Range(0.8f, 1.0f) * (float) damage);
							currentAlien.HP -= hitDamage;
							ApplyHitEffects(currentAlien);
						}
					}
				}
				
				Object.Destroy(gameObject);
			}*/
			}
			yield return null;
		}
	}
	
	protected virtual void ApplyHitEffects(Alien alien){}
	
	// Use this for initialization
	protected void Start () {
		StartCoroutine(MoveProjectile());	
	}
	
	// Update is called once per frame
	protected void Update () {}
}
