using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
private	IniFile ini; 
private string test;
private Controller[] ControllerArray;
private GameObject[] Spawners;
		 
	// Use this for initialization
	void Start () {
		
		ini = new IniFile();
		//Valid ONLY for Microsoft Windows platform!
		ini.Load(Application.dataPath+"//Config//DefaultConfig.ini");
		
		Spawners = GameObject.FindGameObjectsWithTag("Spawner");

		if(Spawners.Length == 0)
			Debug.LogError("No Spawners in Scene! Please place at least one Player Spawner!");
		
		ControllerArray = new Controller[Spawners.Length];
		bool spawnOnce = false;
		for(int i = 0; i < Spawners.Length; i++) {
			if(Spawners[i].GetComponent<Spawner>().isPlayerSpawn && spawnOnce == false) {
				ControllerArray[i]  = new PlayerController();
				ControllerArray[i].TempSetup(ini.GetKeyValue("DefPlayer","Name"),
														  System.Convert.ToInt32(ini.GetKeyValue("DefPlayer","Health"))
														 );
				ControllerArray[i].AttachCharacter("character",Spawners[i].transform.position,Spawners[i].transform.rotation);
				spawnOnce = true;
			}
			else
				ControllerArray[i] = new AIController();
		}		
		
		for(int i = 0; i < Spawners.Length; i++)
			ControllerArray[i].OnInit();

		//test = ini.GetKeyValue("TEST","testA");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Variabila este: "+test);
		for(int i = 0; i < Spawners.Length; i++)
			ControllerArray[i].Execute();
	}

	void Awake () {
		WayPoint.Initialise ();
		WayPoint.Reload ();
	}
	
	
	PlayerController GetAPlayerController() {
		for(int i = 0; i < Spawners.Length; i++)
			if(ControllerArray[i].FriendlyName != "YOU_SHOULDNT_SEE_THIS")
				return (PlayerController)ControllerArray[i];
		return new PlayerController(); 
	}
	
	
	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20),GetAPlayerController().FriendlyName);
	}
}
