using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse2 : MonoBehaviour {
	private PeopleMover thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PeopleMover>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onMouse2 = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onMouse2 = false;
		}
	}
}
