using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Transition
{
	public BoolParameter parameter;			//qual é o parametro

	public ParameterType parameterType;		//qual é o tipo do parametro

	public StateClass targetState;				//alvo da transição

	public int intValue;						//valor de comparação caso parametro seja inteiro

	public float floatValue;					//valor de comparação caso parametro seja float

	public bool triggered;						//identifica o trigger da transiçao

	public Transition(BoolParameter parameter, StateClass targetState)
	{
		this.parameter = parameter;
		this.targetState = targetState;
	}

	public void CheckTransition()
	{
		if (parameter.boolValue == parameter.triggerValue) {
			//se o valor do parametro for igual ao valor que dispara a transicao, entao ativa transiçao.
			triggered = true;
		}
		else
		{
			triggered = false;
		}
	}
}
