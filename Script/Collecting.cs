using UnityEngine;
using System.Collections;

public class Collecting : MonoBehaviour {
	public int pieceCollected = 0;
    CapsuleCollider arm;
	GameController gameController;
	SceneController sceneController;
	SoundClip soundmanager;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameController.onGameRestart += Start;

		sceneController = GameObject.Find ("SceneController")
			.GetComponent<SceneController>();
		soundmanager = GameObject.Find("SoundManager").GetComponent<SoundClip>();
	}

	// Use this for initialization
	void Start () {
		pieceCollected = 0;
        arm = this.gameObject.GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        collectDetection();
	}

    void collectDetection() {
        if (PlayerController.collect == true)
        {
            arm.height = 15;
        }
        else arm.height = 1;
    }

    void OnTriggerEnter(Collider other) {
		if (other.name == "pieces" && PlayerController.collect == true)
		{
			pieceCollected++;

			if (pieceCollected == 1) 
				sceneController.StartVideo(1);
			else if (pieceCollected == 2) 
				sceneController.StartVideo(2);
			else if (pieceCollected == 3){
				sceneController.StartVideo(3);
				gameController.allCollected = true;
				gameController.EndGame();
			}

			Debug.Log (pieceCollected);
           	other.gameObject.SetActive(false);  
        }
    }
}
