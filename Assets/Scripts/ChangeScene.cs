using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void Scene_Change (string SceneName) {
		SceneManager.LoadScene(SceneName);
	}

	public void ExitGame () {
		Debug.Log("QUIT!");
		Application.Quit();
	}
}