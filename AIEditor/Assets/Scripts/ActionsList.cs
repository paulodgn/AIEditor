using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ActionsList 
{
	public List<StateAction> listaActions;

	public ActionsList()
	{
		listaActions = new List<StateAction> ();
	}

	public void AddAction(StateAction action)
	{
		listaActions.Add (action);
	}
}
