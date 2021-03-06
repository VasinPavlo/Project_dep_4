﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum State_of_Line{Const_Lenght,Without_Restrictions,Flesh,Flesh_End,End_Time};
public class Line : MonoBehaviour {
	/*/
	public float speed=100f;
	public float magnitude=10f;
	public float magnitude_speed=1f;
	public int const_length=10;
	public static bool magicSin=true;
	public static bool isTimeForSin=false;
	public State_of_Line state=State_of_Line.Without_Restrictions;
	/*/
	//protected Vector3[]
	public Options options;
	protected List<Vector3> list;
	protected List<Vector3> function;
	protected LineRenderer line_renderer;

	int Index;
	int lastIndex;
	int min_Index=0;
	float _speed;
	float distance;
	bool isChanged;
	bool isDestroyTime=false;
	bool isFleshTime=true;
	public delegate void _awake();
	public event _awake awake;
    static bool isPlayMode = false;
    static int maxL=-1;
	public void Awake()
	{
		line_renderer = GetComponent<LineRenderer> ();
		list = new List<Vector3> ();
		function = new List<Vector3> ();
		Index = 1;
		/*/
		function.Add (new Vector3 (-100, -100, 0));
		function.Add (new Vector3 (0, 0, 0));
		function.Add (new Vector3 (100, 50, 0));
		function.Add (new Vector3 (-50, 100, 0));
		function.Add (new Vector3 (-70, 20, 0));
		function.Add (new Vector3 (10, 35, 0));
		function.Add (new Vector3 (100, 100, 0));
		Add (function [0]);
		/*/
		if (awake != null)
			awake ();
	}

    public static void SetPlayState(bool b)
    {
        isPlayMode = b;
    }

    public static void SetPlayState(bool b,int l)
    {
        maxL = l;
        isPlayMode = b;
    }
	public void Add(Vector3 vec)
	{
		return;
		line_renderer.SetVertexCount (list.Count);
		line_renderer.SetPosition (list.Count - 1, vec);
    }


	void Update() 
	{
		if(true||!options.magicSin)
		{
			list=function;
			options.isTimeForSin=false;
			return;
		}
		switch(options.state)
		{
		case State_of_Line.Without_Restrictions:
		{
			break;
		}
		case State_of_Line.Const_Lenght:
		{
			while(list.Count+1> options.const_length)
			{
				list.RemoveAt(0);
				min_Index++;
			}
			break;
		}
		case State_of_Line.End_Time:
		{
			if(list.Count==0)
			{
				toBegin();
				//function.Clear();

			}
			else
			{
				list.RemoveAt(0);
				min_Index++;

			}
			break;
		}
		case State_of_Line.Flesh:
		{
			if(isFleshTime)
			{
				Build();
				isFleshTime=false;
			}
			return;
		}
		case State_of_Line.Flesh_End:
		{
			if(isFleshTime)
			{
				toBegin();
				isFleshTime=false;
			}
			break;
		}
		}
		if (Index >= function.Count||function.Count<=0)
		{
			if(isDestroyTime&&options.state==State_of_Line.Const_Lenght)
				options.state=State_of_Line.End_Time;
			return;
		}
		if (list.Count <= 0)
			return;
		lastIndex=list.Count-1;
		_speed = options.speed * 0.0197486f;//*Time.deltaTime;
		//print (list [lastIndex].ToString() + function [Index].ToString());
		distance = Vector3.Distance (list [lastIndex], function [Index]);
		//print (distance+">"+Time.deltaTime);
		isChanged = false;
		while(distance<_speed)
		{
			_speed-=distance;
			isChanged=true;
			//list.Add(function[Index]);
			Index++;
			if(Index==function.Count)
			{
				list.Add(function[Index-1]);
				return;
			}
			distance = Vector3.Distance (function [Index-1], function [Index]);
		}
		if(!isChanged)
		{
			list.Add (Vector3.MoveTowards(list[lastIndex],function[Index],_speed));
		}
		else
		{
			list.Add(Vector3.MoveTowards(function[Index-1],function[Index],_speed));
		}
	}
	int Index_of_Wave=1;
	void LateUpdate()
	{
		//Clear ();
        return;
		if (Index_of_Wave > list.Count - 1)
			Index_of_Wave = list.Count - 1;
		if (Index_of_Wave < 1)
			Index_of_Wave = 1;
		/*/
		for(int i=0;i<list.Count;i++)
		{
			line_renderer.SetPosition(i,list[i]);
		}
		return;
		/*/
		if (list.Count < 2)
			return;
		line_renderer.SetVertexCount (list.Count);
		line_renderer.SetPosition (0, list [0]);
		line_renderer.SetPosition (list.Count-1, list[list.Count-1]);
		isDestroyTime = true;
		if(options.isTimeForSin)
		{
			if(Index_of_Wave<list.Count-1)
			{
				if(options.state==State_of_Line.Flesh)
				{
					Index_of_Wave=list.Count-1;
				}
				else
					Index_of_Wave++;
			}
			for(int i=1;i<Index_of_Wave;i++)//
			{
				line_renderer.SetPosition  (i,getCrazyPoint(list[i],list[i+1]-list[i-1],-Mathf.Sin(i+min_Index+Time.time*options.magnitude_speed)));
			}
		}
		else
		{
			if(Index_of_Wave>1)
			{
				if(options.state==State_of_Line.Flesh)
				{
					Index_of_Wave=1;
				}
				else
					Index_of_Wave--;
			}
			for(int i=1;i<list.Count-Index_of_Wave;i++)
			{
				line_renderer.SetPosition (i,list[i]);
			}
			for(int i=list.Count-Index_of_Wave;i<list.Count-1;i++)//
			{
				line_renderer.SetPosition  (i,getCrazyPoint(list[i],list[i+1]-list[i-1],-Mathf.Sin(i+min_Index+Time.time*options.magnitude_speed)));
			}
		}
	}
	float _x;
	Vector3 getCrazyPoint(Vector3 point,Vector3 vec,float sin)
	{
		//
		_x = vec.x;
		vec.x = -vec.y;
		vec.y = _x;
		//
		return point+vec*sin/vec.magnitude*options.magnitude;
	}

	public List<Vector3> getFunctionPoint()
	{
		return function;
	}

	void toBegin()
	{
		isDestroyTime=false;
		isFleshTime = true;
		Index=1;
		Index_of_Wave=1;
		min_Index=0;
		line_renderer.SetVertexCount(0);
		list.Clear();
	}
	public void addFuctionPoint(Vector3 vec)
	{
		function.Add (vec);
		if (function.Count == 0)
			list.Add(vec);
		//print(list[0].ToString());
	}
	public void setFunctionPoints(List<Vector3> _list)
	{
		if (_list == null || _list.Count == 0)
			return;
		//print (function.Count + " count");
		//toBegin ();
        if (!isPlayMode || maxL < 0||_list.Count<=maxL)
        {
            function = _list;
            line_renderer.SetVertexCount(function.Count);
            for (int i = 0; i < function.Count; i++)
            {
                line_renderer.SetPosition(i, function[i]);
            }
        }
        else
        {
            line_renderer.SetVertexCount(maxL);
            function.Clear();
            float step = (float)maxL / _list.Count;
            float d=1;
            int I = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                if (d >= 1)
                {
                    line_renderer.SetPosition(I, _list[i]);
                    function.Add(_list[i]);
                    I++;
                    d -= 1;
                }
                d += step;
            }
            line_renderer.SetPosition(maxL - 1, _list[_list.Count - 1]);

        }
		list.Add (_list [0]);
		if (options.arrow != null&&_list.Count>=2) 
		{
			Vector3 start, end;
			start = _list [_list.Count - 2];
			end = _list [_list.Count - 1];
			start = Vector3.Lerp (end, start, options.lenght_of_arrow);

			List<Vector3> vec = new List<Vector3> ();
			vec.Add (start);
			vec.Add (end);
			options.arrow.setFunctionPoints (vec);
		}
	}

    public void SetColors(Color begin,Color end)
    {
        line_renderer.SetColors(begin, end);
    }
	public void Clear()
	{
		function = new List<Vector3> ();
		if (options.arrow != null) 
		{
			options.arrow.Clear ();
		}
		toBegin ();
	}

	public void FleshTime()
	{
		isFleshTime = true;
	}
	void Build()
	{
		while (true) 
		{
			if (Index >= function.Count || function.Count <= 0) {
				if (isDestroyTime && options.state == State_of_Line.Const_Lenght)
					options.state = State_of_Line.End_Time;
				return;
			}
			if (list.Count <= 0)
				return;
			lastIndex = list.Count - 1;
			_speed = options.speed * 0.0197486f;//*Time.deltaTime;
			//print (list [lastIndex].ToString() + function [Index].ToString());
			distance = Vector3.Distance (list [lastIndex], function [Index]);
			//print (distance+">"+Time.deltaTime);
			isChanged = false;
			while (distance<_speed) {
				_speed -= distance;
				isChanged = true;
				Index++;
				if (Index == function.Count) {
					list.Add (function [Index - 1]);
					return;
				}
				distance = Vector3.Distance (function [Index - 1], function [Index]);
			}
			if (!isChanged) {
				list.Add (Vector3.MoveTowards (list [lastIndex], function [Index], _speed));
			} else {
				list.Add (Vector3.MoveTowards (function [Index - 1], function [Index], _speed));
			}
		}
	}

	[System.Serializable]
	public struct Options
	{
		public float speed;
		public float magnitude;
		public float magnitude_speed;
		public float lenght_of_arrow;
		public int const_length;
		public bool magicSin;
		public bool isTimeForSin;
		public State_of_Line state;
		public Line arrow;
	}

}
