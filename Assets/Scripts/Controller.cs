using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	public OBJECTS Obj;
	public CLONE_OF_OBJECTS clones;
	public Parents parents;
	public Options options;

	List<GameObject> points;
	List<Edge> lines;
	List<Edge> vectors;

	// Use this for initialization
	void Start () 
	{
		points = new List<GameObject> ();
		lines = new List<List<Edge> > ();
		//
		addLines (options.list);
		options.list = new List<Vector3> ();
		for (float x = -5; x <= 5; x += 0.02f) 
		{
			options.list.Add (new Vector3 (x, x * x, 0));
			//print (x + " " + x * x);
		}
		addLines (options.list);
		//
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonUp(0))
		{
			Vector3 vec = Obj.Cam_cont.getMousePosition ();
			print (vec);
			addPoint(vec);
		}
	}

	public void addPoint(Vector3 vec)
	{
		GameObject point = (Object.Instantiate (clones.point, vec, Quaternion.Euler (0, 0, 0))as GameObject);
		points.Add (point);
		point.transform.SetParent (parents.Points.transform);
	}

	public void addLines(List<Vector3> list)
	{
		List<Edge> line=new List<Edge>();
		for (int i = 0; i < list.Count - 1; i++) 
		{
			Edge edge = addLine (list [i], list [i + 1]);
			lines.Add (edge);
			//addPoint (list [i]);
		}
		//addPoint (list [list.Count - 1]);
	}

	public Edge addLine(Vector3 start,Vector3 end)
	{
		Edge line = (Object.Instantiate (clones.edge, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Edge>();
		//lines.Add (line);
		line.set(start,end);
		line.transform.SetParent (parents.Lines.transform);
		return line;
	}

	public Edge addVector(Vector3 start,Vector3 end)
	{
		Edge vector = (Object.Instantiate (clones.vector, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Edge>();
		//lines.Add (line);
		vector.set(start,end);
		vector.transform.SetParent (parents.Lines.transform);
		return vector;
		
	}

	[System.Serializable]
	public struct OBJECTS
	{
		public Camera_Controller Cam_cont;
	}
	[System.Serializable]
	public struct CLONE_OF_OBJECTS
	{
		public GameObject line;
		public GameObject point;
		public GameObject edge;
		public GameObject vector;
	}
	[System.Serializable]
	public struct Parents
	{
		public GameObject Lines;
		public GameObject Points;
	}
	[System.Serializable]
	public struct Options
	{
		public List<Vector3> list;
	}
}
