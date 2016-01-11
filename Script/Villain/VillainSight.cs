using UnityEngine;
using System.Collections;

public class VillainSight : MonoBehaviour {
	PlayerSeen playerSeen;
	SphereCollider sphereTrigger;

	public Vector3 playerSeenPosition;
	public bool playerInSight = false;
	public float halfSightAngle;

	void Awake (){
		playerSeen = GameObject.Find("GameController").GetComponent<PlayerSeen>();
		sphereTrigger = GetComponent<SphereCollider>();

		// TODO: Debug
		GameObject.FindGameObjectWithTag("GameController")
			.GetComponent<GameController>().onGameRestart += Start;
	}

	// Use this for initialization
	void Start () {
		playerSeenPosition = playerSeen.resetPosition;
		playerInSight = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Player") {
			playerInSight = false;
			Vector3 direction = other.transform.position - (transform.position + transform.up);

			float angle = Vector3.Angle(transform.forward, direction);
			if (angle < halfSightAngle) {
				RaycastHit hit;
				bool hitNotNull = Physics.Raycast(transform.position, direction.normalized, out hit, sphereTrigger.radius * (transform.lossyScale.magnitude / 1.75f));
 				if (hitNotNull) {
					Debug.Log (hit.collider.name);
					if (hit.collider.tag == "Player" &&
				    hit.collider.isTrigger == false) {
					playerInSight = true;
					playerSeenPosition = hit.transform.position + hit.transform.up;
					Debug.Log("Player in Sight!");
					}
				}
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			playerInSight = false;
		}
	}
}
