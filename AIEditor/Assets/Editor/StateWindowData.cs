using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StateWindowData {

	public Rect janela;
	public string name;

	public int StateActionOption;
	string[] optionsStateAction;

	public StateWindowData(Rect janela, string nome, List<StateAction> actions)
	{
		this.janela = janela;
		this.name = nome;

		optionsStateAction = new string[actions.Count];

		for (int i = 0; i < actions.Count; i++) 
		{
			optionsStateAction [i] = actions [i].Name;
		}
	}

	public void DrawWindow()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Name");
		name = GUILayout.TextField (name);
		GUILayout.EndHorizontal ();
		GUILayout.Space (20);
		GUILayout.Label ("State Action");
		StateActionOption = EditorGUILayout.Popup (StateActionOption, optionsStateAction);
	}

	public int GetStateActionOption()
	{
		return StateActionOption;
	}

	public string GetStateName()
	{
		return name;
	}
}
