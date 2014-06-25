using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPSCamera : MonoBehaviour {
	GameObject cam;
	
	private float rotationX = 0;
	private float rotationY = 0;

	public float sensitivityX = 2;
	public float sensitivityY = 2;

	public float defaultSensX = 2;
	public float defaultSensY = 2;

	private Quaternion xQuaternion;
	private Quaternion yQuaternion;
	private Quaternion originalRotation, originalPlayerRotation;

	public float minimumY = -80;
	public float maximumY = 80;

	public int frameCounterX = 1005;
	public int frameCounterY = 1005;

	public int delayX = 2;
	public int delayY = 2;

	private List<float> rotArrayX = new List<float>();
	private List<float> rotArrayY = new List<float>();
	
	// Update is called once per frame
	void Awake () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		originalRotation = cam.transform.rotation;
		originalPlayerRotation = transform.rotation;
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	void LateUpdate()
	{
		float rotAverageX = 0, x = Input.GetAxis("Mouse X");

		float ok = 1f;
		if ( rotArrayX.Count > delayX && x == 0 ) {
			ok = 0f;
			for ( var i = rotArrayX.Count - delayX; i < rotArrayX.Count; i++ )
				if ( rotArrayX[i] != 0 ) {
					ok = 1f;
					break;
				}
		}

		rotationX += x * sensitivityX * defaultSensX;
		rotArrayX.Add (rotationX);

		if (rotArrayX.Count >= frameCounterX)
			rotArrayX.RemoveAt(0);

		for (var i_counterX = 0; i_counterX < rotArrayX.Count; i_counterX++)
			rotAverageX += rotArrayX[i_counterX];
		rotAverageX /= rotArrayX.Count;
		rotAverageX *= ok;

		float rotAverageY = 0, y = Input.GetAxis("Mouse Y");

		ok = 1f;
		if ( rotArrayY.Count > delayY && y == 0 ) {
			ok = 0f;
			for ( var i = rotArrayY.Count - delayY; i < rotArrayY.Count; i++ )
			if ( rotArrayY[i] != 0 ) {
				ok = 1f;
				break;
			}
		}

		rotationY += y * sensitivityY * defaultSensY;  
		rotArrayY.Add ( rotationY );

		if (rotArrayY.Count >= frameCounterY)
			rotArrayY.RemoveAt(0);

		for (var i_counterY = 0; i_counterY < rotArrayY.Count; i_counterY++)
			rotAverageY += rotArrayY[i_counterY];
		rotAverageY /= rotArrayY.Count;
		rotAverageY *= ok;
		    
		if ((rotAverageY >= -360) && (rotAverageY <= 360))
			rotAverageY = Mathf.Clamp (rotAverageY, minimumY, maximumY);
		else if (rotAverageY < -360)
			rotAverageY = Mathf.Clamp (rotAverageY+360, minimumY, maximumY);
		else
		    rotAverageY = Mathf.Clamp (rotAverageY-360, minimumY, maximumY);

		xQuaternion = Quaternion.AngleAxis (rotAverageX, transform.up);
		yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);

		transform.rotation = originalPlayerRotation * xQuaternion;
		cam.transform.localRotation = originalRotation * yQuaternion;
	}
}
