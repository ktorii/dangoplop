using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float lifeTimeDelay;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTimeDelay);
	}
}