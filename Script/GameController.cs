using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public bool gameStarted = false;
	public bool mazeEntered = false;
	public bool scenePlaying = false;
	// the first time player sees the monster before entering the maze
	public bool monsterDemo = false;
	public bool allCollected = false;
	public event System.Action onGameStart;
	public event System.Action onGameRestart;
	public event System.Action onGamePlayStart;
	public event System.Action onGameLose;
	public event System.Action onGameWin;
	public event System.Action onSceneStart;
	public event System.Action onSceneEnd;

	SceneController sceneController;

	void Awake () {

		sceneController = GameObject.Find ("SceneController").GetComponent<SceneController>();

		onSceneStart += () => {
			PlayerController.moveLock = true;
			Debug.Log ("Lock player move");
		};

		onSceneEnd += () => {
			PlayerController.moveLock = false;
			Debug.Log ("Unlock player move");
		};

		onGameRestart += Start;

		onSceneStart += () => {
			LightningFlashes.LockForLightning = true;
			GameObject[] audioObj = GameObject.FindGameObjectsWithTag("AutoPlayAudio");
			foreach (GameObject obj in audioObj) {
				AudioSource audioSource = obj.GetComponent<AudioSource>();
				if (audioSource != null) {
					audioSource.Pause();
				}
			}
		};

		onSceneEnd += () => {
			LightningFlashes.LockForLightning = false;
			GameObject[] audioObj = GameObject.FindGameObjectsWithTag("AutoPlayAudio");
			foreach (GameObject obj in audioObj) {
				AudioSource audioSource = obj.GetComponent<AudioSource>();
				if (audioSource != null) {
					audioSource.Play();
				}
			}
		};

		onGameRestart += () => {
			LightningFlashes.LockForLightning = false;
			GameObject[] audioObj = GameObject.FindGameObjectsWithTag("AutoPlayAudio");
			foreach (GameObject obj in audioObj) {
				AudioSource audioSource = obj.GetComponent<AudioSource>();
				if (audioSource != null) {
					audioSource.Play();
				}
			}
		};

		Cursor.visible = false;
	}

	// Use this for initialization
	void Start () {
		gameStarted = false;
		mazeEntered = false;
		scenePlaying = false;
		monsterDemo = false;
		allCollected = false;

		// TODO: disable this for release
		// if (onSceneStart != null) onSceneStart ();
	}
	
	// Update is called once per frame
	void Update () {
		KeyBoardBackdoor();
	}

	public void GameStart() {
		gameStarted = true;
		if (onGameStart != null) {
			onGameStart();
		}
	}

	void KeyBoardBackdoor () {
		if (Input.GetKeyDown(KeyCode.O)) {
			if (onSceneStart != null)
				onSceneStart ();
		}
		else if (Input.GetKeyDown(KeyCode.L)) {
			if (onSceneEnd != null)
				onSceneEnd ();
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) {
			ExitGame ();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(3);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(4);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6)) {
			GameObject.Find ("SceneController").GetComponent<SceneController>().StartVideo(5);
		}
	}

	public void GamePlayStart () {
		mazeEntered = true;
		if (onGamePlayStart != null)
			onGamePlayStart();
	}

	public void EndGame () {
		if (!allCollected) {
			Invoke("RestartGame", sceneController.DelayWhenLose());
			if (onSceneStart != null)
				onSceneStart();
			if (onGameLose != null)
				onGameLose();
		}
		else {
			Invoke ("ExitGame", sceneController.DelayWhenWin() - 0.5f);
			if (onGameWin != null)
				onGameWin();
		}
	}

	void RestartGame() {
		// Application.LoadLevel(0);
		if (onGameRestart != null)
			onGameRestart();
	}

	void ExitGame() {
		Application.Quit();
	}

	public void SceneStart () {
		if (onSceneStart != null) 
			onSceneStart();
	}

	public void SceneEnd () {
		if (onSceneEnd != null)
			onSceneEnd();
	}
}
