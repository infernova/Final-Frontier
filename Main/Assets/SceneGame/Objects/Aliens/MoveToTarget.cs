using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour {
	public Transform goal;
	public float rotationSpeed = 90f;
	private Vector2 dirVec;
	public float moveSpeed = 40;
	void FixedUpdate() {
		/*
		 *
		 AlienMovementSpeed.Add(typeof(AlienArmouredGrunt).Name, 40);
		AlienMovementSpeed.Add(typeof(AlienBoss).Name, 25);
		AlienMovementSpeed.Add(typeof(AlienFast).Name, 70);
		AlienMovementSpeed.Add(typeof(AlienGrunt).Name, 50);
		AlienMovementSpeed.Add(typeof(AlienRebirthing).Name, 40);
		AlienMovementSpeed.Add(typeof(AlienRegenerating).Name, 40); *
		 */
		
		float currX = transform.position.x;
		float currZ = transform.position.z;
		Vector2 currPos = new Vector2(currX,currZ);
		float goalX = goal.position.x;
		float goalZ = goal.position.z;
		Vector2 goalPos = new Vector2(goalX, goalZ);
		dirVec = goalPos - currPos;
		Vector3 nextDirVec = (new Vector3 (dirVec.x, 0, dirVec.y)).normalized;
		Vector3 moveVec = nextDirVec * moveSpeed * Time.deltaTime;
		transform.position += moveVec;
	}
}
