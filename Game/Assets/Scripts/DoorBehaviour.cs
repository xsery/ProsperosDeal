using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour {

	public bool hasLock = false;
	public string doorId;

	public Transform player;

	public bool isWall;
	public bool isStair;
	bool onStair = false;
	public bool isLocked;
	public bool needKey;
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

		if (hasLock) {
			if (PlayerPrefs.GetInt (doorId) == 0) {
				isLocked = true;
			} else {
				isLocked = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (isLocked) {
			if ((col.gameObject.tag == "Player") && (needKey)) {
				if (col.gameObject.GetComponent<PlayerMovement> ().hasKey == true) {
					isLocked = false;
				}
			}
			Debug.Log ("Is Locked!");
		}

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
