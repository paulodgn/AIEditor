using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StateWindowData {

	public Rect janela;							//janela do estado
	public string name;							//nome da janela

	public int StateActionOption;				//opcao selecionada no popup
	string[] optionsStateAction;				//opcoes que aparecem no popup

	public int currentActionID;

	public List<IParameter> listaParametros; 	//lista de parametros
	ParameterCreator paramCreator;				

	private int numeroTransicoes;				//numero de transicoes adicionas
												//serve para desenhar as linhas necessarias para mostrar os dados das transicoes

	public int TransitionPopupOption;			//indice da escolha do popup da transiçao. Aqui escohe se o parametro dentro dos criados previamente
	ParameterCreator parameterCreator;			//criador de paramtros. Presicamos para ter acesso á lista

	string[] OpcoesParametro;					//guarda o nome dos parametros criados. Usado no popup de escolha de parametro

	int stateID;								//id do estado da janela

	List<Transition> tempTransition;			//lista de transiçoes do estado

	int opcaoValorParametroBool=0;				//valor do paramtero de for bool

	GameObject obj;

	public StateWindowData(Rect janela, string nome, int stateID, int actionID, List<StateAction> actions, ParameterCreator paramCreator)
	{
		this.janela = janela;							//area da janela a ser desenhada
		this.name = nome;								//nome da janela	
		this.stateID = stateID;
		optionsStateAction = new string[actions.Count];	//lista dos nomes da acoes disponiveis
		parameterCreator = paramCreator;
		OpcoesParametro = new string[parameterCreator.listaP.Count];
		tempTransition = new List<Transition> ();

		//guarda todos os nomes das acoes existentes para serem mostradas non popup
		for (int i = 0; i < actions.Count; i++) 
		{
			optionsStateAction [i] = actions [i].Name;
		}
		//guarda o nome de todos os parametros disponiveis
		for (int j=0; j < parameterCreator.listaP.Count; j++) 
		{
			OpcoesParametro [j] = parameterCreator.listaP [j].Name;
		}

		//atribuir opcao da acao do estado.
		StateActionOption = actionID;
	
	}

	public void DrawWindow()
	{
		//Titulo
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Name");
		name = GUILayout.TextField (name);
		GUILayout.EndHorizontal ();

		//Popup para escolha de ação de estado
		GUILayout.BeginVertical ();
		GUILayout.Space (20);
		GUILayout.Label ("State Action");
		StateActionOption = EditorGUILayout.Popup (StateActionOption, optionsStateAction);
		currentActionID = StateActionOption;
		GUILayout.Space (20);

		//Transições
		//Botao de adicionar transiçao
		GUILayout.Label("Transitions");

		//mostrar todos as transiçoes do estado

		//tempTransition = GetStateTransitions();
		if (Selection.activeGameObject != null) 
		{
			obj = Selection.activeGameObject;

		}

		for (int i = 0; i < obj.GetComponent<StateMachineClass>().StateList[stateID].listaTransitions.Count; i++) 
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label (obj.GetComponent<StateMachineClass>().StateList[stateID].listaTransitions[i].parameter.Name);
			//GUILayout.Label (tempTransition [i].parameter.boolValue.ToString());
			//opcaoValorParametroBool = EditorGUILayout.Popup(opcaoValorParametroBool,"");
			GUILayout.EndHorizontal ();
		}
			
		GUILayout.EndVertical ();

	}

	void TransitionWindow(int id)
	{
		GUILayout.BeginHorizontal ();

		TransitionPopupOption = EditorGUILayout.Popup (TransitionPopupOption, OpcoesParametro);

		GUILayout.EndHorizontal ();
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

	List<Transition> GetStateTransitions()
	{
		if (Selection.activeGameObject != null) 
		{
			GameObject obj = Selection.activeGameObject;
			StateMachineClass tempMachine = obj.GetComponent<StateMachineClass> ();
			List<StateClass> tempStates = tempMachine.StateList;
			for (int i = 0; i < tempStates.Count; i++) 
			{
				if (tempStates [i].ID == this.stateID) 
				{
					return tempStates [i].listaTransitions;
				}
			}
		}
		return null;
	}

}
