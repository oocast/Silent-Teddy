using UnityEngine;
using System.Collections;

public class PlayerSight : MonoBehaviour {
	public float sightRange;
	public float sightHalfAngle;
	public float notInViewTimer = 0f;

	bool villainInSight = false;
	Transform villain;
	VillainAI villainAI;
	GameController gameController;
	Vector3 initialPosition;
	Quaternion initialRotation;

	void Awake () {
		GameObject villainObj = GameObject.FindGameObjectWithTag("Enemy");
		villain = villainObj.transform;
		villainAI = villainObj.GetComponent<VillainAI>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		initialPosition = transform.position;
		initialRotation = transform.rotation;

		villainAI.whileChasing += ResetTimer;
		gameController.onGameRestart += Start;
	}

	// Use this for initialization
	void Start () {
		villainInSight = false;
		notInViewTimer = 0f;

		transform.position = initialPosition;
		transform.rotation = initialRotation;
	}
	
	// Update is called once per frame
	void Update () {
		CheckSight();
	}

	void CheckSight () {
		Vector3 direction = villain.position - transform.position;
		villainInSight = false;
		if (direction.magnitude < sightRange &&
		    Vector3.Angle(transform.forward, direction) < sightHalfAngle) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, direction.normalized, out hit, sightRange)) {
				if (hit.collider.tag == "Enemy" && !hit.collider.isTrigger) {
					villainInSight = true;
				}
			}
		}

		if (villainInSight) {
			ResetTimer();
		}
		else if (gameController.mazeEntered){
			notInViewTimer += Time.deltaTime;
		}
	}

	public void ResetTimer () {
		notInViewTimer = 0f;
	}
}
