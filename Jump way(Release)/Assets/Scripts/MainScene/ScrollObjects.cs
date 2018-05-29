using UnityEngine;
using System.Collections;

public class ScrollObjects : MonoBehaviour {

	public float speed = 5f, checkPos = 0;
	private RectTransform rec;
	private bool start;
	private static bool _immediatelyShow;

	void Start () {
		rec = GetComponent <RectTransform> ();
	}

	void Update () {
		// Check for IOS platform and if time more than 4 make all animations
		if (!_immediatelyShow) {
			if (Application.platform == RuntimePlatform.IPhonePlayer && Time.time > 4.2f)
				start = true;
			else if (Application.platform != RuntimePlatform.IPhonePlayer)
				start = true;
		} else // Show immediately if we already play
			start = true;

		if (rec.offsetMin.y != checkPos && start) {
			_immediatelyShow = true;
			rec.offsetMin += new Vector2 (rec.offsetMin.x, speed);
			rec.offsetMax += new Vector2 (rec.offsetMax.x, speed);
		}
	}
}
