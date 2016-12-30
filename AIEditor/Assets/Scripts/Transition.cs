using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Transition
{
	public BoolParameter parameter;			//qual é o parametro


	public ParameterType parameterType;		//qual é o tipo do parametro

	public StateClass targetState;				//alvo da transição

	public int intValue;						//valor de comparação caso parametro seja inteiro

	public float floatValue;					//valor de comparação caso parametro seja float

	public bool triggered;

	public Transition(BoolParameter parameter, StateClass targetState)
	{
		this.parameter = parameter;
		this.targetState = targetState;
	}
		
}
