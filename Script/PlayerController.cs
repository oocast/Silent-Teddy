using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public static PSMoveWrapper psMoveWrapper;
    public static bool forward;
    public static bool collect;
    public static bool moveLock;

	GameController gameController;
    SoundClip soundmanager;
	bool lockforCollecting;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		gameController.onGameLose += MoveLock;
		gameController.onGameWin += MoveLock;
		gameController.onGameRestart += Start;
		soundmanager = GameObject.Find ("SoundManager").GetComponent<SoundClip>();

		psMoveWrapper = gameObject.GetComponent<PSMoveWrapper>();
		psMoveWrapper.Connect();

		// TODO: activate this line if the player can't stop reaching the monster
		//gameController.onGamePlayStart += MoveLockThenUnlock;
	}

    // Use this for initialization
    void Start()
    {
		forward = false;
		collect = false;
		moveLock = false;
		lockforCollecting = false;
        

    }

    void Update()
    {
        moveController();
    }

    void moveController() {
        if ( moveLock == false)
        {
            if (psMoveWrapper.valueT[0] > 200 || Input.GetKey(KeyCode.Mouse1))
            {
				if (!soundmanager.moveForward.isPlaying)
                soundmanager.moveForward.Play();
                forward = true;
            }
            else
            {
                forward = false;
                soundmanager.moveForward.Stop();
            }
			if (psMoveWrapper.valueT[2] > 200||psMoveWrapper.valueT[1] > 200 || Input.GetKey(KeyCode.Mouse0))
            {
                collect = true;
				if (!soundmanager.collecting.isPlaying && lockforCollecting == false)
				{
					lockforCollecting = true;
					soundmanager.collecting.Play();
				}
			}
            else
            {
                collect = false;
				lockforCollecting = false;
            }

        }
    }

	void MoveLockThenUnlock () {
		Invoke ("MoveLock", 0f);
		Invoke ("MoveUnlock", 4f);
	}
    
	void MoveLock() {
		moveLock = true;
	}

	void MoveUnlock() {
		moveLock = false;
	}
}
