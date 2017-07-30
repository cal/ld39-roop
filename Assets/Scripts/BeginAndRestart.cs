using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginAndRestart : MonoBehaviour {

	public void BeginGame() {
		SceneManager.LoadScene("Game");
	}

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1.0f;
	}

}
