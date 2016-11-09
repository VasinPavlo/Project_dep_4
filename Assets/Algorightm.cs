using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorightm : MonoBehaviour {

	// Use this for initialization
	List<List<Vector3> > lists_of_speed;
	void Start () 
	{
		lists_of_speed = new List<List<Vector3>> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static Vector3 getMagicVec(Vector3 a,Vector3 b)
	{
		Vector3 res = new Vector3 ();
		res.x = a.y * b.z - a.z * b.y;
		res.y = a.x * b.z - a.z * b.x;
		res.z = a.x * b.y - a.y * b.x;
		res.Normalize();
		return res;
	}

	int FindVector_of_speed(List<Edge> lines,List<Vector3> points)
	{
		lists_of_speed.Add (new List<Vector3> ());

	}
}
