using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]  
public class PathManager : MonoBehaviour {
	public Transform[] path;
	public float[] distToEnd;
	// Use this for initialization
	void Start () {
		distToEnd = new float[path.Length];
		// Calculate intermediate distances from the 2nd last 
		// while Accumulating backwards
		for (int i= path.Length-2; i >= 0; i--) {
			distToEnd[i] = (path[i+1].position - path[i].position).magnitude + distToEnd[i+1];
		}
	}
	
}
