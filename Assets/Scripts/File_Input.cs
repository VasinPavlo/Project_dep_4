using UnityEngine;
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
    private static DirectoryInfo directory= new DirectoryInfo ("Assets//docs//move");
	void Awake()
	{
		Fcon = GameObject.FindGameObjectWithTag ("File_Controller").GetComponent<File_Controller>();
		anim = GetComponent<Animator> ();
		text = GetComponent<Text> ();
        if (!directory.Exists)
        {
            directory.Create();
        }
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

    public void setFile(string _name)
    {
        string name=_name.Replace(".txt"," ");
        if (name != "")
        {
            FileInfo[] files = directory.GetFiles(name+".txt");
            if (files.Length == 0)
            {
                file = new FileInfo(directory.FullName + "\\" + name + ".txt");
            }
            else
                file = directory.GetFiles(_name)[0];
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
                if(text!=null)
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
        if (text.Length == 0)
            return list_of_list;
        int i = 0;
		for(;i<text.Length;i++)
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

    StreamReader stream;
    public List<Vector3> ReadLine()
    {
        if(stream==null||stream.EndOfStream)
            stream= file.OpenText();
        List<Vector3> list = new List<Vector3>();
        if (stream.EndOfStream)
        {
            //stream.Close();
            _end_of_stream = true;
            return list;
        }
        else
        {
            _end_of_stream = false;
        }
        
        String line;

        string[] numbers;
        Vector3 vec;

        float x = 0, y = 0, z = 0;
        int i = 0;
        while(!stream.EndOfStream)
        {
            line  = stream.ReadLine();
            if (line == "end of line") 
            {
                break;
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
        if (stream.EndOfStream)
        {
            //stream.Close();
            _end_of_stream = true;
        }
        return list;
    }

    bool _end_of_stream;

    public bool EndOfStream
    {
        get
        {
            return _end_of_stream;
        }
    }

	public string getName()
	{
		return text.text;
	}

}
