using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public Vector3 nodePosition;
	public float startDistance;
	public float endDistance;
	public Node parent;

	List<Node> neighbours = new List<Node>();
	//different sintax to write getters and setters
	public float totalDistance{
		get{
			return startDistance + endDistance;
		}
	}


	public Node(Vector3 worldPosition, float circlecastThickness){
		this.nodePosition = worldPosition;
	}

	public List<Node> getNeighbours(Node endNode, bool checkPlayerCollider, string ghostName){
		if(!checkPlayerCollider){
			List<Node> neighboursWithEndNode = new List<Node>(neighbours);
			if( CheckEndNode(endNode, checkPlayerCollider) ){
				neighboursWithEndNode.Add (endNode);
			}
			neighboursWithEndNode = CheckGhostsColliders(neighboursWithEndNode, ghostName);
			return neighboursWithEndNode;
		}
		else{
			List<Node> neighboursCheckPlayerColliderWithEndNode = new List<Node>();
			foreach(Node node in neighbours){
				bool collidedWithPlayer = false;
				foreach(RaycastHit2D collidersFound in Physics2D.LinecastAll(nodePosition, node.nodePosition)){
					if(collidersFound.collider.gameObject.tag == Tags.player){
						collidedWithPlayer = true;
					}
				} 
				if(!collidedWithPlayer){
					neighboursCheckPlayerColliderWithEndNode.Add(node);
				}
			}
			if( CheckEndNode(endNode, checkPlayerCollider) ){
				neighboursCheckPlayerColliderWithEndNode.Add(endNode);
			}
			neighboursCheckPlayerColliderWithEndNode = CheckGhostsColliders(neighboursCheckPlayerColliderWithEndNode, ghostName);
			return neighboursCheckPlayerColliderWithEndNode;
		}
	}

	List<Node> CheckGhostsColliders(List<Node> nodeList, string ghostName){
		List<Node> neighboursWithoutGhosts = new List<Node>();
		foreach(Node node in nodeList){
			bool collidedWithGhost = false;
			foreach(RaycastHit2D collidersFound in Physics2D.LinecastAll(nodePosition, node.nodePosition)){
				if(collidersFound.collider.gameObject.tag == Tags.ghost && collidersFound.collider.gameObject.name != ghostName){
					collidedWithGhost = true;
				}
			} 
			if(!collidedWithGhost){
				neighboursWithoutGhosts.Add(node);
			}
		}
		return neighboursWithoutGhosts;
	}

	public void InitNeighbours(float objectThickness){
		neighbours = findNeighbours(AllNodes.allNodes, objectThickness);
	}

	public bool CheckEndNode(Node endNode, bool checkPlayerCollider){
		bool collidedWithWall = false;
		bool collidedWithPlayer = false;

		bool sameNode = (endNode.nodePosition == nodePosition);

		//foreach(RaycastHit2D collidersFound in Physics2D.CircleCastAll(nodePosition, 0.8f, (endNode.nodePosition-nodePosition).normalized )){
		foreach(RaycastHit2D collidersFound in Physics2D.LinecastAll(nodePosition, endNode.nodePosition)){
			if(collidersFound.collider.gameObject.tag == Tags.scenarioWall){
				collidedWithWall = true;
			}
			if(collidersFound.collider.gameObject.tag == Tags.player && checkPlayerCollider){
				collidedWithPlayer = true;
			}
		}

		if(!collidedWithWall && !collidedWithPlayer && !sameNode){
			return true;
		}else{
			return false;
		}
	}

	public List<Node> findNeighbours(List<Node> allNodes, float objectThickness){

		List<Node> neighboursFound = new List<Node>();

		foreach(Node newNode in AllNodes.getAllNodesWithEndNode()){
			bool collidedWithWall = false;
			bool collidedWithPlayer = false;

			//checks if the node is not the same node
			bool sameNode = (newNode.nodePosition == nodePosition);
			//do raycasts (circlecasts) to see which nodes are neighbours to this
			//if the raycast (circlecast) hits a wall, the node is not a neighbour
			//the (target-actual).normalized calculates the direction of the circlecast

			//foreach(RaycastHit2D collidersFound in collidersFromCast){
			foreach(RaycastHit2D collidersFound in Physics2D.LinecastAll(nodePosition, newNode.nodePosition)){
				if(collidersFound.collider.gameObject.tag == Tags.scenarioWall){
					collidedWithWall = true;
				}
			}
			if(!collidedWithWall && !collidedWithPlayer && !sameNode){
				neighboursFound.Add(newNode);
			}
		}

		return neighboursFound;
	}

}
