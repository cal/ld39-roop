using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMover : MonoBehaviour {

	public GameObject leftMousePeek;
	public GameObject rightMousePeek;
	public GameObject leftMouseBuzzing1;
	public GameObject leftMouseBuzzing2;
	public GameObject leftMouseBuzzing3;
	public GameObject rightMouseBuzzing;
	public GameObject rightMousePinched;

	private float mouseSpawnTickSize = 10.0f;
	private float mouseSpawnTickTime;

	private float mouseMoveTickSize = 3.0f;
	private float mouseMoveTickTime;

	// Use this for initialization
	void Start () {
		leftMousePeek.SetActive(false);
		rightMousePeek.SetActive(false);
		leftMouseBuzzing1.SetActive(false);
		leftMouseBuzzing2.SetActive(false);
		leftMouseBuzzing3.SetActive(false);
		rightMouseBuzzing.SetActive(false);
		rightMousePinched.SetActive(false);

		mouseSpawnTickTime = mouseSpawnTickSize;
		MouseMove();
	}
	
	// Update is called once per frame
	void Update () {
		mouseSpawnTickTime -= Time.deltaTime;
		if (mouseSpawnTickTime <= 0.0f) {
			MouseSpawn();
		}

		mouseMoveTickTime -= Time.deltaTime;
		if (mouseMoveTickTime <= 0.0f) {
			MouseMove();
		}
	}

	void MouseSpawn() {
		mouseSpawnTickTime = mouseSpawnTickSize;

		if (Random.value >= 0.5f) {
			// left hand side
			if (!leftMousePeek.activeSelf) {
				leftMousePeek.SetActive(true);
			}
		}
		else {
			// right hand side
			if (!rightMousePeek.activeSelf) {
				rightMousePeek.SetActive(true);
			}
		}
	}

	void MouseMove() {
		mouseMoveTickTime = mouseMoveTickSize;

		if (leftMousePeek.activeSelf) {
			switch(Random.Range(0,4)) {
				case 0:
					// touch power outlet 1
					leftMousePeek.SetActive(false);
					leftMouseBuzzing1.SetActive(true);
					break;
				case 1:
					// touch power outlet 2
					leftMousePeek.SetActive(false);
					leftMouseBuzzing2.SetActive(true);
					break;
				case 2:
					// touch power outlet 3
					leftMousePeek.SetActive(false);
					leftMouseBuzzing3.SetActive(true);
					break;
				case 3:
					// go back into mouse hole
					leftMousePeek.SetActive(false);
					break;
				case 4:
				default:
					// do nothing
					break;
			}
		}

		if (rightMousePeek.activeSelf) {
			switch(Random.Range(0,3)) {
				case 0:
					// get stuck in mechanism
					rightMousePeek.SetActive(false);
					rightMousePinched.SetActive(true);
					break;
				case 1:
					// touch power outlet
					rightMousePeek.SetActive(false);
					rightMouseBuzzing.SetActive(true);
					break;
				case 2:
					// go back into mouse hole
					rightMousePeek.SetActive(false);
					break;
				case 3:
				default:
					// do nothing
					break;
			}
		}
	}

}
