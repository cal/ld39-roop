using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
**	References:
**	- https://www.youtube.com/watch?v=KBSHz-ee8Sk
*/

public class LadderZone : MonoBehaviour {

	private PeopleMover thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PeopleMover>();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onLadder = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onLadder = false;
		}
	}
}
