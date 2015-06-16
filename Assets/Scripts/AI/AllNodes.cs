using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AllNodes {

	public static List<Node> allNodes = new List<Node>();
	public static Node endNode;

	public static List<Node> getAllNodesWithEndNode(){
		List<Node> allNodesWithEndNode = new List<Node>(allNodes);
		allNodesWithEndNode.Add(endNode);

		return allNodesWithEndNode;
	}
}
