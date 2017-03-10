using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	private static PlayerMovement instance;

	Rigidbody2D rigid;
	float dir;
	public float speed;

	bool isFlip = false;

	public bool spawnOnLeft = true;
	public bool spawned = true;

	string lastScene;

	public bool hasKey = false;

	void Awake(){
		if (!instance) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		Debug.Log ("TEST!");
		lastScene = SceneManager.GetActiveScene ().name;
	}
	
	// Update is called once per frame
	void Update () {
		dir = Input.GetAxis ("Horizontal");
		if (dir != 0){
			Walk (dir);
		}
//		if (!spawned) {
//			SpawnPosition ();
//			spawned = true;
//		}
		if (lastScene != SceneManager.GetActiveScene ().name) {
			SpawnPosition ();
			lastScene = SceneManager.GetActiveScene ().name;
		}


	}

	void Walk(float dir){ // Walk function
		rigid.velocity = new Vector2 ((speed*dir), rigid.velocity.y);
		//anim.SetBool ("running", true);
		if ((dir < 0) && (!isFlip)) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			isFlip = true;
		} else if ((dir > 0) && (isFlip)) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			isFlip = false;
		}
	}

	void SpawnPosition(){
		Debug.Log ("spawn" + lastScene);
		if (!spawnOnLeft) {
			rigid.velocity = Vector2.zero;
			transform.position = GameObject.Find ("SP_Right").transform.position;
		} else if (spawnOnLeft) {
			rigid.velocity = Vector2.zero;
			transform.position = GameObject.Find ("SP_Left").transform.position;
		}
		spawned = true;
	}
}
