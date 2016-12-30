using UnityEngine;
using System.Collections;
using System;

//[System.Serializable]
public class StateAction
{
	public string Name;			//nome da acao
	public Action<string> stateAction;	//conjunto de acoes a serem executadas

	public StateAction(string name, Action<string> action)
	{
		this.Name = name;
		this.stateAction = action;
	}

	public void ExecuteAction()
	{
		//executa a string guardada com ocodigo
		stateAction.Invoke (stateAction.Method.ToString());
	}
}
