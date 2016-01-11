using UnityEngine;
using System.Collections;

public class VillainAI : MonoBehaviour {
	public float chaseWaitTime = 2f;
	public float patrolWaitTime = 2f;
	public Transform[] patrolWayPoints;
	public event System.Action whileChasing;
	public event System.Action whilePatrolling;

	GameController gameController;
	PlayerSeen playerSeen;
	NavMeshAgent nav;
	VillainSight sight;


	Vector3 initialPosition;
	Quaternion initialRotation;
	int wayPointIndex = 0;
	public float chaseSpeed;
	public float patrolSpeed;
	float patrolTimer;
	float chaseTimer;
	bool chaseWarned = false;
	bool isHolding = true;
	float turnDirection = 1f;
	float turnAngle = 0f;
	System.Action stateAction;
	
	void Awake() {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameController.onSceneStart += OnEnterHoldState;
		gameController.onSceneEnd += OnExitHoldState;
		gameController.onGamePlayStart += OnExitHoldState;
		gameController.onGameWin += OnEnterHoldState;
		gameController.onGameLose += OnEnterHoldState;
		gameController.onGameRestart += Start;

		playerSeen = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerSeen>();

		// TODO:Debug
		initialPosition = transform.position;
		initialRotation = transform.rotation;

		nav = GetComponent<NavMeshAgent>();
		
	}
	
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		sight = GetComponentInChildren<VillainSight>();

		Transform wayPoints = GameObject.Find("Way Points").transform;
		int numWayPoints = wayPoints.childCount;
		patrolWayPoints = new Transform[numWayPoints];
		for (int i = 0; i < numWayPoints; i++) {
			patrolWayPoints[i] = wayPoints.GetChild(i);
		}

		wayPointIndex = 0;
		patrolTimer = 0f;
		chaseTimer = 0f;
		chaseWarned = false;
		turnDirection = 1f;
		turnAngle = 0f;

		nav.enabled = false;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		nav.enabled = true;

		OnEnterHoldState();
	}
	
	// Update is called once per frame
	// State machine changing state
	void Update () {
		ChangeState();
		if (stateAction != null) 
			stateAction();
	}
	
	void Chase () {
		if (whileChasing!=null){
			whileChasing();
		}
		nav.Resume();
		nav.speed = chaseSpeed;
		nav.destination = sight.playerSeenPosition;

		ColorChange.FindPlayer = true;

		if (nav.remainingDistance < nav.stoppingDistance) {
			chaseTimer += Time.deltaTime;

			if (chaseTimer > chaseWaitTime) {
				chaseTimer = 0f;
				sight.playerSeenPosition = playerSeen.resetPosition;
				turnDirection = -turnDirection;
				turnAngle = 0f;
			}
		}
		else {
			chaseTimer = 0f;
			turnAngle = 0f;
		}
	}
	
	void Patrol () {
		if (whilePatrolling!=null){
			whilePatrolling();
		}

		nav.Resume();
		nav.speed = patrolSpeed;
		nav.destination = patrolWayPoints[wayPointIndex].position;

		ColorChange.FindPlayer = false;

		if (nav.destination == playerSeen.resetPosition ||
		    nav.remainingDistance < nav.stoppingDistance) {
			patrolTimer += Time.deltaTime;

			if (patrolTimer > patrolWaitTime * 0.6f)
				TurnAround();

			if (patrolTimer > patrolWaitTime) {
				wayPointIndex = (wayPointIndex + 1) % patrolWayPoints.Length;
				patrolTimer = 0f;
				turnDirection = -turnDirection;
				turnAngle = 0f;
			}
		}
		else {
			patrolTimer = 0f;
			turnAngle = 0f;
		}
	}
	
	// Villain not moving
	// Used before game start, after game end, during cut scene
	void Hold () {
		//nav.Stop ();
	}
	
	void ChangeState () {
		// If there is no sceneario happening
		if (!isHolding) {
			if (sight.playerSeenPosition != playerSeen.resetPosition) {
				Debug.Log("Change to Chase");
				stateAction = Chase;
			}
			else {
				Debug.Log("Change to Patrol");
				stateAction = Patrol;
			}
		}
	}

	void TurnAround () {
		if (turnAngle < 200f) {
			transform.Rotate(transform.up, nav.angularSpeed * Time.deltaTime * turnDirection);
			turnAngle += nav.angularSpeed * Time.deltaTime;
		}
	}
	
	void OnEnterHoldState () {
		Debug.Log("Enter Hold State");
		isHolding = true;
		stateAction = Hold;
		nav.enabled = false;
		GetComponent<AudioSource>().mute = true;
	}
	
	void OnExitHoldState () {
		if (gameController.mazeEntered) {
			Debug.Log("Exit Hold State");
			isHolding = false;
			stateAction = Patrol;
			nav.enabled = true;
			GetComponent<AudioSource>().mute = false;
		}
	}

	void OnPlayerEnterMaze () {
		isHolding = false;
		stateAction = Patrol;
	}

	public void OverloadPatrolState() {
		OnExitHoldState();
	}

	public void ChangeWaypointIndex(int index) {
		wayPointIndex = index;
	}
}
