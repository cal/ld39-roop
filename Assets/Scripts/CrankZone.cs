using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankZone : MonoBehaviour {

	private PeopleMover thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PeopleMover>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onCrank = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onCrank = false;
		}
	}
}
