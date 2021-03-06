﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Algorightm : MonoBehaviour {

	// Use this for initialization
	public Options options;
	public Objects Obj;
	public List<Vector3> lists_of_speed;
    public List<Vector3> lists_of_speed2;
	List<Edge> _lines;
	List<Vector3> _points;
	List<Vector3> _lines_2;
    List<List<Vector3> > _lines_3;
    int index_of_lines_3;
	List<MyThread> threads;
	Thread thread;
	[System.NonSerialized]
	public bool isWork=false;
	[System.NonSerialized]
	public bool isTime_Start=false;
	void Awake () 
	{
		//print ("Hello World");
		options.isVector3Line = Obj.cont.options.isTime_for_Light_Render_Grafick;
		threads = new List<MyThread> ();
		thread = new Thread (update);

		thread.Start();


	}

	public void plus(int a)
	{
		count_of_thread += a;
	}


	public void Des_Time()
	{
		return;
		for (int i = 0; i < threads.Count; i++) 
		{
			threads [i].Des ();
		}
		isTime_exit = true;
	}
	bool isTime_exit=false;
	// Update is called once per frame
	void Update () {
	}

	public static Vector3 getMagicVec(Vector3 a,Vector3 b)
	{
		Vector3 res = new Vector3 ();
		res.x = a.y * b.z - a.z * b.y;
        res.y = -(a.x * b.z - a.z * b.x);
		res.z = a.x * b.y - a.y * b.x;
        //print("lenght of normal:" + res.magnitude);
		res.Normalize();
		return res;
	}

	void update()
	{
		print("thread algorithm:"+thread.ManagedThreadId);
		while (!isTime_exit) 
		{
			//print ("Hello " + thread.ManagedThreadId);
			if (isTime_Start)
            {
                isTime_Start = false;
                if (index_of_lines_3 == -1)
                {
                    thread.Priority = System.Threading.ThreadPriority.Normal;
                    print("FindList_of_speed");
                    FindList_of_speed();
                    thread.Priority = System.Threading.ThreadPriority.Normal;
                }
                else
                {
                    if (_lines_3.Count == 0)
                        continue;
                    if (index_of_lines_3 >= _lines_3.Count)
                    {
                        index_of_lines_3 = -1;
                        continue;
                    }
                    _lines_2 = _lines_3[index_of_lines_3];
                    //print("Count:"+_lines_2.Count);
                    thread.Priority = System.Threading.ThreadPriority.Highest;
                    print("FindList_of_speed");
                    FindList_of_speed();
                    thread.Priority = System.Threading.ThreadPriority.Normal;
                }
			}
			Thread.Sleep (0);
		}
	}

	public void FindVector_of_speed(List<Edge> lines,List<Vector3> points)
	{
		if (isWork)
			return;
		_lines = lines;
		_points = points;

		isTime_Start=true;
		isWork = true;
		//StartCoroutine ("FindList_of_speed");
		//FindList_of_speed ();
        index_of_lines_3=-1;
		print("FindVector_of_speed");
	}

	public void FindVector_of_speed(List<Vector3> _lines,List<Vector3> points)
	{
		if(isWork)
			return;
		_lines_2=_lines;
		_points=points;
		isTime_Start=true;
		isWork=true;
	}

    public void FindVector_of_speed(List<List<Vector3> > _lines,List<Vector3> points)
    {
        if (isWork)
            return;
        
        _lines_3 = _lines;
        _points = points;
        index_of_lines_3 = 0;

        lists_of_speed2 = new List<Vector3>();
        for (int i = 0; i < points.Count; i++)
        {
            lists_of_speed2.Add(new Vector3());
        }
        isWork = true;
        isTime_Start = true;
    }




	[System.NonSerialized]
	public int count_of_thread;

	void FindList_of_speed()
	{
		print ("Find List of speed");
		int i, j, k;
		lists_of_speed = new List<Vector3> ();
		for (i = 0; i < _points.Count; i++) {
			lists_of_speed.Add (new Vector3 ());
		}
		count_of_thread = 0;
        print("oks:" + options.isVector3Line);
		if (options.isVector3Line) 
		{
            print("<");
            print(_points.Count);
            print(_lines_2.Count);
            print(_points.Count + "<" + (_lines_2.Count - 1));
			if (_points.Count < _lines_2.Count-1) 
			{
				while (threads.Count < _points.Count) 
				{
					threads.Add (new MyThread (this));
				}
				for (i = 0; i < _lines_2.Count-1; i++) 
				{

					setNumber_in_main_canvas (i, _lines_2.Count-1,"% line");
					for (j = 0; j < _points.Count; j++) 
					{
						k = (i + j) % (_lines_2.Count-1);
						threads [j].Start (_lines_2[k],_lines_2[k+1],_points[j],options.TestGamma);
					}
					while (options.isThreding&&count_of_thread > 0) 
					{
						//print ("infin");
						//yield return new WaitForSeconds (0);
						//thread.Join();
						if (isTime_exit)
							return;
						Thread.Sleep (0);
					}
					for (j = 0; j < _points.Count; j++) 
					{
						k = (i + j) % _lines_2.Count-1;
						//print (lists_of_speed.Count + "-" + k);
						lists_of_speed [j] += threads [j].V;
					}
					Thread.Sleep (0);

				}
			} 
			else 
			{
				while (threads.Count < _lines_2.Count-1) 
				{
					threads.Add (new MyThread (this));
				}
				for (i = 0; i < _points.Count; i++) 
				{
					//print (i);
					setNumber_in_main_canvas(i,_points.Count,"% point");
					for (j = 0; j < _lines_2.Count-1; j++) 
					{
						k = (i + j) % _points.Count;
						threads [j].Start (_lines_2 [j], _lines_2 [j+1], _points [k], options.TestGamma);
					}
					while (options.isThreding&&count_of_thread > 0) 
					{
						//print ("count_of_thread:"+count_of_thread);
						//yield return new WaitForSeconds (0);
						//thread.Join();
						if (isTime_exit)
							return;
						Thread.Sleep(0);
					}
					for (j = 0; j < _lines_2.Count-1; j++) 
					{
						k = (i + j) % _points.Count;
						//print (j + " " + lists_of_speed.Count);
						lists_of_speed [k] += threads [j].V;
					}
					Thread.Sleep (0);
				}

			}
		} 
		else 
		{
			if (_points.Count < _lines.Count) 
			{
				while (threads.Count < _points.Count) 
				{
					threads.Add (new MyThread (this));
				}
				for (i = 0; i < _lines.Count; i++) 
				{
					setNumber_in_main_canvas (i, _lines.Count,"% line");
					for (j = 0; j < _points.Count; j++) 
					{
						k = (i + j) % _lines.Count;
						threads [j].Start (_lines[k].LeftConer,_lines[k].RightConer,_points[j],options.TestGamma);
					}
					while (options.isThreding&&count_of_thread > 0) 
					{
						//print ("infin");
						//yield return new WaitForSeconds (0);
						//thread.Join();
						if (isTime_exit)
							return;
						Thread.Sleep (0);
					}
					for (j = 0; j < _points.Count; j++) 
					{
						k = (i + j) % _lines.Count;
						//print (lists_of_speed.Count + "-" + k);
						lists_of_speed [j] += threads [j].V;
					}

				}
			} 
			else 
			{
				while (threads.Count < _lines.Count) 
				{
					threads.Add (new MyThread (this));
				}
				for (i = 0; i < _points.Count; i++) 
				{
					//print (i);
					setNumber_in_main_canvas(i,_points.Count,"% point");
					for (j = 0; j < _lines.Count; j++) 
					{
						k = (i + j) % _points.Count;
						threads [j].Start (_lines [j].LeftConer, _lines [j].RightConer, _points [k], options.TestGamma);
					}
					while (options.isThreding&&count_of_thread > 0) 
					{
						//print ("count_of_thread:"+count_of_thread);
						//yield return new WaitForSeconds (0);
						//thread.Join();
						if (isTime_exit)
							return;
						Thread.Sleep(0);
					}
					for (j = 0; j < _lines.Count; j++) 
					{
						k = (i + j) % _points.Count;
						//print (j + " " + lists_of_speed.Count);
						lists_of_speed [k] += threads [j].V;
					}
				}

			}
		}
        End_of_fspeed();
	}

    void End_of_fspeed()
    {
        if (index_of_lines_3 == -1)
        {
            print("end Work");
            isWork = false;
            Clear_text_in_main_canvas();
        }
        else
        {
            print("find:" + index_of_lines_3);
            lists_of_speed2=Move_Controller.plus(lists_of_speed2, lists_of_speed);
            index_of_lines_3++;
            if (_lines_3.Count <= index_of_lines_3)
            {
                lists_of_speed.Clear();
                lists_of_speed = lists_of_speed2;
                lists_of_speed2 = null;
                _lines_3 = null;
                print("end Work");
                isWork = false;
                Clear_text_in_main_canvas();

            }
            else
            {
                //_lines_2 = _lines_3[index_of_lines_3];
                isTime_Start = true;
            }
        }
    }

	void setNumber_in_main_canvas(int a,int p,string text)
	{
		//print (a.ToString () + "/" + p.ToString () + text);
		//Obj.main_canvas.setText (a.ToString () + "/" + p.ToString ()+text);
	}

	void Clear_text_in_main_canvas()
	{
		Obj.main_canvas.setText ("");
	}

	[System.Serializable]
	public struct Options
	{
		public float TestGamma;
		public float deltaR;
		public bool isThreding;
		public bool isVector3Line;
	}
	[System.Serializable]
	public struct Objects
	{
		public Main_Canvas main_canvas;
		public Controller cont;
	}
	

}
public class MyThread:MonoBehaviour
{
	Algorightm _algo;
	float Gamma;
	Vector3 A;
	Vector3 B;
	Vector3 M;
	Thread thread;
	public Vector3 V;
	public MyThread(Algorightm algo)
	{
		_algo=algo;
		if (algo.options.isThreding) 
		{
			thread = new Thread (update);
			thread.IsBackground = true;

			thread.Start ();
		}
	}
	public void Start(Vector3 a,Vector3 b,Vector3 m,float gamma)
	{
		A = a;
		B = b;
		M = m;
		Gamma = gamma;
		_algo.plus(1);
		//print ("++");
		//StartCoroutine ("_Start");

		//print ("--?");
		if(_algo.options.isThreding)
			Time_to_Start=true;
		else
			_Start();

	}
	bool Time_to_Start=false;
	bool Time_to_Des=false;
	public void Des()
	{
		Time_to_Des = true;
	}
	void update()
	{
		//print("thread:"+thread.ManagedThreadId);
		while (!Time_to_Des) 
		{
			if (Time_to_Start) 
			{
				Time_to_Start = false;
				thread.Priority = System.Threading.ThreadPriority.AboveNormal;
				print ("_Start");
				_Start ();
				thread.Priority = System.Threading.ThreadPriority.Lowest;
			}
			Thread.Sleep (0);
		}
	}
	void _Start()
	{
		//print ("_Start");
		//print ("Start:" + Thread.GetDomainID ());
		Vector3 AB = B - A;
		Vector3 AM = M - A;
		Vector3 BM = M - B;
		float l_AB = Vector3.Distance (A, B);
		float l_AM = Vector3.Distance (A, M);
		float l_BM = Vector3.Distance (M, B);
        float cos_a, cos_b;
        if (l_AB * l_AM * l_BM != 0)
        {
            cos_a = (l_AB * l_AB + l_AM * l_AM - l_BM * l_BM) / (2 * l_AB * l_AM);
            cos_b = (l_AB * l_AB + l_BM * l_BM - l_AM * l_AM) / (2 * l_AB * l_BM);
        }
        else
        {
            cos_a = 1;
            cos_b = 1;
        }
		float _h=Mathf.Abs(l_AM*Mathf.Sqrt(1- cos_a*cos_a));
		//print (_h);
        _h=(_h<_algo.options.deltaR?epsilon(_h,_algo.options.deltaR):1/_h);
        if (float.IsNaN(_h))
            _h = 0;
        if (_h == 0)
        {
            V = new Vector3();
        }
        else
        {
            float v=Gamma*_h/(4*Mathf.PI)*(cos_a+cos_b);
            V = Algorightm.getMagicVec(AM,AB)*v;
        }
        //print("Algo:"+V);
		//yield return new WaitForSeconds (0);
		End ();
		//print ("End:" + Thread.GetDomainID ());
	}

	void End ()
	{
		//print ("--");
		_algo.plus(-1);
		//print ("Hel" + thread.ManagedThreadId);
	}

    float epsilon(float _h,float delta)
    {
        //options.deltaR2 = options.deltaR * options.deltaR;
        float _delta=delta*delta*delta;
        return _h * _h / _delta;
    }

}