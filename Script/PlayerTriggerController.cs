using UnityEngine;
using System.Collections;

public class PlayerTriggerController : MonoBehaviour {
	GameController gameController;
	PlayerController playerController;


	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Enemy" && other.isTrigger == false) {
			Debug.Log ("Enemy catches you!");
			gameController.EndGame();
		}
	}
}
