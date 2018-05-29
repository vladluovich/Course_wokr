using UnityEngine;
using System.Collections;

public class DeleteBlock : MonoBehaviour {

	void OnTriggerEnter (Collider other) {		
		if (other.tag == "Cube")
			Destroy (other.gameObject);
	}

}
