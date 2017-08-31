using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStopTime : MonoBehaviour {

	public float laserStopTime;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, laserStopTime);
	}

	public float LaserTime(){
		return laserStopTime;
	}
}
