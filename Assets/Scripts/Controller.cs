using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Test:MonoBehaviour
{
	public Thread thread;
	public Test()
	{
		thread = new Thread (test);
		thread.Start ();
	}
	public bool isWork=true;
	int i=0;
	public void test()
	{
		while (isWork) 
		{
			print ("Hello World:"+i);
			i++;
			Thread.Sleep (1000);
		}
	}
	void OnApplicationQuit()
	{
		print ("Hello World BBBBBBBBBBBBBBBBBBBBBB");
		isWork = false;
	}
	public void start()
	{
		thread.Start ();
	}
}
public class Controller : MonoBehaviour {

	public OBJECTS Obj;
	public CLONE_OF_OBJECTS clones;
	public Parents parents;
	public Options options;

	List<GameObject> points;
	List<Vector3> vec_points;
	List<Edge> lines;
	List<Edge> vectors;


	void Start () 
	{
		points = new List<GameObject> ();
		lines = new List<Edge>  ();
		vectors = new List<Edge> ();
		//
		//addLines (options.list);
		options.list = new List<Vector3> ();
		for (float x = 0; x <= 2; x += 1f) 
		{
			options.list.Add (new Vector3 (x*10,x*x*10 , 0));
			//print (x + " " + x * x);
		}
		addLines (options.list);
		options.list.Clear();
		for (float x = 0; x >= -2; x -= 1f) 
		{
			options.list.Add (new Vector3 (x*10,x*x*10 , 0));
			//print (x + " " + x * x);
		}
		addLines (options.list);
		options.list.Clear();
		//
		//test = new Test ();
	}

	void OnApplicationQuit()
	{
		print ("Hello World AAAAAAAAAAAAAAAAAAAAAA I Can FLYYYYYYYYYYYYYYYYYYY...BOOMMMMMMM");
		//test.isWork = false;
		Obj.algo.Des_Time();
	}
	
	// Update is called once per frame
	bool wait=false;
	float time=0;
	Test test;
	void Update () 
	{
		/*/
		time += Time.deltaTime;
		if (time >= 3) 
		{
			time = 0;
			if (test.isWork) {
				test.isWork = false;
			} 
			else 
			{
				//test.thread.Suspend ();
				test.isWork = true;
				test.start ();
			}
		}
		/*/
		if (wait) 
		{
			if (!Obj.algo.isWork) 
			{
				print ("End");
				addVectors(vec_points,Obj.algo.lists_of_speed);
				wait = false;
			}
			return;
		}
		if(Input.GetMouseButtonUp(0))
		{
			vec_points = Obj.Cam_cont.getVector_of_point ();
			//new List<Vector3> ();
			//vec_points.Add (Obj.Cam_cont.getMousePosition ());
			Obj.algo.FindVector_of_speed (lines, vec_points);
			wait = true;
			print ("Start");
		}
	}

	public void addPoint(Vector3 vec)
	{
		GameObject point = (Object.Instantiate (clones.point, vec, Quaternion.Euler (0, 0, 0))as GameObject);
		points.Add (point);
		point.transform.SetParent (parents.Points.transform);
	}

	public void addPoints(List<Vector3> list)
	{
		while (list.Count > points.Count) 
		{
			addPoint (new Vector3 ());
		}
		for (int i = 0; i < list.Count; i++) 
		{
			points [i].transform.position = list [i];
		}
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
		vectors.Add (vector);
		vector.set(start,end);
		vector.transform.SetParent (parents.Vectors.transform);
		return vector;
		
	}

	public void addVectors(List<Vector3> points,List<Vector3> list_of_speed)
	{
		while (vectors.Count < points.Count) {
			addVector(new Vector3(),new Vector3(1,0,0));
		}
		for (int i = 0; i < points.Count; i++) {
			vectors [i].set (points [i], points [i] + list_of_speed [i]);
		}
	}

	[System.Serializable]
	public struct OBJECTS
	{
		public Camera_Controller Cam_cont;
		public Algorightm algo;
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
		public GameObject Vectors;
	}
	[System.Serializable]
	public struct Options
	{
		public List<Vector3> list;
	}
}
