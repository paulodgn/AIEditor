using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;


[System.Serializable]
public class ParameterCreator
{
	public Rect janela;					//janela

	string[] opcoes = { "bool", "int" };
	int opcao = 0;
	string textName = " ";
	//lista para reordable list

	//SerializedObject parameters;

	//list de parametros
	ParametersList listaParametros;

	//List<IParameter> listaParametros;
	//ReorderableList parametersRL;
	List<BoolParameter> listaP;

	public ParameterCreator(Rect janela)
	{
		this.janela = janela;
		listaParametros = new ParametersList ();

		listaP = new List<BoolParameter> ();
		listaP.Add(new BoolParameter("speed", true));
		listaP.Add (new BoolParameter ("health", false));
		//parametersRL = new ReorderableList(listaParametros,typeof(IParameter),false,false,false,false);
	}

	public void DrawWindow()
	{
		

		/*parametersRL = new ReorderableList(listaParametros.Parametros,typeof(ParametersList),false,false,false,false);
		parametersRL.drawElementCallback = (Rect rect, int index, bool isActive,bool isFocused) =>
		{
			rect.y += 5;
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect, parametersRL.serializedProperty.GetArrayElementAtIndex(index));
		};*/
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
	}
}
/*
SerializedProperty CustomInspectorVar= serializedObject.FindProperty ("NameOfListInClass");
You might also need to add this in if you have a list of custom elements
	EditorGUI.BeginChangeCheck();
	EditorGUILayout.PropertyField(CustomInspectorVar, true);
if(EditorGUI.EndChangeCheck()) 
	serializedObject.ApplyModifiedProperties();*/