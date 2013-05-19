using UnityEngine;
using System.Collections;

public abstract class Alien : PausableMonoBehaviour {
	public int iconHeightOffset { set; get; }
	public int iconWidthOffset { set; get; }
	public static int noOfAliens = 0;
	public PathManager routeManager;
	private Transform[] route;
	public float distToEnd = Mathf.Infinity;
	public int goalPathIndex = 0;
	private Vector3 lastPosition;

	private float moveSpeedLeft;
	public float rotationSpeed = 90f;
	public HealthBar hpBar;
	public bool isDead = false;
	
	/*public abstract string Description{
		get;
		set;
	}*/
	
	public int _HP;
	public int HP{
		get { return _HP; }
		set {
			_HP = value;
			if(_HP > MaxHP){
				_HP = MaxHP;
			}
			if (_HP < 0)
				_HP = 0;
		}
	}
	
	public void DamageOrHeal(int val, bool poison = false) {
		if (isDead)
			return;
		
		HP += val;
		if (GameSession.Instance.isNetworkGame) {
			// Tell other guy to damage too ><
			networkView.RPC("RPCDamageOrHeal",RPCMode.Others,val, poison? 1 : 0);
		}
		showDamageIcon(val, poison);
	}
	
	[RPC]
	void RPCDamageOrHeal(int val, int poison) {
		HP += val;
		showDamageIcon(val, (poison == 1));
	}
	
	void showDamageIcon(int val, bool poison) {
		string prefix = "";
		if (poison) {
			prefix = "#";
		}
		if (val > 0) {
			UIScreenTextController.MakeHealText(iconHeightOffset, val.ToString(), transform.position);	
		}else if (val < 0) {
			UIScreenTextController.MakeDamageText(iconHeightOffset, prefix + Mathf.Abs(val).ToString(), transform.position);	
		}
	}
	
	private float _movementSpeed = -1f;
	public virtual float movementSpeed {
		get {
			if(_movementSpeed <= 0f){
				_movementSpeed = Constants.AlienMovementSpeed[this.GetType().Name];
			}
			
			return _movementSpeed;
		}
		set { _movementSpeed = value; }
	}
	
	public virtual int MaxHP{
		get { return GameSession.Instance.round * 
			Constants.AlienMaxHP[this.GetType().Name]; }
	}
	
	public virtual int Cost{
		get { return Constants.AlienCost[this.GetType().Name]; }
	}
	
	
	protected void Death(){
		
		GameSession session = GameSession.Instance;
		if (session.isNetworkGame) {
			networkView.RPC("RPCdeath",RPCMode.All);
		} else {
			handleDeath();
		}
		
	}
	[RPC]
	void RPCdeath() {
		handleDeath();
	}
	
	protected virtual void handleDeath() {
		isDead = true;
		if (hpBar != null)
			Destroy(hpBar.gameObject);
		animation.Play("death");
	}
	
	void clearCorpse() {
		GameSession session = GameSession.Instance;
		if(session.isDefender){
			showGoldGain(Constants.AlienDeathGold[this.GetType().Name]);
		}
		else{
			float total = routeManager.distToEnd[0];
			float percentageComplete = ((total - distToEnd)/total);
			float thousands = Mathf.Max(total/1000f/1.4f,1f);
			
			showGoldGain(Mathf.FloorToInt(percentageComplete * thousands * (float) Cost));
		}
		Destroy(gameObject);
	}
	
	public void ReachEnd(){
		GameSession session = GameSession.Instance;
		if(session.isAttacker){
			float total = routeManager.distToEnd[0];
			float thousands = Mathf.Max(total/1000f/1.4f,1f);
			showGoldGain(Mathf.FloorToInt(2 * thousands * Cost));
		} else if (session.isNetworkGame) {
			networkView.RPC("RPCreachEnd",RPCMode.Others);
		}
		
		session.lives -= Constants.AlienReachEndLifeDeduction[this.GetType().Name];
		if (session.isNetworkGame)
			Network.Destroy(gameObject);
		else
			Object.Destroy(gameObject);
	}
	
	protected void showGoldGain(int val) {
		if (val == 0)
			return;
		GameSession session = GameSession.Instance;
		session.credits += val;
		UIScreenTextController.MakeGoldText(iconHeightOffset,"+$"+val, transform.position);
	}
	
	
	
	[RPC]
	void RPCreachEnd() {
		showGoldGain(2 * Cost);
	}
	
	public void Init(int spawnPointID, int routeId){
		if (GameSession.Instance.isNetworkGame) {
			networkView.RPC("RPCInit",RPCMode.Others,spawnPointID,routeId);
		}
		trueInit(spawnPointID,routeId);
	}
	
	[RPC]
	void RPCInit(int spawnPointID, int routeId) {
		trueInit(spawnPointID,routeId);
	}
	
	
	void trueInit(int spawnPointID, int routeId) {
		routeManager = GameStart.Instance.spawnPoints[spawnPointID].routes[routeId];
		if (routeManager != null) {
			route = routeManager.path;
			goalPathIndex = 1;
			Vector3 dir = route[goalPathIndex].position - transform.position;
   			transform.rotation = Quaternion.LookRotation(dir);
		}
	}
	
	protected void Start() {
		isDead = false;
		HP = MaxHP;
		HealthBar healthBar = ((GameObject) Instantiate(PrefabManager.PrefabHealthBar)).GetComponent<HealthBar>();
		hpBar = healthBar;
		healthBar.Init(transform);
	}
	
	private Vector2 dirVec;
	
	float getDistToNext() {
		if (goalPathIndex < route.Length) {
			float currX = transform.position.x;
			float currZ = transform.position.z;
			Vector2 currPos = new Vector2(currX,currZ);
			float goalX = route[goalPathIndex].position.x;
			float goalZ = route[goalPathIndex].position.z;
			Vector2 goalPos = new Vector2(goalX, goalZ);
			dirVec = goalPos - currPos;
			return dirVec.magnitude;
		}
		return Mathf.Infinity;
	}
	
	protected void Update() {
		
	}
	
	protected void FixedUpdate () {
		if(!GameSession.Instance.isNetworkGame || GameSession.Instance.isDefender) {
			if (!isDead && HP <= 0) {
				Death();
				return;
			}
		}
		
		if (routeManager != null && !isDead) {
			moveSpeedLeft = movementSpeed * Time.deltaTime;
			float distToNext = 0f;
			// Overshoot as many points as possible
			// Can still move further than the next waypoint
			while (moveSpeedLeft >= 0f && 
				(distToNext = getDistToNext()) <= moveSpeedLeft) {
				// Move to next waypoint
				moveSpeedLeft -= distToNext;
				Vector3 pos = route[goalPathIndex].position;
				transform.position = new Vector3(pos.x,transform.position.y, pos.z);
				goalPathIndex++;
			}
			
			if (goalPathIndex < route.Length) {
				Vector3 nextDirVec = (new Vector3 (dirVec.x, 0, dirVec.y)).normalized;
				// Move the last bit of movement that cannot reach next waypoint
				if (moveSpeedLeft > 0f) {
					Vector3 moveVec = nextDirVec * moveSpeedLeft;
					transform.position += moveVec;
				}
				// Calculate distToEnd
				distToEnd = distToNext + routeManager.distToEnd[goalPathIndex];
				// Rotate to next waypoint
				Quaternion targetRotation = Quaternion.LookRotation(nextDirVec);
	    		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			} else {
				distToEnd = 0f;	
			}
		}
		
	}
	
	protected void LateUpdate(){}
	void OnDestroy() {
		noOfAliens--;
		if (hpBar != null)
			Destroy(hpBar.gameObject);
	}
	
	public void slowDown(float SlowTime, float SlowRate) {
		networkView.RPC("RPCSlow",RPCMode.Others, SlowTime, SlowRate);
	}
	
	[RPC]
	void RPCSlow(float SlowTime, float SlowRate) {
		SlowEffect fx = gameObject.AddComponent<SlowEffect>();
		fx.Init(SlowTime, SlowRate);
	}		
	
}