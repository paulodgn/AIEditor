using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//[System.Serializable]
[ExecuteInEditMode]
public class ActionManager : MonoBehaviour
{
	//public ActionsList actionList = new ActionsList();
	public List<StateAction> listaActions = new List<StateAction>();
	public GameObject obj;

	/*public ActionManager(GameObject obj)
	{
		this.obj = obj;
		CreateActionList ();
	}*/

	void Start()
	{
		//obj = GetComponent<GameObject> ();

		listaActions.Clear ();
		CreateActionList ();
	}

	public void CreateActionList()
	{
		StateAction run = new StateAction ("Run", (s) =>
			{
				//codigo
				gameObject.GetComponent<Renderer>().material.color = Color.green;
				Debug.Log("tou a correr que nem um doido!");
			});
		listaActions.Add (run);

		StateAction attack = new StateAction ("Attack", (s) =>
			{
				//codigo
				gameObject.GetComponent<Renderer>().material.color = Color.red;
				Debug.Log("tou a atacar que nem um doido!");
			});
		listaActions.Add (attack);

	}
}
