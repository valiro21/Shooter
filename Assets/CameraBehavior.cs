using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	GameObject cam;
	public float MinPosition = 0.4f, MaxPosition = 5f, MouseSensivityY = 3f, MouseSensivityX = 60f;
	void Awake () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		cam.transform.localPosition = new Vector3 (-MaxPosition, MinPosition, 0f);
		MouseSensivityY = (MaxPosition - MinPosition) / MouseSensivityY;
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse Y") > 0) {
			if ( cam.transform.localPosition.x < -MinPosition ) {
				cam.transform.localPosition += new Vector3 ( MouseSensivityY * Time.deltaTime, MouseSensivityY * Time.deltaTime, 0);
				cam.transform.localEulerAngles = new Vector3 ( 90 * cam.transform.localPosition.y / (MaxPosition - MinPosition), cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
			}
			if ( cam.transform.localPosition.x >= -MinPosition ) {
				cam.transform.localPosition = new Vector3 ( -MinPosition, MaxPosition, 0);
				cam.transform.localEulerAngles = new Vector3 ( 90f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
			}
		}

		if (Input.GetAxis ("Mouse Y") < 0) {
			if ( cam.transform.localPosition.x > -MaxPosition ) {
				cam.transform.localPosition -= new Vector3 ( MouseSensivityY * Time.deltaTime, MouseSensivityY * Time.deltaTime, 0);
				cam.transform.localEulerAngles = new Vector3 ( 90 * cam.transform.localPosition.y / (MaxPosition - MinPosition), cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
			}
			if ( cam.transform.localPosition.x <= -MaxPosition ) {
				cam.transform.localPosition = new Vector3 ( -MaxPosition, MinPosition, 0);
				cam.transform.localEulerAngles = new Vector3 ( 0f, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
			}
		}

		if (Input.GetAxis ("Mouse X") > 0)
			transform.eulerAngles += new Vector3 ( 0, MouseSensivityX * Time.deltaTime, 0 );
		if (Input.GetAxis ("Mouse X") < 0)
			transform.eulerAngles -= new Vector3 ( 0, MouseSensivityX * Time.deltaTime, 0 );
	}
}
