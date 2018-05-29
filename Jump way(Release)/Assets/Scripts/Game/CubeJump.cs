﻿using UnityEngine;
using System.Collections;

public class CubeJump : MonoBehaviour {

	public static bool jump, nextBlock;
	public GameObject mainCube, buttons, lose_buttons;
	private bool animate, lose;
	private float scratch_speed = 0.5f, startTime, yPosCube;
	public static int count_blocks;

	void Awake () {
		// Static variables to false
		jump = false;
		nextBlock = false;
	}

	void Start () {
		StartCoroutine (CanJump ());
	}

	void FixedUpdate () {
		if (animate && mainCube.transform.localScale.y > 0.4f)
			PressCube (-scratch_speed);
		else if (!animate && mainCube != null) {
			if (mainCube.transform.localScale.y < 1f)
				PressCube (scratch_speed * 3f);
			else if (mainCube.transform.localScale.y != 1f)
				mainCube.transform.localScale = new Vector3 (1f, 1f, 1f);			
		}

		if (mainCube != null) {
			if (mainCube.transform.position.y < -5f) {
				Destroy (mainCube, 0.5f);
				print ("Player Lose");
				lose = true;
			}
		}

		if (lose)
			PlayerLose ();
	}

	void PlayerLose () {
		buttons.GetComponent <ScrollObjects> ().speed = 5f;
		buttons.GetComponent <ScrollObjects> ().checkPos = 50;
		if (!lose_buttons.activeSelf)
			lose_buttons.SetActive (true);
		lose_buttons.GetComponent <ScrollObjects> ().checkPos = 130;
		lose_buttons.GetComponent <AudioSource> ().Play ();
	}

	void OnMouseDown () {
		if (nextBlock && mainCube.GetComponent <Rigidbody> ()) {
			animate = true;
			startTime = Time.time;

			yPosCube = mainCube.transform.localPosition.y;
		}
	}

	void OnMouseUp () {
		if (nextBlock && mainCube.GetComponent <Rigidbody> () && startTime > 0f) {
			animate = false;

			// Jump
			jump = true;
			float force, diff;
			diff = Time.time - startTime;
			if (diff < 3f)
				force = 190 * diff;
			else
				force = 280f;
			if (force < 60f)
				force = 60f;

			mainCube.GetComponent <Rigidbody> ().AddRelativeForce (mainCube.transform.up * force);
			mainCube.GetComponent <Rigidbody> ().AddRelativeForce (mainCube.transform.right * -force);

			StartCoroutine (checkCubePos ());
			nextBlock = false;
		}
	}

	void PressCube (float force) {
		mainCube.transform.localPosition += new Vector3 (0f, force * Time.deltaTime, 0f);
		mainCube.transform.localScale += new Vector3 (0f, force * Time.deltaTime, 0f);
	}

	IEnumerator checkCubePos () {
		yield return new WaitForSeconds (1.5f);
		if (yPosCube == mainCube.transform.localPosition.y) {
			print ("Player Lose");
			lose = true;
		}
		else {
			while (!mainCube.GetComponent <Rigidbody> ().IsSleeping ()) {
				yield return new WaitForSeconds (0.05f);
				if (mainCube == null) 
					break;
			}

			if (!lose) {
				nextBlock = true;
				count_blocks++;
				print ("Next one");			
				mainCube.transform.localPosition = new Vector3 (-0.3f, mainCube.transform.localPosition.y, mainCube.transform.localPosition.z);
				mainCube.transform.eulerAngles = new Vector3 (0f, mainCube.transform.eulerAngles.y, 0f);
			}
		}
	}

	IEnumerator CanJump () {
		while (!mainCube.GetComponent <Rigidbody> ())
			yield return new WaitForSeconds (0.05f);
		yield return new WaitForSeconds (1f);
		nextBlock = true;
	}

}