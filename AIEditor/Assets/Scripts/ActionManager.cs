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
		listaActions.Clear ();
		CreateActionList ();
	}

	public void CreateActionList()
	{
		StateAction run = new StateAction ("CorVerde", (s) =>
			{
				//codigo
				gameObject.GetComponent<Renderer>().material.color = Color.green;
				//Debug.Log("estou a ficar verde de raiva!");
			});
		listaActions.Add (run);

		StateAction attack = new StateAction ("CorVermelho", (s) =>
			{
				//codigo
				gameObject.GetComponent<Renderer>().material.color = Color.red;
				//Debug.Log("estou a ficar vermelho!");
			});
		listaActions.Add (attack);

		StateAction amarelo = new StateAction ("CorAmarelo", (s) =>
			{
				//codigo
				gameObject.GetComponent<Renderer>().material.color = Color.yellow;
				Debug.Log("estou a ficar amarelo!");
			});
		listaActions.Add (amarelo);

	}
}
