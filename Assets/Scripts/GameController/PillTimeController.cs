using UnityEngine;
using System.Collections;

public class PillTimeController : MonoBehaviour {

	public float pillTime;
	public float pillTimeRemaining = 0f;
	public bool ghostsEscaping = false;

	void Update () {
		if(pillTimeRemaining > 0){
			if(ghostsEscaping == false){
				ghostsEscaping = true;
			}
			pillTimeRemaining -= Time.deltaTime;
		}else{
			ghostsEscaping = false;
		}
	}

	public bool getGhostsEscaping(){
		return ghostsEscaping;
	}

	public float getPillTimeRemaining(){
		return pillTimeRemaining;
	}

	public void gotPill(){
		pillTimeRemaining = pillTime;
	}
}
