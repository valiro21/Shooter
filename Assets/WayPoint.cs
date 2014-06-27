using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPoint : MonoBehaviour {
	static GameObject[] rp;
	static WayPoint[] wp;
	static float ChRadius = 0.5f;
	public static int INF = 99999999;

	List<int> Neighbour;
	float[] dst;
	GameObject RealPoint;
	public int index;
	public Vector3 position;

	public WayPoint (int _index) {
		RealPoint = rp[_index];
		dst = new float[rp.Length];
		position = rp[_index].transform.position;
		index = _index;
		Neighbour = new List<int> ();
	}

	public static void Initialise () {
		rp = GameObject.FindGameObjectsWithTag ("WayPoint");
		wp = new WayPoint[rp.Length];
		for (int i = 0; i < rp.Length; i++) {
			wp[i] = new WayPoint (i);
		}
	}

	public static void Reload () {
		ReloadPaths ();
		ReloadShortestPath ();
	}

	static void ReloadPaths () {
		for ( int i = 0; i < wp.Length; i++ )
			wp[i].ReloadNeighbours();
	}

	void ReloadNeighbours () {
		Vector3 x = transform.position, y, k;

		for(int i = 0; i < wp.Length; i++ ) {
			y = wp[i].RealPoint.transform.position;

			k = Vector3.Cross ( y - x, Vector3.up );
			k /= k.magnitude; k *= ChRadius;

			dst[i] = INF;
			if ( gameObject != wp[i] && !Physics.Raycast ( x, y, Vector3.Distance (x, y) ) && !Physics.Raycast ( x + k, y + k, Vector3.Distance (x, y) ) &&
			    !Physics.Raycast ( x - k, y - k, Vector3.Distance (x, y) ) ) {
				dst[i] = Vector3.Distance (x, y);
				Neighbour.Add (i);
			}

			if ( gameObject == wp[i] )
				dst[i] = 0;
		}
	}

	static void ReloadShortestPath () {
		for (int k = 0; k < wp.Length; k++)
			for (int i = 0; i < wp.Length; i++)
				for (int j = 0; j < wp.Length; j++)
					if ( wp[i].dst[k] + wp[k].dst[j] < wp[i].dst[j] )
						wp[i].dst[j] = wp[i].dst[k] + wp[k].dst[j];
	}
	
	public float LengthTo ( int i ) {
		return dst[i];
	}
	
	List<int> ShortestWPPathTo (int i) {
		if (gameObject == wp [i])
			return new List<int>();

		List<int> Path;
		float MinDist = INF;
		int MinIndex = 0;
		for (int j = 0; j < Neighbour.Count; j++) {
			float dist = wp[Neighbour[j]].LengthTo (i);
			if ( dist < MinDist ) {
				MinDist = dist;
				MinIndex = j;
			}
		}

		Path = wp [MinIndex].ShortestWPPathTo (i);
		Path.Insert (0, MinIndex);

		return Path;
	}

	public static List<Vector3> ShortestPathBetween ( Vector3 origin, Vector3 destination ) {
		List<int> wpOrigin = AccessibleWayPointsFrom (origin);
		List<int> wpDest = AccessibleWayPointsFrom (destination);
		float dist = 0f, MinDist = INF;
		int MinI = -1, MinJ = -1;

		for (int i = 0; i < wpOrigin.Count; i++ )
			for ( int j = 0; j < wpDest.Count; j++ ) {
				dist = Vector3.Distance ( origin, wp[i].position );
				dist = Vector3.Distance ( wp[j].position, destination );
				dist += wp[i].LengthTo(j);

				if ( dist < MinDist ) {
					MinDist = dist;
					MinI = i; MinJ = j;
				}
			}

		List<Vector3> Path = new List<Vector3>();
		if ( origin != wp[MinI].position )
			Path.Add (wp [MinI].position);

		Path.AddRange (wp [MinI].ShortestPathTo (MinJ));

		if ( wp[MinJ].position != destination )
			Path.Add (destination);
		
		return Path;
	}

	public List<Vector3> ShortestPathTo (int i) {
		List<int> Path = ShortestWPPathTo (i);
		List<Vector3> VPath = new List<Vector3> ();
		foreach ( int j in Path )
			VPath.Add ( rp[j].transform.position );

		return VPath;
	}

	public static WayPoint ClosestWayPointTo ( Vector3 origin ) {
		Vector3 x = origin, y, k;
		float MinDist = INF;
		int MinIndex = -1;
		for (int i = 0; i < rp.Length; i++) {
			y = rp[i].transform.position;
			
			k = Vector3.Cross ( y - x, Vector3.up );
			k /= k.magnitude; k *= ChRadius;

			if ( !Physics.Raycast ( x, y, Vector3.Distance (x, y) ) && !Physics.Raycast ( x + k, y + k, Vector3.Distance (x, y) ) &&
			    !Physics.Raycast ( x - k, y - k, Vector3.Distance (x, y) ) && Vector3.Distance (x, y) < MinDist ) {
				MinDist = Vector3.Distance (x, y);
				MinIndex = i;
			}
		}
		return wp[MinIndex];
	}

	public static List<int> AccessibleWayPointsFrom ( Vector3 origin ) {
		Vector3 x = origin, y, k;
		List<int> WP = new List<int> ();
		for (int i = 0; i < rp.Length; i++) {
			y = rp[i].transform.position;
			
			k = Vector3.Cross ( y - x, Vector3.up );
			k /= k.magnitude; k *= ChRadius;

			Debug.DrawLine (x, y);
			Debug.DrawLine (x+k, y+k);
			Debug.DrawLine (x-k, y-k);
			if ( !Physics.Raycast ( x, y, Vector3.Distance (x, y) ) && !Physics.Raycast ( x + k, y + k, Vector3.Distance (x, y) ) &&
			    !Physics.Raycast ( x - k, y - k, Vector3.Distance (x, y) ) ) {
				WP.Add ( i );
			}
		}
		return WP;
	}
}
