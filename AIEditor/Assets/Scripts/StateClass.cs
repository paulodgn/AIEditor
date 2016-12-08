using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class StateClass
{

	public string StateName;
	public int ID;
	//acao escolhida para o estado
	public Action<string> currentStateAction;




}
