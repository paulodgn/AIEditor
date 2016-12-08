using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;



public class StateMachineEditor : EditorWindow 
{
	//janela
	public static StateMachineEditor window;

	StateMachineClass stateMachine;

	//get objeto selecionado
	private GameObject obj;

	//comportamenos disponiveis para os estados
	//public AvaiableStates states;

	//dados para adicionar ao estado
	string name = "";
	List<string> names = new List<string>();

	//lsita de estados. cópia da lista do statemechine
	ReorderableList stateList;

	//bool para verificar se foldout esta visivel
	bool showStates;

	//posicao inicial para cada estado
	int inicialPosition = 30;

	//numero de estados criados
	int numeroEstados=0;

	//flag para determinar se ja foi efetuado load da maquina de estados
	bool loaded=false;

	//indice da acao desolvida pelo popup. É usada para escolher a acao que o estado vai ter.
	int[] indice;
	int lastIndice;

	bool test;

	//janela
	Rect stateWindow = new Rect(10,10,200,200);

	//lista de janelas
	List<StateWindowData> stateWindows = new List<StateWindowData>();
	List<Rect> stateWindows2 = new List<Rect> ();

	//para abrir a janela a partir do menu
	[MenuItem ("AIEditor/StateMachine")]

	static void OpenWindow()
	{
		//cria a janela
		window = (StateMachineEditor)EditorWindow.GetWindow(typeof(StateMachineEditor));
		//muda o titulo a janela
		window.title = "State Machine";


	}


	void OnGUI()
	{
		//serve para debug. abre sempre a jenla sem ser necessario estar sempre a clicar no botao
		if(window == null)
			OpenWindow();

		//mostra os elementos do objeto selecionado
		if(Selection.activeGameObject != null)
		{
			obj = Selection.activeGameObject;
			GUI.Label(new Rect(0, 0, position.width, 25), "Current selected object: " + obj.name);

		}

		//mostra os campos do script statemachine associado ao objeto
		stateMachine = obj.GetComponent<StateMachineClass>();

		if(stateMachine != null)
		{

			//se a maquina de estados ja tem dados, cria uma janela para cada estado
			if (stateMachine.StateList.Count > 0 && !loaded) 
			{
				if (GUILayout.Button ("Load StateMachine") ) 
				{
					for (int i = 0; i < stateMachine.StateList.Count; i++) 
					{
						stateWindows.Add (new StateWindowData (new Rect (10, 10, 200, 200), stateMachine.StateList[i].StateName, stateMachine.actions.listaActions));
					}
					loaded = true;
				}
			}
			//senao colocamos um botao para criar estados.
			else 
			{
				//se for pressionado o botao criar estado
				if (GUILayout.Button ("New State") ) 
				{
					loaded = true;
					//criamos um novo estado
					stateMachine.CreateNewState (" ", stateMachine.StateList.Count);
					//criamos uma nova janela
					stateWindows.Add (new StateWindowData (new Rect (10, 10, 200, 200), " " + stateMachine.StateList.Count, stateMachine.actions.listaActions));
				}
			}

			//se existem janelas para desenhar, desenha
			BeginWindows ();
			EditorGUIUtility.LookLikeControls ();
			if (stateWindows.Count > 0) 
			{
				for (int i = 0; i < stateWindows.Count; i++) 
				{
					stateWindows [i].janela = GUI.Window (i, stateWindows [i].janela, CreateWindow, "Window " + i);

				}
			}
			EndWindows ();

		}


		//botao para remover o ultimo estado da lista
		if (GUI.Button (new Rect (position.width / 2 - 100, position.height - 30, 200, 25), "Delete Last State")) 
		{
			
			stateMachine.RemoveStateFromList ();
			stateWindows.RemoveAt (stateWindows.Count-1);
		}

		//botao para fazer load das acoes disponiveis
		if (GUI.Button (new Rect (position.width / 2 - 100, position.height - 60, 200, 25), "Load Actions")) 
		{
			stateMachine.actions.listaActions.Clear ();
			stateMachine.actions.CreateActionList ();
		}

		//botao para fazer load das acoes disponiveis


	}

	void CreateWindow(int unusedWindow)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("State Name");
		//name = GUILayout.TextField (name, 25);
		stateWindows[unusedWindow].name = GUILayout.TextField(stateWindows[unusedWindow].name, 25);

		stateWindows [unusedWindow].DrawWindow ();
		//Debug.Log (unusedWindow + "  + " + stateWindows [unusedWindow].name);
		/*
		//vai buscar as acoes disponiveis á lista de acoes para as mostrar no popup
		string[] options = new string[stateMachine.actions.listaActions.Count];
		for (int i = 0; i < stateMachine.actions.listaActions.Count; i++) 
		{
			options [i] = stateMachine.actions.listaActions [i].Name;
		}

		//POPUP para mostrar todas as acoes disponiveis
		GUILayout.BeginArea (new Rect (95, 60, 100, 50));

		indice = new int[stateWindows.Count];
		lastIndice = EditorGUILayout.Popup (lastIndice, options); //PROBLEMA. NA SEGUNDA FRAME O INDICE NAO MUDA, LOGO FICAM OS DOIS ESTADOS COM A MESMA ACCAO!
		indice[unusedWindow] = lastIndice;
		//a opcao escolhida é o indice da acao a ser executada pelo estado.
		//atribuimos a este estado a sua açao.

		//procurar qual o estado da respetiva janela
		//StateClass tempState = stateMachine.StateList.Find(x => x.ID == unusedWindow);
		//tempState.currentStateAction = stateMachine.actions.listaActions [indice].stateAction;

		stateMachine.StateList[unusedWindow].currentStateAction = stateMachine.actions.listaActions [lastIndice].stateAction;
		//Debug.Log (stateMachine.actions.listaActions [indice].Name);
		//Debug.Log(unusedWindow);

		GUILayout.EndArea ();*/

		if (GUILayout.Button("Debug")) 
		{
			Debug.Log (unusedWindow +", " + indice[unusedWindow]);
		}
		//funciona
		//obj = (GameObject)EditorGUILayout.ObjectField (obj, typeof(StateClass));

		GUILayout.EndHorizontal();
		GUI.DragWindow();
	}

	void CreateNewState()
	{
		
	}

	void Update()
	{
			Repaint();
	}
}
//----------------------------------------------------------------------------------------------------------
//CODIGO OBSOLETO
/*inicialPosition = 30;
		for (int i = 0; i< numeroEstados; i++) 
		{
			//criar formulario para preencher dados do estado
			name = EditorGUI.TextField (new Rect (5, inicialPosition, position.width-10, 25),"State Name" , "new state");
			inicialPosition += 30;
		}*/




//criar um botao para criar um novo state 
/*if(GUI.Button(new Rect(position.width / 2 - 100, position.height - 60, 200, 25), "Create New State"))
		{
			numeroEstados++;
		}*/

//save button
/*if(GUI.Button(new Rect(position.width / 2 - 100, position.height - 60, 200, 25), "Save State"))
		{
			stateMachine.StateList.Clear ();
			for (int i = 0; i < stateWindows.Count; i++) 
			{
				stateMachine.SaveNewStateData (stateWindows[i].name, 999);
			}

		}*/
//------------------------------------------------------------------------------------------------------------
//adicionar caixas de estado quando se clica no botao.
/*BeginWindows ();

			EditorGUIUtility.LookLikeControls();

			if (GUILayout.Button("Create State")) 
			{
				//stateWindows.Add(new Rect(10, 10, 200, 200));
				stateWindows.Add(new StateWindowData( new Rect(10,10,200,200), " "));
			}

			if (stateWindows.Count > 0) 
			{
				for (int i = 0; i < stateWindows.Count; i++) 
				{
					stateWindows [i].janela = GUI.Window (i, stateWindows [i].janela, CreateWindow, "Window " + i);

				}
			}


			EndWindows ();*/