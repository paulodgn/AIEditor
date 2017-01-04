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


	/*public BoolParameter GetParameter(string name)
	{
		for (int i = 0; i < listaP.Count; i++) 
		{
			if (listaP [i].Name == name) {
				return listaP [i];
			} else
				return null;
		}
		return null;*/
		
}
