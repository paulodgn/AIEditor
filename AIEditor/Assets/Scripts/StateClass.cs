using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class StateClass
{

	public string StateName;

	public int ID;

	public int ActionID;

	public string teste;//opcao da acao do estado escolhida
	//acao escolhida para o estado. Faz sentido? ou toda a acao? para que preciso do nome da acao?
	public StateAction currentStateAction;

	public List<Transition> listaTransitions;


}
