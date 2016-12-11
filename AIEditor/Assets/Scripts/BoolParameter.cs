using UnityEngine;
using System.Collections;

[System.Serializable]
public class BoolParameter : IParameter 
{
	public string Name { get; set;}
	public bool Value;

	public BoolParameter(string name, bool value)
	{
		Name = name;
		Value = value;
	}
}
