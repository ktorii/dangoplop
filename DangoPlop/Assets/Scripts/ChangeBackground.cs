using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour {
	public Sprite ogBackground;
	public Sprite redBackground;
	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeBackground(){
		renderer.sprite = redBackground;
		StartCoroutine (Wait ());
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds (1);
		renderer.sprite = ogBackground;
	}
}
