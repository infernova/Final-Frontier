using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Tower : SelectableObject {
	public Transform firingRadius;
	private float rotationSpeed = 180;
	public Alien target = null; // The alien that you're aiming for
	
	public virtual int BuildCost {
		get{
			return Constants.TowerBuildCosts[this.GetType().Name];
		}
	}
	
	public virtual string Name{
		get;
		set;
	}
	
	protected GameObject Bullet{
		get{
			return PrefabManager.PrefabTowerBullets[this.GetType().Name][Level-1];
		}
	}
	 
	private Transform _barrel = null;
	protected virtual Vector3 BarrelTip{
		get{
			if (_barrel == null)
				_barrel = transform.parent.FindChild("Barrel"+Level);
			return (_barrel == null)? transform.position : _barrel.position;
		}
	}
	
	protected virtual int Damage{
		get{
			return Constants.TowerDamages[this.GetType().Name][Level - 1];
		}
	}
	
	private int _Level = 1;
	public int Level{
		get{
			return _Level;
		}
		set{
			if(Level < 3){
				_Level = value;
			}
			else if(Level >= 3){
				_Level = 3;
			}
		}
	}

	protected virtual int Range{
		get{
			return Constants.TowerRanges[this.GetType().Name][Level - 1];
		}
	}
	
	protected virtual int AreaOfEffect{
		get{
			return Constants.TowerAreasOfEffect[this.GetType().Name][Level - 1];
		}
	}
	
	protected virtual double RateOfFire{
		get{
			return Constants.TowerRatesOfFire[this.GetType().Name][Level - 1];
		}
	}
	
	public virtual int UpgradeCost{
		get {
			return (Level < 3)? Constants.TowerUpgradeCosts[this.GetType().Name][Level - 1] : 0;
		}
	}
	
	public virtual int SellingCost{
		get{
			int cost = BuildCost;
			
			for(int i = 1; i < Level; i++){
				cost += Constants.TowerUpgradeCosts[this.GetType().Name][i - 1];
			}
			
			return cost / 2;
		}
	}
	
	private void updateFiringRadius() {
		if (firingRadius != null) {
			firingRadius.localScale = new Vector3(Range*2,20,Range*2);
			firingRadius.transform.position = new Vector3(transform.position.x,firingRadius.transform.position.y,transform.position.z);
		}
	}
	
	
	public bool Upgrade(){
		GameSession session = GameSession.Instance;
		if (Level > 2)
			return false;
		int cost = UpgradeCost;
		if (cost <= session.credits) {
			Level++;
			swapTowerSkin();
			updateFiringRadius();
			session.credits -= cost;
			UIScreenTextController.MakeGoldText(0,"-$"+cost, transform.position);
			return true;
		}
		return false;
	}
	
	
	
	public void swapTowerSkin() {
		//Debug.Log("swapping");
		for (int i=0;i<3;i++) {
			if (towers[i] != null) {
				bool toShow = i+1 == Level;
			//	Debug.Log(i + " " + toShow);
				towers[i].SetActiveRecursively(toShow);
			}
		}
		if (towers[Level-1] == null) {
			renderer.enabled = true;
		} else {
			renderer.enabled = false;
		}
	}
	bool toInitialize = true;
	public virtual void Init(Transform firingRad){
		toInitialize = true;
		firingRadius = firingRad;
		updateFiringRadius();
	}
	
	protected abstract void ProjectileInit(GameObject proj, Alien target);
	
	public GameObject [] towers = new GameObject[3];
	
	// Use this for initialization
	void Start () {
		if (GameSession.Instance.isDefender || 
			!GameSession.Instance.isNetworkGame){
			StartCoroutine(Shoot());
		}
		for (int i=0;i<3;i++) {
			try {
				towers[i] = transform.parent.FindChild("Tower"+(i+1)).gameObject;
			}catch (Exception e) {
				Debug.Log(e.ToString());
			}
		}
		Level = 1;
		swapTowerSkin();
	}
	
	
	protected virtual IEnumerator Shoot(){
		while(true){
			yield return _sync();
			
			Alien closestEnemy;
			if((closestEnemy = EnemyInRange()) != null){
				GameObject newProj;
				if (GameSession.Instance.isNetworkGame) {
					newProj = (GameObject) Network.Instantiate(Bullet, BarrelTip, Quaternion.identity,0);
					TowerWrapper tower = transform.parent.GetComponent<TowerWrapper>();	
					tower.mirrorShootAction();
				} else {
					newProj = (GameObject) GameObject.Instantiate(Bullet, BarrelTip, Quaternion.identity);
					handleShootAnimationSound();
				}
				ProjectileInit(newProj, closestEnemy);
				yield return new WaitForSeconds((float) RateOfFire);
			}
		}
	}
	
	
	public void handleShootAnimationSound() {
		if (towers[Level-1] != null && towers[Level-1].animation != null) {
			towers[Level-1].animation.Play();
		}
				
		GetComponent<AudioSource>().PlayOneShot(PrefabManager.TowerShotSounds[this.GetType().Name]);
	}
	
	protected Alien EnemyInRange(){
		RaycastHit[] hits = Physics.SphereCastAll(new Vector3(transform.position.x, 1000,transform.position.z),(float)Range,new Vector3(0,-1,0),Mathf.Infinity,Constants.AliensLayer);
		
		if(hits.Length == 0) {
			target = null;
			return target;
		}
		
		float distToEnd = Mathf.Infinity;
		Alien closestAlien = null;
		
		foreach(RaycastHit hit in hits){
			Alien currentAlien = hit.collider.GetComponent<Alien>();
			if (currentAlien.isDead)
				continue;
			if(this.GetType() == typeof(TowerSlow) && currentAlien.GetComponent<SlowEffect>() != null)
				continue;
			if (currentAlien == target)
				return target;
			if(currentAlien.distToEnd < distToEnd){
				closestAlien = currentAlien;
				distToEnd = currentAlien.distToEnd;
			}
		}
		
		if(closestAlien == null){
			foreach(RaycastHit hit in hits){
				Alien currentAlien = hit.collider.GetComponent<Alien>();
				if (currentAlien.isDead)
					continue;
				if (currentAlien == target)
					return target;
				if(currentAlien.distToEnd < distToEnd){
					closestAlien = currentAlien;
					distToEnd = currentAlien.distToEnd;
				}
			}
		}
		
		target = closestAlien;
		
		return closestAlien;
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		if (toInitialize) {
			toInitialize = false;
			swapTowerSkin();
		}
		Alien closestEnemy;
		if (GameSession.Instance.isDefender) {
			if ((closestEnemy = EnemyInRange()) != null) {
				Vector3 lookDir = closestEnemy.transform.position - transform.position;
				lookDir -= new Vector3(0,lookDir.y,0);
				Quaternion targetRotation = Quaternion.LookRotation(lookDir);
		    	transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			}
		} else if (GameSession.Instance.isNetworkGame) {
			TowerWrapper tower = transform.parent.GetComponent<TowerWrapper>();	
			if (tower != null && tower.targetView != NetworkViewID.unassigned) {
				NetworkView view = NetworkView.Find(tower.targetView); 
				if (view == null) // enemy has dieded
					return;
				closestEnemy = (Alien) view.GetComponent<Alien>();
				Vector3 lookDir = closestEnemy.transform.position - transform.position;
				lookDir -= new Vector3(0,lookDir.y,0);
				Quaternion targetRotation = Quaternion.LookRotation(lookDir);
		    	transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			}
			if (tower != null && Level != tower.Level) {
				Level = tower.Level;
				swapTowerSkin();
			}
		}
	}
	
	
	public static Tower Create(string type, TowerSpawnPoint spawn) {	
		GameObject tower;
		if (GameSession.Instance.isNetworkGame) {
			tower = (GameObject) Network.Instantiate(PrefabManager.PrefabTowers[type],spawn.transform.parent.position,Quaternion.identity,0);
		} else {
			tower = (GameObject) Instantiate(PrefabManager.PrefabTowers[type],spawn.transform.parent.position,Quaternion.identity);
		}
		Tower towerData = tower.GetComponentInChildren<Tower>();
		towerData.Init(spawn.firingRadius);	
		if (spawn.ring != null) {
			spawn.ring.obj = towerData;
			spawn.ring.toInit = true;
		}
		if (GameSession.Instance.isNetworkGame) {
			Network.Destroy(spawn.transform.parent.gameObject);
		} else {
			Destroy(spawn.transform.parent.gameObject);
		}
		GameSession.Instance.credits -= Constants.TowerBuildCosts[type];
		UIScreenTextController.MakeGoldText(0, "-$"+Constants.TowerBuildCosts[type], spawn.transform.position);
		return towerData;
	}
	
	public void Sell() {
		GameObject obj ;
		if (GameSession.Instance.isNetworkGame) {
			obj = (GameObject)Network.Instantiate(PrefabManager.PrefabTowerSpawnPoint,transform.parent.position,Quaternion.identity,0);
		} else {
			obj = (GameObject)Instantiate(PrefabManager.PrefabTowerSpawnPoint,transform.parent.position,Quaternion.identity);
		}
		TowerSpawnPoint spawn = obj.GetComponentInChildren<TowerSpawnPoint>();
		spawn.toInit = false;
		spawn.firingRadius = firingRadius;
		ring.obj = spawn;
		ring.toInit = true;
		GameSession.Instance.credits += SellingCost;
		UIScreenTextController.MakeGoldText(0, "+$"+SellingCost, transform.position);
		if (GameSession.Instance.isNetworkGame) {
			Network.Destroy(transform.parent.gameObject);
		}else {
			Destroy(transform.parent.gameObject);
		}
	}
	
	public void Select() {
		if (GameSession.Instance.isAttacker)
			return;
		GameSession.Instance.refreshSelected();
		GameSession session = GameSession.Instance;

		session.selectedObject = this;
		if (firingRadius != null) {
			firingRadius.renderer.enabled = true;
		}
		showRing = true;
		UITowerSelectedCard.Instance.showAndSet(this.GetType().Name, Level);
		UITowerUpgradeCard.Instance.showAndSet(this.GetType().Name, Level);
		UIBlockLower.Instance.guiTexture.texture = PrefabManager.UIBlockLowerSellUpgrade;
		UIBlockLowerSelectLeft.Instance.showAndSet(this.GetType().Name, Level);
		UIBlockLowerSelectRight.Instance.showAndSet(this.GetType().Name, Level);
		UIBlockLowerDetails.Instance.guiTexture.texture = PrefabManager.Invisible;
	}
	
	protected override void handleDeselect() { 
		GameSession.Instance.selectedObject = null;
		if (firingRadius != null) {
			firingRadius.renderer.enabled = false;
		}
		showRing = false;
		UIBlockLower.Instance.guiTexture.texture = PrefabManager.UIBlockLowerDefault;
		UITowerSelectedCard.Instance.hide();
		UITowerUpgradeCard.Instance.hide();
		UIBlockLowerSelectLeft.Instance.hide();
		UIBlockLowerSelectRight.Instance.hide();
	}
	protected override void handleButtonDown() {
		if (!GameSession.Instance.disableTouch)
			Select ();
	}
}