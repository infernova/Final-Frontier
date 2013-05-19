using UnityEngine;
using System.Collections;

public class RandomWander : MonoBehaviour {
	float changeTime;
	Vector3 direction;
	public float movementSpeed = 40;
	float rotationSpeed = 180;
	// Update is called once per frame
	void Update () {
		changeTime -= Time.deltaTime;
		if (changeTime <= 0f) {
			Debug.Log("change direction");
			changeTime = UnityEngine.Random.Range(0.6f,3);
			float rads = UnityEngine.Random.Range(0f,1f) * Mathf.PI * 2;
    		direction = (new Vector3(Mathf.Cos(rads), 0, Mathf.Sin(rads))).normalized;
		}
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		transform.position += Time.deltaTime * direction * movementSpeed;
	}
}
