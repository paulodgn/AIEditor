using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StateWindowData {

	public Rect janela;
	public string name;

	public int StateActionOption;
	string[] optionsStateAction;

	public List<IParameter> listaParametros; 	//lista de parametros
	ParameterCreator paramCreator;

	public StateWindowData(Rect janela, string nome, List<StateAction> actions)
	{
		this.janela = janela;							//area da janela a ser desenhada
		this.name = nome;								//nome da janela	
		optionsStateAction = new string[actions.Count];	//lista dos nomes da acoes disponiveis

		//inicar listas
		//listaParametros = new List<IParameter>();

		//guarda todos os nomes das acoes existentes para serem mostradas non popup
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

		GUILayout.BeginVertical ();
		GUILayout.Space (20);

		GUILayout.Label ("State Action");
		StateActionOption = EditorGUILayout.Popup (StateActionOption, optionsStateAction);

		GUILayout.Space (20);
		GUILayout.EndVertical ();
	
	}


	void Cenas()
	{
		Debug.Log ("novas cenas");
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
