using UnityEngine;
using System.Collections;

public abstract class Controller {
	protected int Health;
	protected int Armour;
	protected float MovementSpeed;
	protected GameObject AttachedCharacter;
	public string Name;
	public string FriendlyName;
	
	public Controller() 
	{	
		Health = 100;
		Armour = 100;
		MovementSpeed = 10;
		AttachedCharacter = null;
		Name = "BLANK";
		FriendlyName = "YOU_SHOULDNT_SEE_THIS";		
	}
	
	public Controller(int _health, int _armour, float _movementSpeed,
							GameObject _attachedCharacter, string _name, string _friendlyName)
		{
			Health = _health;
			Armour = _armour;
			MovementSpeed = _movementSpeed;
			if(_attachedCharacter == null)
				AttachedCharacter = new GameObject();
			else
				AttachedCharacter = _attachedCharacter;
			
			Name = _name;
			FriendlyName = _friendlyName;			
		}
		
	public virtual void OnInit()
		{
			
			
		}
		
	public void Execute()
		{
			if(Health <= 0)
					Dying();
		}


	public void SetHealth(int _health)
		{
			if(_health >= 0)
				Health = _health;
		}
		
	public void TakeDamage(int dmg)
		{
			Health -= dmg;	
		}
		
	public void Dying()	{}

	public void TempSetup(string _friendlyName, int _health)
		{
			FriendlyName = _friendlyName;
			Health = _health;
		}
		
	public void AttachCharacter(GameObject character)
	{
		GameObject aux = GameObject.Find(character.name);
		if(aux.transform.Find("Camera"))
		{
			AttachedCharacter.transform.Find("Camera").GetComponent<Camera>().enabled = false;
			aux.transform.Find("Camera").GetComponent<Camera>().enabled = true;
			AttachedCharacter = aux;
		}
	}
	
	public void AttachCharacter(string charName, Vector3 location, Quaternion rotation)
	{
		/*AttachedCharacter = (GameObject)Object.Instantiate(Resources.Load<GameObject>("Prefabs/"+charName),location,rotation);
		AttachedCharacter.transform.Find("TextName").GetComponent<TextMesh>().text = FriendlyName;*/
	}
		
		
		
		
		
}