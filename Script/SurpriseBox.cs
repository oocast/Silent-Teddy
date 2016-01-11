using UnityEngine;
using System.Collections;

public class SurpriseBox : MonoBehaviour {
	public Vector3[] dir;
	public float[] distance;
	public int waypointIndex;
	public bool alwaysTriggerOnce;
	public int triggerOnceValidEntrance;

	float coolDownTime = 60f;
	float triggerTime = -50f;
	TeleportController teleport;
	PlayerSight sight;

	void Awake () {
		teleport = GameObject.Find("TeleportController").GetComponent<TeleportController>();
		sight = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSight>();
	
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onGameRestart += Start;
	}

	// Use this for initialization
	void Start () {
		triggerTime = -50f;
	}
	
	// Update is called once per frame
	void Update () {
		if (alwaysTriggerOnce == true && triggerTime > 0f) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if ((sight.notInViewTimer > teleport.cornerTeleportTimeLimit && 
			    sight.notInViewTimer < teleport.backTeleportTimeLimit &&
			    Time.time - triggerTime > coolDownTime) ||
			    (alwaysTriggerOnce == true &&
			 	triggerTime < 0f)) {

				int maxIdx = 0;
				float maxValue = -Mathf.Infinity;
				Vector3 direction = other.transform.position - transform.position;
				for (int i = 0; i < dir.Length; i++) {
					float value = Vector3.Dot(direction, dir[i]);
					if (value > maxValue) {
						maxIdx = i;
						maxValue = value;
					}
				}

				if (alwaysTriggerOnce && maxIdx != triggerOnceValidEntrance) {
					return;
				}

				int teleportIdx = 0;
				if (dir.Length == 4) {
					// enter from vertical way in the T shape
					if (maxIdx % 2 == 1) {
						if (Random.Range(0f,1f) > 0.5f) {
							teleportIdx = (maxIdx + 1) % 4;
						}
						else {
							teleportIdx = (maxIdx - 1) % 4;
						}
					}
					// enter from horizontal way in the T shape
					else {
						teleportIdx = 1;
					}
				}
				else if (dir.Length == 2) {
					teleportIdx = (maxIdx + 1) % 2;
				}
				Vector3 position = transform.position + transform.up + dir[teleportIdx] * distance[teleportIdx];
				teleport.TeleportVillain(position, transform.position, waypointIndex);
				sight.ResetTimer();	
				triggerTime = Time.time;
			}
		}
	}
}
