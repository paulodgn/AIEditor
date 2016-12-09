using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class StateClass
{

	public string StateName;
	public int ID;

	//acao escolhida para o estado. Faz sentido? ou toda a acao? para que preciso do nome da acao?
	public StateAction currentStateAction;




}
