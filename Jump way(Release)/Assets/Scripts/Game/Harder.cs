using UnityEngine;
using System.Collections;

public class Harder : MonoBehaviour {

	public GameObject detectClicks;
	private bool hard;

	void Update () {
		if (CubeJump.count_blocks > 0) {
			if (CubeJump.count_blocks % 7 == 0 && !hard) {
				print ("Harder");
				Camera.main.GetComponent <Animation> ().Play ("Harder");
				detectClicks.transform.position = new Vector3 (10.11f, 6.29f, -5.63f);
				detectClicks.transform.eulerAngles = new Vector3 (29.43f, -60f, 0f);
				hard = true;
			} else if ((CubeJump.count_blocks % 7) - 1 == 0 && hard) {
				hard = false;
				print ("Easier");
				detectClicks.transform.position = new Vector3 (0f, 0f, -8f);
				detectClicks.transform.eulerAngles = new Vector3 (0f, 0f, 0f);
				Camera.main.GetComponent <Animation> ().Play ("Easier");
			}
		}			
	}
}
