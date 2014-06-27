using UnityEngine;
using System.Collections;

public class AIController : Controller {
	public AIController() 
	{
		Health = 100;
		Armour = 100;
		MovementSpeed = 10;
		AttachedCharacter = null;
		Name = "BLANK";
		FriendlyName = "YOU_SHOULDNT_SEE_THIS";
	}
	
	public AIController(int _health, int _armour, float _movementSpeed,
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
			
		Debug.Log("This is an AI Controller");
			
	}
	
	public void Hit(int HP)
	{
		Health -= HP;
		if(Health <= 0)
			Dying();
	}
	

}
