using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class StateMachineEditor : EditorWindow 
{
	//janela
	public static StateMachineEditor window;

	StateMachineClass stateMachine;

	//get objeto selecionado
	private GameObject obj;
	ParameterCreator parameterCreator;

	//flag para determinar se ja foi efetuado load da maquina de estados
	bool loaded=false;

	//lista de janelas
	List<StateWindowData> stateWindows = new List<StateWindowData>();

	//dados do menu de criaçao de parametros
	string txtParametro = "";
	int opcaoTipoParametro = 0;
	string[] tipoParametro = {"bool", "int", "float"};

	//para abrir a janela a partir do menu
	[MenuItem ("AIEditor/StateMachine")]

	static void OpenWindow()
	{
		//cria a janela
		window = (StateMachineEditor)EditorWindow.GetWindow(typeof(StateMachineEditor));
		//muda o titulo a janela
		//window.title = "State Machine";

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
			parameterCreator = Selection.activeGameObject.GetComponent<ParameterCreator> ();
			GUI.Label(new Rect(0, 0, position.width, 25), "Current selected object: " + obj.name);

		}

		#region State Machine Windows
		//mostra os campos do script statemachine associado ao objeto
		stateMachine = obj.GetComponent<StateMachineClass>();

		if(stateMachine != null)
		{

			//se a maquina de estados ja tem dados, cria uma janela para cada estado
			if (stateMachine.StateList.Count > 0 && !loaded) 
			{
				if (GUILayout.Button ("Load StateMachine") ) 
				{
					LoadData();
					for (int i = 0; i < stateMachine.StateList.Count; i++) 
					{
						stateWindows.Add (new StateWindowData (new Rect (10, 10, 200, 200), stateMachine.StateList[i].StateName, stateMachine.StateList[i].ActionID, stateMachine.actions.listaActions, parameterCreator));
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
					stateWindows.Add (new StateWindowData (new Rect (10, 10, 200, 200), " " + stateMachine.StateList.Count,0 , stateMachine.actions.listaActions, parameterCreator));
				}
			}

			//se existem janelas para desenhar, desenha
			BeginWindows ();
			EditorGUIUtility.LookLikeControls ();
			//desenhar janelas para os estados
			if (stateWindows.Count > 0) 
			{
				for (int i = 0; i < stateWindows.Count; i++) 
				{
					stateWindows [i].janela = GUI.Window (i, stateWindows [i].janela, CreateWindow, "Window " + i);

				}
			}

			//desenhar janela para a geraçao de parametros
			//parameterCreator.janela = GUI.Window(999, parameterCreator.janela,CreateParameterWindow, "Parameters");

			EndWindows ();

		}
		#endregion

		#region Parameter Creator
		//Ui para criaçao de parametros
		GUILayout.BeginArea(new Rect(window.position.width-200f, 20, 200, 200),"Parameters");

		GUILayout.BeginVertical ();

		txtParametro = GUILayout.TextField (txtParametro);
		opcaoTipoParametro = EditorGUILayout.Popup (opcaoTipoParametro, tipoParametro);

		//imprimir lista
		for (int i = 0; i < parameterCreator.listaP.Count; i++) 
		{
			GUILayout.Label (parameterCreator.listaP[i].Name);
		}

		GUILayout.Space (10);
		if (GUILayout.Button ("Create parameter")) 
		{
			//parameterCreator.listaP.Add (new BoolParameter ("teste", true));
			if (txtParametro != "") 
			{
				//verificar qual o tipo de parametro escolhido
				if (opcaoTipoParametro == 0) //bool
				{
					parameterCreator.listaP.Add(new BoolParameter(txtParametro, ParameterType.boolean, true));
				}
				if (opcaoTipoParametro == 1) //int 
				{
					parameterCreator.listaP.Add(new BoolParameter(txtParametro, ParameterType.integer, true));
				}
				if (opcaoTipoParametro == 2) //float
				{
					parameterCreator.listaP.Add(new BoolParameter(txtParametro, ParameterType.floatingPoint, true));
				}
			}
				

		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		//temos de actualizar os parametros

		#endregion

		#region Manu Principal
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
		#endregion
	}

	#region Create State Window
	void CreateWindow(int unusedWindow)
	{


		//desenha a janela respetiva
		stateWindows [unusedWindow].DrawWindow ();

		//atribui o nome do textfield ao nome do estado
		stateMachine.StateList [unusedWindow].StateName = stateWindows [unusedWindow].GetStateName ();

		//atribui ao estado o id da açao selecionada. Previne que ao fazer reload nao volta á ção default
		stateMachine.StateList[unusedWindow].ActionID = stateWindows[unusedWindow].StateActionOption;

		//atribui ao estado a acao selecionada no menu dropdown
		stateMachine.StateList[unusedWindow].currentStateAction = stateMachine.actions.listaActions [stateWindows[unusedWindow].GetStateActionOption()]/*.GetStateActionOption()].stateAction*/;
		GUI.DragWindow();
	}
	#endregion

	#region Save/Load
	void OnDestroy()
	{
		SaveData ();
	}



	void SaveData()
	{
		string data = "data para guardar";
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/StateMachineInfo.dat");
		bf.Serialize (file, stateMachine.StateList);
		file.Close ();

	}

	void LoadData()
	{
		if (File.Exists (Application.persistentDataPath + "/StateMachineInfo.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/StateMachineInfo.dat", FileMode.Open);
			List<StateClass> tempList = (List<StateClass>)bf.Deserialize (file);
			file.Close ();
			for (int i = 0; i < tempList.Count; i++) 
			{
				Debug.Log (tempList [i].StateName + " , " + tempList [i].ActionID + " , " + tempList [i].currentStateAction.Name);
			}
			stateMachine.StateList = tempList;
		}

	}
	#endregion

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

//OLD CREATE WINDOW, DIDNT WORK SO GREAT
/*void CreateWindow(int unusedWindow)
{
	//GUILayout.BeginHorizontal ();
	//GUILayout.Label ("State Name");
	//name = GUILayout.TextField (name, 25);
	//stateWindows[unusedWindow].name = GUILayout.TextField(stateWindows[unusedWindow].name, 25);

	stateWindows [unusedWindow].DrawWindow ();
	stateMachine.StateList [unusedWindow].StateName = stateWindows [unusedWindow].GetStateName ();
	//Debug.Log (unusedWindow + "  + " + stateWindows [unusedWindow].name);

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

	GUILayout.EndArea ();

	if (GUILayout.Button("Debug")) 
	{
		Debug.Log (unusedWindow +", " + indice[unusedWindow]);
	}
	//funciona
	//obj = (GameObject)EditorGUILayout.ObjectField (obj, typeof(StateClass));

	//GUILayout.EndHorizontal();
	GUI.DragWindow();
}*/