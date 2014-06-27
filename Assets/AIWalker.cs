using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIWalker : MonoBehaviour {
	public float MoveSpeed, RotationSpeed;
	List<Vector3> ShortestPathToPlayer;
	public bool FollowPlayer;


	bool MoveTowards ( Vector3 destination ) {
		Vector3 direction = destination - transform.position;
		if (Vector3.Angle (transform.forward, direction) < 0.001f) {
			Vector3 look = Vector3.RotateTowards (transform.forward, direction, RotationSpeed * Time.deltaTime, 0f);
			transform.LookAt (look + transform.position);
		} else {
			Vector3 position = Vector3.MoveTowards ( transform.position, destination, MoveSpeed * Time.deltaTime );
			transform.position = position;
		}
		if (transform.position == destination)
			return true;
		return false;
	}

	// Update is called once per frame
	void Update () {
		if ( FollowPlayer == true ) {
			ShortestPathToPlayer = WayPoint.ShortestPathBetween ( transform.position,
															GameObject.FindGameObjectWithTag ("Player").transform.position);
			if ( ShortestPathToPlayer.Count > 0 )
				MoveTowards ( ShortestPathToPlayer[0] );
		}
	}
}
