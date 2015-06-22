using UnityEngine;
using System.Collections;

public class CollisionsController : MonoBehaviour {

	PillTimeController pillTimeController;

	void Awake(){
		pillTimeController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<PillTimeController>();
	}
	
	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag == Tags.pacote){
			Destroy(co.gameObject);
		}

		if(co.tag == Tags.superPacote){
			Destroy(co.gameObject);
			pillTimeController.gotPill();
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
