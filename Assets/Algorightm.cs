using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Algorightm : MonoBehaviour {

	// Use this for initialization
	public Options options;
	public List<Vector3>  lists_of_speed;
	List<Edge> _lines;
	List<Vector3> _points;
	List<Thread> threads;
	public bool isWork=false;
	void Awake () 
	{
		//print ("Hello World");
		threads = new List<Thread> ();
	}

	
	// Update is called once per frame
	void Update () {
		options.deltaR2 = options.deltaR * options.deltaR;
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

	public void FindVector_of_speed(List<Edge> lines,List<Vector3> points)
	{
		if (isWork)
			return;
		_lines = lines;
		_points = points;
		isWork = true;
		//StartCoroutine ("FindList_of_speed");
		FindList_of_speed ();
	}

	[System.NonSerialized]
	public int count_of_thread;

	void FindList_of_speed()
	{
		//print ("Find List of speed");
		int i, j, k;
		lists_of_speed = new List<Vector3> ();
		for (i = 0; i < _points.Count; i++) {
			lists_of_speed.Add (new Vector3 ());
		}
		count_of_thread = 0;
		if (_points.Count < _lines.Count) 
		{
			while (threads.Count < _points.Count) 
			{
				threads.Add (new Thread (this));
			}
			for (i = 0; i < _lines.Count; i++) 
			{
				for (j = 0; j < _points.Count; j++) 
				{
					k = (i + j) % _lines.Count;
					threads [j].Start (_lines[k].LeftConer,_lines[k].RightConer,_points[j],options.TestGamma);
				}
				while (false&&count_of_thread > 0) 
				{
					//print (count_of_thread);
					//yield return new WaitForSeconds (0);
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
				threads.Add (new Thread (this));
			}
			for (i = 0; i < _points.Count; i++) {
				for (j = 0; j < _lines.Count; j++) {
					k = (i + j) % _points.Count;
					threads [j].Start (_lines [j].LeftConer, _lines [j].RightConer, _points [k], options.TestGamma);
				}
				while (false&&count_of_thread > 0) 
				{
					//yield return new WaitForSeconds (0);
				}
				for (j = 0; j < _lines.Count; j++) 
				{
					k = (i + j) % _points.Count;
					//print (j + " " + lists_of_speed.Count);
					lists_of_speed [k] += threads [j].V*10;
				}
			}
			
		}
		//print ("end Work");
		isWork = false;
	}

	[System.Serializable]
	public struct Options
	{
		public float TestGamma;
		public float deltaR;
		public float deltaR2;
	}
	

}
public class Thread:MonoBehaviour
{
	Algorightm _algo;
	float Gamma;
	Vector3 A;
	Vector3 B;
	Vector3 M;
	public Vector3 V;
	public Thread(Algorightm algo)
	{
		_algo=algo;
	}
	public void Start(Vector3 a,Vector3 b,Vector3 m,float gamma)
	{
		A = a;
		B = b;
		M = m;
		Gamma = gamma;
		_algo.count_of_thread++;
		//print ("++");
		//StartCoroutine ("_Start");
		_Start();
		//print ("--?");
	}

	void _Start()
	{
		//print ("_Start");
		Vector3 AB = B - A;
		Vector3 AM = M - A;
		Vector3 BM = M - B;
		float l_AB = Vector3.Distance (A, B);
		float l_AM = Vector3.Distance (A, M);
		float l_BM = Vector3.Distance (M, B);
		float cos_a = (l_AB * l_AB + l_AM * l_AM - l_BM * l_BM) / (2 * l_AB * l_AM);
		float cos_b = (l_AB * l_AB + l_BM * l_BM - l_AM * l_AM) / (2 * l_AB * l_BM);
		float _h=Mathf.Abs(l_AM*Mathf.Sqrt(1- cos_a*cos_a));
		//print (_h);
		_h=(_h<_algo.options.deltaR?_h/_algo.options.deltaR2:1/_h);
		//print (_h);
		float v=Gamma*_h/(4*Mathf.PI)*(cos_a+cos_b);
		V = Algorightm.getMagicVec(AM,AB)*v;
		//yield return new WaitForSeconds (0);
		End ();
	}

	void End ()
	{
		//print ("--");
		//_algo.count_of_thread--;
	}

}