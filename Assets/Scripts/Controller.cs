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
	List<Line> vectors_line;


	void Start () 
	{
		points = new List<GameObject> ();
		lines = new List<Edge>  ();
		vectors = new List<Edge> ();
		vectors_line = new List<Line> ();
		//
		//addLines (options.list);
		options.list = new List<Vector3> ();
		for (float x = -2; x <= 2; x += 1f) 
		{
			options.list.Add (new Vector3 (x*10,x*x*10 , 0));
			//print (x + " " + x * x);
		}
		addLines (options.list);
		options.list.Clear();
		for (float x = -3; x >= -2; x -= 0.001f) 
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
		Remove_everything ();
	}
	
	// Update is called once per frame
	bool wait=false;
	float time=0;
	Test test;
	int sup_Index_of_Vectors_list=0;
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
		if (Input.GetButtonUp ("Clear vector field")) 
		{
			Clear_Vectors_list ();
		}
		if (wait) 
		{
			if (!Obj.algo.isWork) 
			{
				print ("End");
				addVectors(vec_points,Obj.algo.lists_of_speed);
				wait = false;
			}
		}
		else if(Input.GetButtonUp("Create vector field"))
		{
			vec_points = Obj.Cam_cont.getVector_of_point ();
			//new List<Vector3> ();
			//vec_points.Add (Obj.Cam_cont.getMousePosition ());
			Obj.algo.FindVector_of_speed (lines, vec_points);
			wait = true;
			print ("Start");
		}
	}

	void Clear_Vectors_list()
	{
		if (!options.isTime_for_Light_Render) {
			for (int i = 0; i < vectors.Count; i++) {
				vectors [i].gameObject.SetActive (false);
			}
		} else {
			for (int i = 0; i < vectors_line.Count; i++) {
				vectors_line [i].Clear ();
			}
		}
		sup_Index_of_Vectors_list = 0;
	}

	void Remove_everything()
	{
		for (int i = 0; i < vectors.Count; i++) 
		{
			MonoBehaviour.Destroy (vectors [i].gameObject);
			//vectors[i].gameObject.
		}
		vectors.Clear();
		sup_Index_of_Vectors_list = 0;

		for (int i = 0; i < lines.Count; i++) 
		{
			MonoBehaviour.Destroy (lines [i].gameObject);
		}
		lines.Clear ();

		for (int i = 0; i < points.Count; i++) 
		{
			MonoBehaviour.Destroy (points [i].gameObject);
		}

		for (int i = 0; i < vectors_line.Count; i++) 
		{
			MonoBehaviour.Destroy (vectors_line [i].gameObject);
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

	public void addVector(Vector3 start,Vector3 end)
	{
		if (!options.isTime_for_Light_Render) 
		{
			if (sup_Index_of_Vectors_list >= vectors.Count) {
				Edge vector = (Object.Instantiate (clones.vector, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Edge> ();
				vectors.Add (vector);
				vector.set (start, end);
				vector.transform.SetParent (parents.Vectors.transform);
				sup_Index_of_Vectors_list++;
				//return vector;
			} else {
				vectors [sup_Index_of_Vectors_list].set (start, end);
				vectors [sup_Index_of_Vectors_list].gameObject.SetActive (true);
				//return vectors [sup_Index_of_Vectors_list++];
			}
		} 
		else 
		{
			if (sup_Index_of_Vectors_list >= vectors.Count) {
				Line vector = (Object.Instantiate (clones.line, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Line> ();
				vectors_line.Add (vector);
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vector.addFunctionPoints (list);
				vector.transform.SetParent (parents.Vectors.transform);
				sup_Index_of_Vectors_list++;
				//return vector;
			} else {
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vectors_line[sup_Index_of_Vectors_list].gameObject.SetActive (true);
				vectors_line[sup_Index_of_Vectors_list].addFunctionPoints (list);
				//return vectors [sup_Index_of_Vectors_list++];
			}
		}

	}

	public void addVectors(List<Vector3> points,List<Vector3> list_of_speed)
	{
		//
		for (int i = 0; i < points.Count; i++) 
		{
			addVector (points [i], points [i] + list_of_speed [i]);
		}
		/*/
		while (vectors.Count < points.Count) {
			addVector(new Vector3(),new Vector3(1,0,0));
		}
		for (int i = 0; i < points.Count; i++) {
			vectors [i].set (points [i], points [i] + list_of_speed [i]);
		}
		/*/
	}
	public void Clear()
	{
		
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
		public bool isTime_for_Light_Render;
	}
}
