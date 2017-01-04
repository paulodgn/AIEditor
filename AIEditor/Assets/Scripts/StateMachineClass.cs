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
	private StateClass lastActiveState;
	//criador de parametros


	//variaveis para guardar informacao do formulario
	private string newStateName;
	private int newStateNumero;

	private bool exitActionExecuted = false;
	private bool enterActionExecuted = false;

	//debug
	BoolParameter realParameter;
	ParameterCreator pam;
	Transition transition;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () 
	{
		currentActiveState = StateList [0];
		lastActiveState = currentActiveState;
		pam = GetComponent<ParameterCreator>();
		PrintStateList ();
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
			for (int j = 0; j < StateList[i].listaTransitions.Count; j++) 
			{

				if(StateList[i].listaTransitions[j]!=null)
					CheckTransition(StateList[i].listaTransitions[j]);
				
				if (StateList[i].listaTransitions [j].triggered) 
				{
					
					//se transiçao ativa passa para o estado alvo
					currentActiveState=StateList[i].listaTransitions [j].targetState;

					//se existe transiçao antes de mudar de estado executa a exit action do estado atual
					/*if(currentActiveState != lastActiveState)
					{
						exitActionExecuted=false;
					}
					actions = GetComponent<ActionManager>();
					if(!exitActionExecuted)
					{
						actions.listaActions[lastActiveState.ExitActionID].ExecuteAction();
						lastActiveState = currentActiveState;
						exitActionExecuted=true;
					}*/

					//executa a entry action do novo estado
					ExecuteEntryAction();
				}
			}
		}
		#endregion

		//executar ação do estado actual
		//actions.listaActions[currentActiveState.ActionID].ExecuteAction();
		actions = GetComponent<ActionManager>();
		actions.listaActions[currentActiveState.ActionID].ExecuteAction();
		for (int i = 0; i < currentActiveState.listaTransitions.Count; i++) 
		{
			currentActiveState.listaTransitions [i].triggered = false;
		}
	}

	void CheckTransition(Transition t)
	{
		transition = t;
		//BoolParameter realParameter = GetComponent<ParameterCreator> ().listaP.Find (p => p.Name == t.parameter.Name);

		if (t != null) 
		{
			for (int i = 0; i < pam.listaP.Count; i++) {
				if (pam.listaP [i].Name == t.parameter.Name)
					realParameter = pam.listaP [i];
			}

			
			if (realParameter.boolValue == t.parameter.triggerValue) {
				//se o valor do parametro for igual ao valor que dispara a transicao, entao ativa transiçao.
				t.triggered = true;
			} else {
				t.triggered = false;
			}
		}
	}

	void ExecuteEntryAction()
	{
		//cada vez que muda de estado executa a entry action respetiva
		if(currentActiveState != lastActiveState)
		{
			enterActionExecuted=false;
		}
		//ao entrar no estado executa a entrty action
		actions = GetComponent<ActionManager>();
		if(!enterActionExecuted)
		{
			actions.listaActions[lastActiveState.ExitActionID].ExecuteAction();
			actions.listaActions[currentActiveState.EntryActionID].ExecuteAction();
			lastActiveState = currentActiveState;
			enterActionExecuted = true;
		}
	}

	private void PrintStateList()
	{
		for (int i = 0; i < StateList.Count; i++) 
		{
			for (int j = 0; j < StateList [i].listaTransitions.Count; j++) 
			{
				Debug.Log (StateList [i].listaTransitions [j].parameter.Name + ", " + StateList[i].listaTransitions[j].triggered);
			//Debug.Log(StateList[i].StateName);
			}
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
