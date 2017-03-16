﻿using UnityEngine;
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
	List<Arrow> vectors_line_3;
    //List<Arrow> vectors_of_arrow;

	void Start () 
    {

		points = new List<GameObject> ();
		lines = new List<Edge>  ();
		vectors = new List<Edge> ();
		vectors_line = new List<Line> ();
		vec_of_lines = new List<Vector3> ();
		vec_of_Lines = new List<Line> ();
        vectors_line_3 = new List<Arrow> ();
        //vectors_of_arrow = new List<Arrow>();
        addEllipse(new Vector3(),10,10);
        addEllipse(new Vector3(0,0,5),4,4);

        //Obj.move_controller.StartCreateMoves("ellipseE02");
        //Obj.move_controller.StartCreateMoves("ellipseE03");
        Obj.move_controller.StartPlayMove("ellipseE03");
        //Clear_list_of_lines();

        //AddTore(new Vector3(),10,8,4*100,4);
        //Obj.move_controller.StartCreateMoves("Tore1");

        //Obj.move_controller.StartPlayMove("Tore1");

        //Obj.grad_table.create_table(options.number_of_grad, options.minColor, options.maxColor);
        //Obj.speed_table.create_table(options.number_of_grad, options.minV, options.maxV);

        //Obj.online_options.create_table(options.number_of_grad, options.minColor, options.maxColor, options.minV, options.maxV);
	}

	void OnApplicationQuit()//берш
	{
		print ("Hello World AAAAAAAAAAAAAAAAAAAAAA I Can FLYYYYYYYYYYYYYYYYYYY...BOOMMMMMMM");
		//test.isWork = false;
		Obj.algo.Des_Time();
		Remove_everything ();
	}
       
    void addMöbius_strip(float r)
    {
        for (float v = -0.8f; v <= 0.8f; v += 0.05f)
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

    void addTriangle(Vector3 a,Vector3 b,Vector3 c)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        //list.Add(a);
        addLines(list);
    }

    void AddTore(Vector3 pos,float R,float r,float stepR,float stepr)
    {
        List<Vector3> list = getTore(pos, R, r, stepR, stepr);
        addLines(list);
    }

    void AddTore(Vector3 pos,float R,float r,int n,int N)
    {
        AddTore(pos, R, r, 2*Mathf.PI / n, 2*Mathf.PI * N / n);
    }

    void addEllipse(Vector3 pos,float a,float b)
    {
        addLines(getEllipse(pos, a, b));
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

    List<Vector3> getTriangle(Vector3 a,Vector3 b,Vector3 c)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(a);
        return list;
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
        list.Add(vec);
        return list;
    }

    List<Vector3> getTore(Vector3 pos, float R,float r,float stepR,float stepr)
    {
        Vector3 point;
        List<Vector3> list=new List<Vector3>();
        for(float gamma=0, alpha=0;gamma<2*Mathf.PI;gamma+=stepR,alpha+=stepr)
        {
            point = new Vector3((R + r * Mathf.Cos(alpha)) * Mathf.Cos(gamma), (R + r * Mathf.Cos(alpha)) * Mathf.Sin(gamma), r * Mathf.Sin(alpha));
            list.Add(point);
        }
        point=new Vector3((R + r * Mathf.Cos(0)) * Mathf.Cos(0), (R + r * Mathf.Cos(0)) * Mathf.Sin(0), r * Mathf.Sin(0));
        list.Add(point);
        return list;
    }

	
	// Update is called once per frame
	bool wait=false;
	float time=0;
	Test test;
	int sup_Index_of_Vectors_list_1=0;
	int sup_Index_of_Vectors_list_2=0;
	int sup_Index_of_Vectors_list_3=0;
    int sup_Index_of_Vectors_list_4=0;
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
        if (Input.GetButtonUp("Line_4"))
        {
            if (options.isTime_for_Light_Render_Vector != 4)
            {
                print("Line_4");
                Clear_Vectors_list();
                options.isTime_for_Light_Render_Vector = 4;
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

    void Move_and_Moves()
    {
        if (Obj.move_controller.isCreate || Obj.move_controller.isPlay)
        {
            Obj.algo.Obj.main_canvas.setText("Time:" + Obj.move_controller.current_time);
        }
        else
        {
            Obj.algo.Obj.main_canvas.setText("");
        }
    }

    void StartSearchVector_of_speed(List<Vector3> lines,List<Vector3> points)
    {
        Obj.algo.FindVector_of_speed(lines, points);
        wait = true;
    }

    public List<Line> getVector_of_Line()
    {
        return vec_of_Lines;
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
			for (int i = 0; i < vectors_line_3.Count; i++) 
			{
				vectors_line_3 [i].Clear ();
				vectors_line_3 [i].gameObject.SetActive (false);
			}
			sup_Index_of_Vectors_list_3 = 0;
			break;
        case 4:
                for (int i = 0; i < vectors_line_3.Count; i++) 
            {
                vectors_line_3 [i].Clear ();
                vectors_line_3 [i].gameObject.SetActive (false);
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

		for (int i = 0; i < vectors_line_3.Count; i++) 
		{
			MonoBehaviour.Destroy (vectors_line_3 [i].gameObject);
		}

        for (int i = 0; i < vectors_line_3.Count; i++)
        {
            MonoBehaviour.Destroy(vectors_line_3[i].gameObject);
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

    public void Refresh_Color_of_Vectors()
    {
        if(options.isTime_for_Light_Render_Vector!=4)
            return;

        for (int i = 0; i < vectors_line_3.Count; i++)
        {
            vectors_line_3[i].RefreshColor(options.minColor, options.maxColor, options.minV, options.maxV);
        }
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
                if (sup_Index_of_Vectors_list_2 >= vectors_line.Count) 
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
                
            if (sup_Index_of_Vectors_list_3 >= vectors_line_3.Count) 
			{
                Arrow vector = (Object.Instantiate (clones.line_1, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Arrow> ();
				vectors_line_3.Add (vector);
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
				vectors_line_3[sup_Index_of_Vectors_list_3].gameObject.SetActive (true);
				vectors_line_3[sup_Index_of_Vectors_list_3].addFunctionPoints (list);
				//return vectors [sup_Index_of_Vectors_list++];
			}
			sup_Index_of_Vectors_list_3++;
			break;
        case 4:
                
            float V = (end - start).magnitude;
            end = start + (end - start).normalized*options.standart_size_of_vector;
            //print(V);
                if (sup_Index_of_Vectors_list_3 >= vectors_line_3.Count) 
            {
                Arrow vector = (Object.Instantiate (clones.line_1, new Vector3 (), Quaternion.Euler (0, 0, 0)) as GameObject).GetComponent<Arrow> ();
                vectors_line_3.Add (vector);
                List<Vector3> list=new List<Vector3>();
                list.Add (start);
                list.Add (end);
                vector.addFunctionPoints (list);
                vector.transform.SetParent (parents.Vectors.transform);
                vector.setColorV(V, options.minColor, options.maxColor,options.minV, options.maxV);
                //return vector;
            } 
            else
            {

                List<Vector3> list=new List<Vector3>();
                list.Add (start);
                list.Add (end);
                vectors_line_3[sup_Index_of_Vectors_list_3].gameObject.SetActive (true);
                vectors_line_3[sup_Index_of_Vectors_list_3].addFunctionPoints (list);
                    vectors_line_3[sup_Index_of_Vectors_list_3].setColorV(V, options.minColor, options.maxColor,options.minV, options.maxV);
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
            if(options.isTime_for_Light_Render_Vector==4&&current_normal!=null&options.isProjectionTime)
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

    public List<Line> getList_of_Lines()
	{
        return vec_of_Lines;
	}

	public List<List<Vector3> > getList_of_lines()
	{
        List<List<Vector3> > list = new List<List<Vector3>> ();
        List<Vector3> list_2;
        List<Vector3> function_point;
		for (int i = 0; i < vec_of_Lines.Count; i++) 
		{
            function_point = vec_of_Lines[i].getFunctionPoint();
            list_2 = new List<Vector3>();
            for (int j = 0; j < function_point.Count; j++)
            {
                list_2.Add(function_point[j]);
            }
            list.Add (list_2);
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
        public Online_Options online_options;
        public Move_Controller move_controller;
        //public table_of_grad grad_table;
        //public table_of_speed speed_table;
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
        public Color minColor;
        public Color maxColor;
        public float minV;
        public float maxV;
        public float standart_size_of_vector;

        public int number_of_grad;
	}
}
