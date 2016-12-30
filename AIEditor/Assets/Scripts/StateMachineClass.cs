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
	//public ActionManager actions = new ActionManager(gameObject);
	public ActionManager actions;
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
		currentActiveState = StateList [0];

	}

	// Update is called once per frame
	void Update () 
	{

		#region Verificar Transiçoes
		//se transicao está ativa, para a açao atual e vai para o proximo estado
		//para cada estado vamos verificar se alguma das transiçoes fez trigger
		for (int i = 0; i < StateList.Count; i++) 
		{
			//verificar se alguma das transiçoes fez trigger
			for (int j = 0; j < StateList [i].listaTransitions.Count; j++) 
			{
				//StateList[i].listaTransitions[j].UpdatePArameterValue();
				//StateList[i].listaTransitions[j].CheckTransition();
				CheckTransition(StateList[i].listaTransitions[j]);
				if (StateList [i].listaTransitions [j].triggered) 
				{
					//se transiçao ativa passa para o estado alvo
					currentActiveState=StateList [i].listaTransitions [j].targetState;
				}
			}
		}
		#endregion

		//executar ação do estado actual
		//actions.listaActions[currentActiveState.ActionID].ExecuteAction();
		actions = GetComponent<ActionManager>();
		actions.listaActions[currentActiveState.ActionID].ExecuteAction();
		Debug.Log(currentActiveState.StateName);
	}

	void CheckTransition(Transition t)
	{
		BoolParameter realParameter = GetComponent<ParameterCreator> ().listaP.Find (p => p.Name == t.parameter.Name);
		if (realParameter.boolValue == t.parameter.triggerValue) {
			//se o valor do parametro for igual ao valor que dispara a transicao, entao ativa transiçao.
			t.triggered = true;
		}
		else
		{
			t.triggered = false;
		}
	}

	public void CreateNewState(string name, int numero)
	{
		
		//StateClass newState = this.gameObject.AddComponent<StateClass> ();
		StateClass newState = new StateClass();
		newState.StateName = name;
		newState.ID = numero;
		newState.listaTransitions = new List<Transition> ();
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
