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
	List<Vector3> vec_of_lines;
	List<Line> vec_of_Lines;
	List<Edge> lines;
	List<Edge> vectors;
	List<Line> vectors_line;
	List<Line> vectors_line_1;


	void Start () 
	{
		
		points = new List<GameObject> ();
		lines = new List<Edge>  ();
		vectors = new List<Edge> ();
		vectors_line = new List<Line> ();
		vec_of_lines = new List<Vector3> ();
		vec_of_Lines = new List<Line> ();
        vectors_line_1 = new List<Line> ();
        addMöbius_strip(20);
        /*/
        //addSquare(new Vector3(-30, -30), new Vector3(-30, 30), new Vector3(30, 30), new Vector3(30, -30));
        addEllipse(new Vector3(),10,10);
        vec_points = new List<Vector3>();
        for (float i = 10; i >= 10; i -= 0.5f)
        {
            Move(vec_points,getEllipse(new Vector3(0,0,-2), i, i));
        }
        //vec_points = getEllipse(new Vector3(), i, i);
        StartSearchVector_of_speed(vec_of_lines, vec_points);
        //addTriangle(new Vector3(-30, -30), new Vector3(-30, 30), new Vector3(30, 30));
        //addSquare(new Vector3(-30, -30,-10), new Vector3(-30, 30,-15), new Vector3(30, 30, 5), new Vector3(30, -30, 20));
        //StrangeSquare
       
		//
		//addLines (options.list);
		options.list = new List<Vector3> ();
		for (float x = -5; x <= 5; x += 0.1f) 
		{
            options.list.Add (new Vector3 (x,x*x, 0));
			//print (x + " " + x * x);
		}
		addLines (options.list);
		options.list.Clear();
		addLines (options.list);
		options.list.Clear();
		/*/
		//test = new Test ();
	}

	void OnApplicationQuit()
	{
		print ("Hello World AAAAAAAAAAAAAAAAAAAAAA I Can FLYYYYYYYYYYYYYYYYYYY...BOOMMMMMMM");
		//test.isWork = false;
		Obj.algo.Des_Time();
		Remove_everything ();
	}
       
    void addMöbius_strip(float r)
    {
        for (float v = -0.2f; v <= 0.2f; v += 0.05f)
        {
            _addMöbius_strip(v,r);
        }

    }

    void _addMöbius_strip(float v,float r)
    {
        List<Vector3> list=new List<Vector3>();
        Vector3 vec;
        for(float u=0;u<2*Mathf.PI;u+=0.1f)
        {
            vec = new Vector3((1+v/2*Mathf.Cos(u/2))*Mathf.Cos(u), (1+v/2*Mathf.Cos(u/2))*Mathf.Sin(u), v/2*Mathf.Sin(u/2));
            list.Add(vec*r);
        }
        vec = new Vector3((1+v/2*Mathf.Cos(2*Mathf.PI/2))*Mathf.Cos(2*Mathf.PI), (1+v/2*Mathf.Cos(2*Mathf.PI/2))*Mathf.Sin(2*Mathf.PI), v/2*Mathf.Sin(2*Mathf.PI/2));
        //
        list.Add(vec*r);
        addLines(list);
    }

    void Move(List<Vector3> a,List<Vector3> b)
    {
        for (int i = 0; i < b.Count; i++)
        {
            a.Add(b[i]);
        }
    }

    void addSquare(Vector3 a,Vector3 b,Vector3 c, Vector3 d)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(d);
        list.Add(a);
        addLines(list);
    }

    List<Vector3> getSquare(Vector3 a,Vector3 b,Vector3 c, Vector3 d)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(d);
        //list.Add(a);
        return list;
    }

    void addTriangle(Vector3 a,Vector3 b,Vector3 c)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        //list.Add(a);
        addLines(list);
    }

    List<Vector3> getTriangle(Vector3 a,Vector3 b,Vector3 c)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(a);
        return list;
    }

    void addEllipse(Vector3 pos, float a, float b)
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 vec;
        for(float x=0;x<2*Mathf.PI;x+=0.1f)
        {
            vec = new Vector3(a * Mathf.Cos(x) + pos.x, b * Mathf.Sin(x) + pos.y, pos.z);
            list.Add(vec);
        }
        vec = new Vector3(a * Mathf.Cos(2*Mathf.PI) + pos.x, b * Mathf.Sin(2*Mathf.PI) + pos.y, pos.z);
        list.Add(vec);
        addLines(list);
    }

    List<Vector3> getEllipse(Vector3 pos, float a, float b)
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 vec;
        for(float x=0;x<2*Mathf.PI;x+=0.1f)
        {
            vec = new Vector3(a * Mathf.Cos(x) + pos.x, b * Mathf.Sin(x) + pos.y, pos.z);
            list.Add(vec);
        }
        vec = new Vector3(a * Mathf.Cos(2*Mathf.PI) + pos.x, b * Mathf.Sin(2*Mathf.PI) + pos.y, pos.z);
        //list.Add(vec);
        return list;
    }

	
	// Update is called once per frame
	bool wait=false;
	float time=0;
	Test test;
	int sup_Index_of_Vectors_list_1=0;
	int sup_Index_of_Vectors_list_2=0;
	int sup_Index_of_Vectors_list_3=0;
	int sup_Index_of_Lines=0;
    Vector3 current_normal;
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
        if (Input.GetButtonUp("Projection"))
        {
            options.isProjectionTime = !options.isProjectionTime;
        }
		if (Input.GetButtonUp ("Clear vector field")) 
		{
			Clear_Vectors_list ();
		}
        if (Input.GetButtonUp("File_Input"))
        {
            Obj.file_cont.setActive();
        }
		if (Input.GetButtonUp ("Line_1")) 
		{
			if (options.isTime_for_Light_Render_Vector != 1) 
			{
				print ("Line_1");
				Clear_Vectors_list ();
				options.isTime_for_Light_Render_Vector = 1;
			}
		}
		if (Input.GetButtonUp ("Line_2")) 
		{
			if (options.isTime_for_Light_Render_Vector != 2) 
			{
				print ("Line_2");
				Clear_Vectors_list ();
				options.isTime_for_Light_Render_Vector = 2;
			}
		}
		if (Input.GetButtonUp ("Line_3")) 
		{
			if (options.isTime_for_Light_Render_Vector != 3) 
			{
				print ("Line_3");
				Clear_Vectors_list ();
				options.isTime_for_Light_Render_Vector = 3;
			}
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
            current_normal = Obj.Cam_cont.normal();
			//new List<Vector3> ();
			//vec_points.Add (Obj.Cam_cont.getMousePosition ());
			if (options.isTime_for_Light_Render_Grafick) 
			{
				Obj.algo.FindVector_of_speed (vec_of_lines, vec_points);
			} 
			else 
			{
				Obj.algo.FindVector_of_speed (lines, vec_points);
			}
			wait = true;
			print ("Start");
		}
	}

    void StartSearchVector_of_speed(List<Vector3> lines,List<Vector3> points)
    {
        Obj.algo.FindVector_of_speed(lines, points);
        wait = true;
    }

	void Clear_Vectors_list()
	{
		switch (options.isTime_for_Light_Render_Vector) 
		{
		case 1:
			for (int i = 0; i < vectors.Count; i++) 
			{
				vectors [i].gameObject.SetActive (false);
			}
			sup_Index_of_Vectors_list_1 = 0;
			break;
		case 2:
			for (int i = 0; i < vectors_line.Count; i++) 
			{
				vectors_line [i].Clear ();
				vectors_line [i].gameObject.SetActive (false);
			}
			sup_Index_of_Vectors_list_2 = 0;
			break;
		case 3:
			for (int i = 0; i < vectors_line_1.Count; i++) 
			{
				vectors_line_1 [i].Clear ();
				vectors_line_1 [i].gameObject.SetActive (false);
			}
			sup_Index_of_Vectors_list_3 = 0;
			break;
		}
	}

	void Remove_everything()
	{
		for (int i = 0; i < vectors.Count; i++) 
		{
			MonoBehaviour.Destroy (vectors [i].gameObject);
			//vectors[i].gameObject.
		}
		vectors.Clear();
		sup_Index_of_Vectors_list_2 = 0;

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

		for (int i = 0; i < vectors_line_1.Count; i++) 
		{
			MonoBehaviour.Destroy (vectors_line_1 [i].gameObject);
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
		if (list.Count < 2)
			return;
		if (!options.isTime_for_Light_Render_Grafick) 
		{
			//List<Edge> line = new List<Edge> ();
			for (int i = 0; i < list.Count - 1; i++) 
			{
				Edge edge = addLine (list [i], list [i + 1]);
				lines.Add (edge);
				//addPoint (list [i]);
			}
		} 
		else 
		{
			print ("Hello World of Light Render");
			if (sup_Index_of_Lines >= vec_of_Lines.Count) 
			{
				Line line = (MonoBehaviour.Instantiate (clones.line_2, new Vector3 (), Quaternion.Euler (0, 0, 0))as GameObject).GetComponent<Line> ();
				vec_of_Lines.Add (line);
				List<Vector3> _list = new List<Vector3> ();
				for (int i = 0; i < list.Count; i++) {
					vec_of_lines.Add (list [i]);
					_list.Add (list [i]);
				}
				line.addFunctionPoints (_list);
				line.transform.SetParent (parents.Lines.transform);
				sup_Index_of_Lines++;
			} 
			else 
			{
                for (int i = 0; i < list.Count; i++)
                {
                    vec_of_lines.Add(list[i]);
                }
                vec_of_Lines[sup_Index_of_Lines].gameObject.SetActive(true);
				vec_of_Lines[sup_Index_of_Lines].addFunctionPoints(list);
				sup_Index_of_Lines++;
			}
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
		switch (options.isTime_for_Light_Render_Vector) 
		{
		case 1:
			if (sup_Index_of_Vectors_list_1 >= vectors.Count) {
				Edge vector = (Object.Instantiate (clones.vector, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Edge> ();
				vectors.Add (vector);
				vector.set (start, end);
				vector.transform.SetParent (parents.Vectors.transform);
				//return vector;
			} else {
				vectors [sup_Index_of_Vectors_list_1].set (start, end);
				vectors [sup_Index_of_Vectors_list_1].gameObject.SetActive (true);
				//return vectors [sup_Index_of_Vectors_list++];
			}
			sup_Index_of_Vectors_list_1++;
			break;
		case 2:
			if (sup_Index_of_Vectors_list_2 >= vectors.Count) 
			{
				Line vector = (Object.Instantiate (clones.line, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Line> ();
				vectors_line.Add (vector);
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vector.addFunctionPoints (list);
				vector.transform.SetParent (parents.Vectors.transform);
				//return vector;
			} 
			else
			{
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vectors_line[sup_Index_of_Vectors_list_2].gameObject.SetActive (true);
				vectors_line[sup_Index_of_Vectors_list_2].addFunctionPoints (list);
				//return vectors [sup_Index_of_Vectors_list++];
			}
			sup_Index_of_Vectors_list_2++;
			break;
		case 3:
			if (sup_Index_of_Vectors_list_3 >= vectors.Count) 
			{
				Line vector = (Object.Instantiate (clones.line_1, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Line> ();
				vectors_line_1.Add (vector);
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vector.addFunctionPoints (list);
				vector.transform.SetParent (parents.Vectors.transform);
				//return vector;
			} 
			else
			{
				List<Vector3> list=new List<Vector3>();
				list.Add (start);
				list.Add (end);
				vectors_line_1[sup_Index_of_Vectors_list_3].gameObject.SetActive (true);
				vectors_line_1[sup_Index_of_Vectors_list_3].addFunctionPoints (list);
				//return vectors [sup_Index_of_Vectors_list++];
			}
			sup_Index_of_Vectors_list_3++;
			break;
		}

	}

	public void addVectors(List<Vector3> points,List<Vector3> list_of_speed)
	{
		//
		for (int i = 0; i < points.Count; i++) 
		{
            if(options.isProjectionTime)
                list_of_speed[i] = Vector3.ProjectOnPlane(list_of_speed[i], current_normal);
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
	public void Clear_list_of_lines()
	{
		for (int i = 0; i < vec_of_Lines.Count; i++) 
		{
			vec_of_Lines [i].Clear ();
			vec_of_Lines[i].gameObject.SetActive(false);
		}
        vec_of_lines.Clear();
		sup_Index_of_Lines = 0;
	}

	public List<Vector3> getList_of_lines()
	{
		return vec_of_lines;
	}

	public List<List<Vector3> > getList_of_Lines()
	{
		List<List<Vector3> > list = new List<List<Vector3>> ();
		for (int i = 0; i < vec_of_Lines.Count; i++) 
		{
			list.Add (vec_of_Lines [i].getFunctionPoint ());
		}
		return list;
	}

	public void setList_of_Line(List<List<Vector3> > list)
	{
        //print("Hello new List of line:"+list.Count);
		Clear_list_of_lines();
		for (int i = 0; i < list.Count; i++) 
		{
			addLines (list [i]);
            //print(list[i]);
		}
	}

	[System.Serializable]
	public struct OBJECTS
	{
		public Camera_Controller Cam_cont;
		public Algorightm algo;
        public File_Controller file_cont;
	}
	[System.Serializable]
	public struct CLONE_OF_OBJECTS
	{
		public GameObject line;
		public GameObject line_1;
		public GameObject line_2;
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
		public int isTime_for_Light_Render_Vector;
		public bool isTime_for_Light_Render_Grafick;
        public bool isProjectionTime;
	}
}
