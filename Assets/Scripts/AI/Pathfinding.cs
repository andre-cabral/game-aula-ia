﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public Transform startObjectTransform;
	public Transform endObjectTransform;
	public float objectThickness = 1f;
	public float speed = 0.1f;

	List<Node> allNodes = new List<Node>();

	public List<Node> path = new List<Node>();

	void Awake(){
		GameObject[] nodesGameObjects = GameObject.FindGameObjectsWithTag(Tags.node);

		foreach(GameObject nodeGameObject in nodesGameObjects){
			allNodes.Add (new Node(nodeGameObject.transform.localPosition));
		}
	}

	void Update(){
		FindPath(startObjectTransform.position, endObjectTransform.position);
		Move();
	}

	//#####FINDPATH START
	//###################
	void FindPath(Vector3 startPosition, Vector3 endPosition){
		//transforms the start and end objects positions into Node objects
		Node startNode = new Node(startPosition);
		Node endNode = new Node(endPosition);

		//create a List with all the nodes and includes the end node.
		List<Node> allNodesWithEndNode = new List<Node>(allNodes);
		allNodesWithEndNode.Add(endNode);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		openSet.Add(startNode);

		while(openSet.Count > 0){
			Node currentNode = openSet[0];

			//verifies all the possible nodes and uses the one with the smallest total distance
			//or if the total distance is equal, selects the one that is closest to the end
			for(int i=0; i < openSet.Count; i++){
				if (openSet[i].totalDistance < currentNode.totalDistance || 
				    openSet[i].totalDistance == currentNode.totalDistance && 
				    openSet[i].endDistance < currentNode.endDistance){
					currentNode = openSet[i];
				}
			}

			//remove the selected node from the open array and send it to the closed array
			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			//finish the search after reaching the endnode
			if(currentNode == endNode){
				RetracePath(startNode, endNode);
				return;
			}


			foreach(Node neighbour in currentNode.findNeighbours(allNodesWithEndNode, objectThickness)){
				//go to the next node if the node is in the closedset
				if (closedSet.Contains(neighbour)) {
					continue;
				}

				//checks if the neighbour startdistance is greater than the start distance by the current node (in case the node was
				//reached by a longer way
				//after that, calculates the enddistance and add the current node as the parent
				//in the end, add the node to the open node (if the node wasnt in it already)
				float newStartDistance = currentNode.startDistance + Vector2.Distance(currentNode.nodePosition, neighbour.nodePosition);
				if(newStartDistance < neighbour.startDistance || !openSet.Contains(neighbour)){
					neighbour.startDistance = newStartDistance;
					neighbour.endDistance = Vector2.Distance(neighbour.nodePosition, endNode.nodePosition);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}

			}

		}
	}
	//#####FINDPATH END
	//#################

	void RetracePath(Node startNode, Node endNode){
		//list to put all the nodes in order
		List<Node> path = new List<Node>();

		Node currentNode = endNode;

		//checks node by node, and put them on the path list, starting with the endnode
		//it checks node by node getting their parents
		while(currentNode != startNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		this.path = path;
	}

	void Move(){
		Debug.DrawLine(transform.position, path[0].nodePosition);
		if(path[0] != null){
			transform.position = Vector2.MoveTowards(transform.position, path[0].nodePosition, speed);
		}
	}

}