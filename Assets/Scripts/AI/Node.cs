using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public Vector3 nodePosition;
	public float startDistance;
	public float endDistance;
	public Node parent;
	//different sintax to write getters and setters
	public float totalDistance{
		get{
			return startDistance + endDistance;
		}
	}



	public Node(Vector3 worldPosition){
		this.nodePosition = worldPosition;
	}

	public List<Node> findNeighbours(List<Node> allNodes, float objectThickness, bool checkPlayerCollider){
		List<Node> neighbours = new List<Node>();

		foreach(Node newNode in allNodes){
			bool collidedWithWall = false;
			bool collidedWithPlayer = false;

			//checks if the node is not the same node
			bool sameNode = (newNode.nodePosition == nodePosition);

			//do raycasts (circlecasts) to see which nodes are neighbours to this
			//if the raycast (circlecast) hits a wall, the node is not a neighbour
			//the (target-actual).normalized calculates the direction of the circlecast
			foreach(RaycastHit2D collidersFound in Physics2D.CircleCastAll(nodePosition, objectThickness, (newNode.nodePosition-nodePosition).normalized)){
				if(collidersFound.collider.gameObject.tag == Tags.scenarioWall){
					collidedWithWall = true;
				}
				if(collidersFound.collider.gameObject.tag == Tags.player && checkPlayerCollider){
					collidedWithPlayer = true;
				}
			}
			if(!collidedWithWall && !collidedWithPlayer && !sameNode){
				neighbours.Add(newNode);
			}

		}

		return neighbours;
	}

}
