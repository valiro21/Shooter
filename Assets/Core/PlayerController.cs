using UnityEngine;
using System.Collections;

public class PlayerController : Controller {

	public PlayerController() 
	{
		Health = 100;
		Armour = 100;
		MovementSpeed = 10;
		AttachedCharacter = null;
		Name = "BLANK";
		FriendlyName = "YOU_SHOULDNT_SEE_THIS";
	}
	
	public PlayerController(int _health, int _armour, float _movementSpeed,
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
		
	public override void OnInit()
	{
			
		Debug.Log("This is a PlayerController");	
			
	}
	
	public void Execute()
	{
		if(Health <= 0)
			Dying();
	}
	
}
