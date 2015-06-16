using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class createpills : MonoBehaviour {
	public GameObject pill;
	public GameObject allPillsContainer;
	public GameObject allNodesContainer;
	// Use this for initialization
	void Start () {

		//###############


	}

	void RenameNodes(){
		for(int i=0; i<allNodesContainer.transform.childCount; i++){
			GameObject childToRename = allNodesContainer.transform.GetChild(i).gameObject;
			childToRename.name = "Node "+i;
			
			/*
			string pattern = "(\d+)";
			string replacement = "$1";
			Regex rgx = new Regex(pattern);
			string result = rgx.Replace(childToRename.name, replacement);
			childToRename.name = result;
			*/
		}
	}

	void RenamePills(){
		for(int i=0; i<allPillsContainer.transform.childCount; i++){
			GameObject childToRename = allPillsContainer.transform.GetChild(i).gameObject;

			childToRename.name = childToRename.name.Replace("(Clone)","");
			childToRename.name = childToRename.name.Replace("(","");
			childToRename.name = childToRename.name.Replace(")","");

			/*
			string pattern = "(\d+)";
			string replacement = "$1";
			Regex rgx = new Regex(pattern);
			string result = rgx.Replace(childToRename.name, replacement);
			childToRename.name = result;
			*/
		}
	}

	void CreatePills(){
		for(int x=0; x<=27; x++){
			for(int y=0; y<=30; y++){
				Collider2D colliderFound = Physics2D.OverlapCircle(new Vector2(x,y), 1f);
				if(!colliderFound)
					Debug.Log("x="+x+"\ny="+y);
				GameObject.Instantiate(pill,new Vector3(x,y,0), pill.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
