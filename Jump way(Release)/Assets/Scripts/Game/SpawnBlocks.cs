using UnityEngine;
using System.Collections;

public class SpawnBlocks : MonoBehaviour {

	public GameObject block, allCubes, nowBlock, mainCube;
	private GameObject blockInst, oldBlock;
	private Vector3 blockPos;
	private float speed = 7f;
	private bool onPlace;

	void Start () {
		spawn ();
	}

	void Update () {
		if (blockInst.transform.position != blockPos && !onPlace) {
			if (nowBlock.name != "Last Cube") {
				mainCube.transform.position = Vector3.MoveTowards (mainCube.transform.position, new Vector3 (nowBlock.transform.position.x, nowBlock.transform.position.y + 0.825f , nowBlock.transform.position.z), Time.deltaTime * speed);            
				if (mainCube.GetComponent <Rigidbody> ())
					Destroy (mainCube.GetComponent <Rigidbody> ());
			}
			blockInst.transform.position = Vector3.MoveTowards (blockInst.transform.position, blockPos, Time.deltaTime * speed);
		} else if (blockInst.transform.position == blockPos) {
			onPlace = true;
			if (mainCube != null && !mainCube.GetComponent <Rigidbody> ())
				mainCube.AddComponent <Rigidbody> ();
		}

		if (CubeJump.jump && CubeJump.nextBlock) {
			oldBlock = nowBlock;
			nowBlock = blockInst;
			spawn ();
			onPlace = false;
		}

		if (nowBlock != null && nowBlock.transform.position != new Vector3 (-1.64f, 1.9f, -0.617f) && nowBlock.name != "Last Cube")
			nowBlock.transform.position = Vector3.MoveTowards (nowBlock.transform.position, new Vector3 (-1.64f, 1.9f, -0.617f), Time.deltaTime * speed);
		if (oldBlock != null && oldBlock.transform.position != new Vector3 (-5f, 5f, -0.617f))
			oldBlock.transform.position = Vector3.MoveTowards (oldBlock.transform.position, new Vector3 (-5f, 5f, -0.617f), Time.deltaTime * speed);
	}

	float RandScale () {
		float rand;
		if (Random.Range (0, 100) > 80)
			rand = Random.Range (1.2f, 2f);
		else
			rand = Random.Range (1.2f, 1.5f);
		return rand;
	}

	void spawn () { 		
		blockPos = new Vector3 (Random.Range (0.7f, 1.7f), -Random.Range (0.6f, 3.2f), -0.6f);
		blockInst = Instantiate (block, new Vector3 (5f, -6f, 0f), Quaternion.identity) as GameObject;
		blockInst.transform.localScale = new Vector3 (RandScale (), blockInst.transform.localScale.y, blockInst.transform.localScale.z);
		
	}

}