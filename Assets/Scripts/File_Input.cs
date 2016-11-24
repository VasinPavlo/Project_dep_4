﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class File_Input : MonoBehaviour {
	private File_Controller Fcon;
	private Animator anim;
	private Text text;
	private FileInfo file;
	void Awake()
	{
		Fcon = GameObject.FindGameObjectWithTag ("File_Controller").GetComponent<File_Controller>();
		anim = GetComponent<Animator> ();
		text = GetComponent<Text> ();
	}
	public void setToCurrentFile(bool b)
	{
		print ("nice:"+b.ToString());
		anim.SetBool ("isActive", b);
		if (b) 
		{
			Fcon.setCurrent (this);
		}
	}
	public void setFile(FileInfo _file)
	{
		if(_file!=null)
		{
			file=_file;
			string name=file.Name;
			name=name.Replace(".txt"," ");
			//print(name);
			text.text=name;
		}
	}
	public bool setName(string name,DirectoryInfo direc)
	{
		name=name.Replace (" ", "_");
		if (file == null) 
		{
			file = new FileInfo (direc.FullName + "\\" + name + ".txt");
			if (!file.Exists) 
			{
				text.text = name;
				return true;
			} 
			return false;
		}
		else
		{
			FileInfo _file=new FileInfo (direc.FullName + "\\" + name + ".txt");
			print("_file:"+_file.FullName);
			if(!_file.Exists)
				file.MoveTo(_file.FullName);
			else
				return false;
			text.text = name;
			return true;
		}
	}
	public void Delete()
	{
		if (file != null&&file.Exists) 
		{
			file.Delete();
		}
	}
	public void Write(List<List<Vector3> > list)
	{
		StringBuilder text=new StringBuilder();
		/*/
		for(int i=0;i<list.Count;i++)
		{
			text.AppendLine(list[i].x+","+list[i].y);
		}
		if (file != null)
		{
			print (file.FullName);
			File.WriteAllText(file.FullName,text.ToString());
		}
		/*/
		for (int i = 0; i < list.Count; i++) 
		{
			for (int j = 0; j < list [i].Count; j++) 
			{
				text.AppendLine (list [i] [j].x + "," + list [i] [j].y+","+ list [i] [j].z);
			}
			text.AppendLine ("end of line");
		}
		if (file != null) 
		{
			File.WriteAllText (file.FullName, text.ToString ());
		}
	}


	public List<List<Vector3> > Read()
	{
		string[] text = File.ReadAllLines (file.FullName);
		string[] numbers;
		String line;
		List<List<Vector3>> list_of_list = new List<List<Vector3>> ();
		List<Vector3> list=null;
		Vector3 vec;
		float x = 0, y = 0, z = 0;
		for(int i=0;i<text.Length;i++)
		{
			line=text[i];
			if (line == "end of line") 
			{
				if (list != null) 
				{
					list_of_list.Add (list);
					list = null;
				}
				continue;
			} 
			else 
			{
				if (list == null)
					list = new List<Vector3> ();
			}
			numbers=line.Split(',');
			if(numbers.Length<3)
				continue;
			/*/
			for(int j=0;j<numbers.Length;j++)
			{
				print ("read:"+i+" "+j+"-"+numbers[j]);
			}
			/*/
			float.TryParse( numbers[0],out x);
			float.TryParse( numbers[1],out y);
			float.TryParse( numbers[2],out z);
			vec=new Vector3(x,y,z);
			list.Add (vec);
			//print ("read:"+vec.ToString());
		}

		return list_of_list;
	}
	public string getName()
	{
		return text.text;
	}

}
