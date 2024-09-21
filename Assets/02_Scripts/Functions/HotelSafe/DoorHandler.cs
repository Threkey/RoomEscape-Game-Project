using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {

    public BaseSafeHandler safeHandler;

    public bool isOpen = false;

    Animator doorAnimator;

	// Use this for initialization
	void Start () {
        doorAnimator = GetComponent<Animator>();
	}

    public void OpenDoor() {
        doorAnimator.Play("OpenDoor", -1, 0f);
        isOpen = true;
    }

    public void CloseDoor()
    {
        doorAnimator.Play("CloseDoor",-1,0f);
        isOpen = false;
    }
}
