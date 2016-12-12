using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoolParameter 
{
	public string Name;
	public bool boolValue;
	public int intValue;
	public ParameterType parameterType;

	public BoolParameter(string name, ParameterType type, bool value)
	{
		parameterType = type;
		Name = name;
		boolValue = value;
	}
		
}
