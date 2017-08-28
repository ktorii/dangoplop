using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float laserRate;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, laserRate);
	}
}