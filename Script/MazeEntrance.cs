using UnityEngine;
using System.Collections;

public class MazeEntrance : MonoBehaviour {
	GameController gameController;
    SoundClip soundmanager;
    
	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Use this for initialization
	void Start () {
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundClip>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player" && !gameController.mazeEntered) {
            soundmanager.moveForward.Stop();
            PlayerController.moveLock = true;
            PlayerController.forward = false;
            PlayerController.collect = false;
            GameObject.Find("Villain").GetComponent<AudioSource>().Play();
			gameController.GamePlayStart();
            Invoke("animationPlay",1f);
            Invoke("unLock", 4.3f);
		}
	}


    void animationPlay() {
        GameObject.Find("OVRPlayerController").GetComponent<Animator>().enabled = true;
		GameObject.Find("MonsterTalking").GetComponent<AudioSource>().Play();
    }


    void unLock() {
        GameObject.Find("OVRPlayerController").GetComponent<Animator>().enabled = false;
        PlayerController.moveLock = false;
		soundmanager.whats_that.Play ();
    }
}
