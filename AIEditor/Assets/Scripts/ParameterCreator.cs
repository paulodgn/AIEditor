using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
//using UnityEditorInternal;


[System.Serializable]
public class ParameterCreator : MonoBehaviour
{
	public Rect janela;						//janela

	string[] opcoes = { "bool", "int" };
	int opcao = 0;
	string textName = " ";

	//lista de parametros
	public List<BoolParameter> listaP = new List<BoolParameter>();

	void Start()
	{
		for (int i = 0; i < listaP.Count; i++) {
			//Debug.Log (listaP [i].Name);
		}
	}

	public BoolParameter GetParameter(string name)
	{
		for (int i = 0; i < listaP.Count; i++) 
		{
			if (listaP [i].Name == name) {
				return listaP [i];
			} else
				return null;
		}
		return null;
	}

	/*
	public void DrawWindow()
	{
		
		GUILayout.BeginVertical ();
		GUILayout.Label ("Parameter");
		textName = GUILayout.TextField (textName);
		opcao = EditorGUILayout.Popup (opcao, opcoes);

		//imprimir lista
		//parametersRL.DoLayoutList ();
		for (int i = 0; i < listaP.Count; i++) 
		{
			GUILayout.Label (listaP [i].Name);
		}

		GUILayout.Space (10);
		if (GUILayout.Button ("Create parameter")) 
		{
			listaP.Add (new BoolParameter ("teste", true));
		}
		GUILayout.EndVertical ();
	}*/
}
