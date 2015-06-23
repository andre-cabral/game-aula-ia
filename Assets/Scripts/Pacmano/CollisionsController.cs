using UnityEngine;
using System.Collections;

public class CollisionsController : MonoBehaviour {

	PillTimeController pillTimeController;
	int pillsRemaining = 999;

	void Awake(){
		pillTimeController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<PillTimeController>();
		pillsRemaining = GameObject.FindGameObjectsWithTag(Tags.pacote).Length;
		pillsRemaining += GameObject.FindGameObjectsWithTag(Tags.superPacote).Length;
	}

	void Update(){
		if(pillsRemaining <= 0){
			Application.LoadLevel(0);
		}
	}
	
	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag == Tags.pacote){
			Destroy(co.gameObject);
			pillsRemaining--;
		}

		if(co.tag == Tags.superPacote){
			Destroy(co.gameObject);
			pillTimeController.gotPill();
			pillsRemaining--;
		}

		if(co.tag == Tags.ghost){
			if(pillTimeController.getGhostsEscaping()){
				Destroy(co.gameObject);
			}else{
				Application.LoadLevel(0);
			}
		}

	}
}
