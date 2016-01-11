using UnityEngine;
using System.Collections;

public class TeleportController : MonoBehaviour {
	public float cornerTeleportTimeLimit;
	public float backTeleportTimeLimit;

	PlayerSight sight;
	Transform villain;
	VillainAI villainAI;
	NavMeshAgent villainNav;
	Transform player;

	void Awake () {
		GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		villain = enemy.transform;
		villainAI = enemy.GetComponent<VillainAI>();
		player = playerObj.transform;
		sight = playerObj.GetComponent<PlayerSight>();
		villainNav = villain.gameObject.GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TeleportVillain (Vector3 position, Vector3 pivet, int waypointIndex) {
		villainNav.enabled = false;
		villain.position = position;
		villain.LookAt(pivet);
		villainNav.enabled = true;
		villainAI.ChangeWaypointIndex(waypointIndex);
		villainAI.OverloadPatrolState();
	}
}
