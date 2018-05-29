using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Buttons : MonoBehaviour {

	public float bigger = 0.55f, lower = 0.5f;


	void OnMouseDown () {
		transform.localScale = new Vector3 (bigger, bigger, bigger);
	}

	void OnMouseUp () {
		transform.localScale = new Vector3 (lower, lower, lower);
	}

	void OnMouseUpAsButton () {
		GetComponent <AudioSource> ().Play ();
		switch (gameObject.name) {
		case "Restart":
			SceneManager.LoadScene ("main");
			break;
		case "FaceBook":
			Application.OpenURL ("https://facebook.com");
			break;
		
		case "Google+":
			Application.OpenURL ("https://google.com");
			break;
		}
	}

}
