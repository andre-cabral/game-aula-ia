using UnityEngine;
using System.Collections;

public class Pacote : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D co) {
		if (co.tag == Tags.pacote){
			Destroy(co.gameObject);
		}
	}
}
