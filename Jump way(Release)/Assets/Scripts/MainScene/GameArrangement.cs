using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameArrangement : MonoBehaviour {

	public GameObject[] cubes;
	public GameObject buttons, m_cube, spawn_blocks;
	public Light dirLight, dirLight_2;
	public Animation cubes_anim, block;
	public Text playTxt, gameName, study, record;

	private bool clicked;

	void Update () {
		if (clicked && dirLight.intensity != 0)
			dirLight.intensity -= 0.035f;	
		if (clicked && dirLight_2.intensity >= 1.125f)
			dirLight_2.intensity -= 0.025f;
	}

	void OnMouseDown () {
		if (!clicked) {	
			StartCoroutine (delCubes ());
			clicked = true; // Только 1 раз!
			playTxt.gameObject.SetActive (false);
			study.gameObject.SetActive (true);
			record.gameObject.SetActive (true);
			gameName.text = "0";
			buttons.GetComponent <ScrollObjects> ().speed = -5f;
			buttons.GetComponent <ScrollObjects> ().checkPos = -200f;
			m_cube.GetComponent <Animation> ().Play ("StartGameCube");
			StartCoroutine (cubeToBlock ());
			m_cube.transform.localScale = new Vector3 (1f, 1f, 1f);
			cubes_anim.Play ();
		} else if (clicked && study.gameObject.activeSelf)
			study.gameObject.SetActive (false);
	}

	IEnumerator delCubes () {
		for (int i = 0; i < 3; i++) {
			yield return new WaitForSeconds (0.5f);
			cubes [i].GetComponent <FallCube> ().enabled = true;
		}

		spawn_blocks.GetComponent <SpawnBlocks> ().enabled = true;
		dirLight_2.transform.eulerAngles = new Vector3 (31.2f, 338f, 12f);
	}

	IEnumerator cubeToBlock () {
		yield return new WaitForSeconds (m_cube.GetComponent <Animation> ().clip.length + 0.5f);
		block.Play ();

		// Add Rigidbody component
		m_cube.AddComponent <Rigidbody> ();
	}

}
