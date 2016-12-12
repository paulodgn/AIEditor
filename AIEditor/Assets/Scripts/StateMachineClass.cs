using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum AvaiableStates
{
	Idle,
	Patrol
}


public class StateMachineClass : MonoBehaviour {


	public List<StateClass> StateList =  new List<StateClass>();

	//acoes que podem ser executadas em cada estado
	public ActionManager actions = new ActionManager();

	//estado em que se encontra
	private StateClass currentActiveState;

	//criador de parametros


	//variaveis para guardar informacao do formulario
	private string newStateName;
	private int newStateNumero;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () 
	{
		//StateList = new List<StateClass>();

	}

	// Update is called once per frame
	void Update () 
	{
		
	}


	public void CreateNewState(string name, int numero)
	{
		
		//StateClass newState = this.gameObject.AddComponent<StateClass> ();
		StateClass newState = new StateClass();
		newState.StateName = name;
		newState.ID = numero;

		StateList.Add (newState);

	}

	public void SaveNewStateData(string name, int numero)
	{
		newStateName = name;
		newStateNumero = numero;
		CreateNewState (newStateName, newStateNumero);
	}

	public void RemoveStateFromList()
	{
			StateList.RemoveAt (StateList.Count-1);
	}
}
