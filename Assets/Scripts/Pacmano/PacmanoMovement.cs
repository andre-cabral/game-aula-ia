using UnityEngine;
using System.Collections;

public class PacmanoMovement : MonoBehaviour {

	public float speed = 0.4f;
	Vector2 dest = Vector2.zero;
	Rigidbody2D rigidBodyObject;
	CircleCollider2D colliderObject;
	float colliderRadius;
	public LayerMask scenarioWallLayerMask;
	Animator animatorObject;
	
	void Awake() {
		rigidBodyObject = GetComponent<Rigidbody2D>();
		animatorObject = GetComponent<Animator>();
		colliderObject = GetComponent<CircleCollider2D>();
		colliderRadius = colliderObject.radius;
		dest = transform.position;
	}
	
	void FixedUpdate() {
		Movement ();
	}


	//Movement start
	//#############
	//#############
	void Movement(){
		// Move closer to Destination
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		rigidBodyObject.MovePosition(p);
		
		// Check for Input if not moving
		if ((Vector2)transform.position == dest) {
			
			//vertical
			if(Input.GetAxisRaw(Buttons.axisHorizontal) == 0){
				if (Input.GetAxisRaw(Buttons.axisVertical) > 0 && valid(Vector2.up))
					dest = (Vector2)transform.position + Vector2.up;
				if (Input.GetAxisRaw(Buttons.axisVertical) < 0 && valid(-Vector2.up))
					dest = (Vector2)transform.position - Vector2.up;
			}
			
			//horizontal
			if(Input.GetAxisRaw(Buttons.axisVertical) == 0){
				if (Input.GetAxisRaw(Buttons.axisHorizontal) > 0 && valid(Vector2.right))
					dest = (Vector2)transform.position + Vector2.right;
				if (Input.GetAxisRaw(Buttons.axisHorizontal) < 0 && valid(-Vector2.right))
					dest = (Vector2)transform.position - Vector2.right;
			}
			
			//diagonals
			//up-right
			if (Input.GetAxisRaw(Buttons.axisVertical) > 0 
			    && Input.GetAxisRaw(Buttons.axisHorizontal) > 0 
			    && valid(Vector2.up)
			    && valid(Vector2.right)
			    && valid(Vector2.up + Vector2.right)
			    )
				dest = (Vector2)transform.position + Vector2.right + Vector2.up;
			
			//down-right
			if (Input.GetAxisRaw(Buttons.axisVertical) < 0 
			    && Input.GetAxisRaw(Buttons.axisHorizontal) > 0 
			    && valid(-Vector2.up)
			    && valid(Vector2.right)
			    && valid(-Vector2.up + Vector2.right)
			    )
				dest = (Vector2)transform.position + Vector2.right - Vector2.up;
			
			//down-left
			if (Input.GetAxisRaw(Buttons.axisVertical) < 0 
			    && Input.GetAxisRaw(Buttons.axisHorizontal) < 0 
			    && valid(-Vector2.up)
			    && valid(-Vector2.right)
			    && valid(-Vector2.up -Vector2.right)
			    )
				dest = (Vector2)transform.position - Vector2.right - Vector2.up;
			
			//up-left
			if (Input.GetAxisRaw(Buttons.axisVertical) > 0 
			    && Input.GetAxisRaw(Buttons.axisHorizontal) < 0 
			    && valid(Vector2.up)
			    && valid(-Vector2.right)
			    && valid(Vector2.up -Vector2.right)
			    )
				dest = (Vector2)transform.position - Vector2.right + Vector2.up;
			
		}
		
		// Animation Parameters
		Vector2 dir = dest - (Vector2)transform.position;
		animatorObject.SetFloat(PacmanoAnimationHashes.dirX, dir.x);
		animatorObject.SetFloat(PacmanoAnimationHashes.dirY, dir.y);
	}
	
	bool valid(Vector2 dir) {
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector2 pos = transform.position;
		Collider2D colliderFound = Physics2D.OverlapCircle(pos+dir, colliderRadius, scenarioWallLayerMask);
			
		return  !(colliderFound);
	}
	//Movement End
	//#############
	//#############
}
