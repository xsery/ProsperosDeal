using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour {

	public Transform player;

	public bool isWall;
	public bool isStair;
	bool onStair = false;
	public bool isLocked;
	public Object toRoom;

	void Start(){
		if (isStair) {
			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}

	void Update(){
		if (onStair) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				player = GameObject.Find ("Player").transform;
				if (this.gameObject.name == "R_stair") {
					player.GetComponent<PlayerMovement> ().spawnOnLeft = true;
					player.GetComponent<PlayerMovement> ().spawned = false;
				} else if (this.gameObject.name == "L_stair") {
					player.GetComponent<PlayerMovement> ().spawnOnLeft = false;
					player.GetComponent<PlayerMovement> ().spawned = false;
				}
				SceneManager.LoadScene (toRoom.name);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if ((!isLocked) && (!isStair) && (col.gameObject.GetComponent<PlayerMovement> ().spawned == true)) {
			if ((col.gameObject.tag == "Player") && (!isWall)) {
				if (this.gameObject.name == "R_door") {
					col.gameObject.GetComponent<PlayerMovement> ().spawnOnLeft = true;
					col.gameObject.GetComponent<PlayerMovement> ().spawned = false;
				} else if (this.gameObject.name == "L_door") {
					col.gameObject.GetComponent<PlayerMovement> ().spawnOnLeft = false;
					col.gameObject.GetComponent<PlayerMovement> ().spawned = false;
				}
				SceneManager.LoadScene (toRoom.name);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			onStair = true;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			onStair = false;
		}
	}
}
