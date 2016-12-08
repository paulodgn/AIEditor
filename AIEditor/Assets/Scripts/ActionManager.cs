using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ActionManager
{
	//public ActionsList actionList = new ActionsList();
	public List<StateAction> listaActions = new List<StateAction>();

	public ActionManager()
	{
		CreateActionList ();
	}

	public void CreateActionList()
	{
		StateAction run = new StateAction ("Run", (s) =>
			{
				//codigo
				Debug.Log("tou a correr que nem um doido!");
			});
		listaActions.Add (run);

		StateAction attack = new StateAction ("Attack", (s) =>
			{
				//codigo
				Debug.Log("tou a atacar que nem um doido!");
			});
		listaActions.Add (attack);

	}
}
