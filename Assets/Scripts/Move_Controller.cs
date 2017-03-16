using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Move_Controller : MonoBehaviour {

	// Use this for initialization
    public DirectoryInfo direc=new DirectoryInfo("Assets//docs//Move");
    public Controller cont;
    public Algorightm algo;
    public float TimeStep=0.1f;
    public float time = 10;
    public float minTimeStepForPlay=0.1f;
    public int AlgoIndex=1;
    float dtime;
    public bool play_after_create = false;
    public bool isPlay;
    public bool isCreate;
    List<int> list_of_lenght;

    private List<Line> lines;

	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public float current_time
    {
        get
        {
            return t;
        }
    }

    float t;

    List<List<Vector3> > moves;
    string name_of_file;
    public void StartCreateMoves(string file_name)
    {
        name_of_file = file_name;
        lines = cont.getVector_of_Line();
        t=0;
        moves = new List<List<Vector3>>();
        StartCoroutine("_StartCreateMoves");
    }

    List<Vector3> plus(List<Vector3> a,List<Vector3> b)
    {
        for (int i = 0; i < a.Count; i++)
        {
            a[i] = a[i] + b[i];
        }
        return a;
    }

    List<List<Vector3> > plus(List<List<Vector3> > x,List<Vector3>  v, float dt)
    {
        int j = 0;
        int k = 0;
        for (int i = 0; i < v.Count; )
        {
            if (k >= x[j].Count)
            {
                k = 0;
                j++;
                if (j >= x.Count)
                    return x;
            }
            Vector3 _x=x[j][k];
            x[j][k] = move(x[j][k], v[i], dt);
            //print(_x + "+"+v[i]+"=" + x[j][k]);
            i++;k++;
           
        }
        return x;
    }

    Vector3 move(Vector3 x,Vector3 v, float dt)
    {
        return x + v * dt;
    }

    IEnumerator _StartCreateMoves()
    {
        isCreate = true;
        List<List<Vector3> > vortex_line_1 = cont.getList_of_lines();
        List<List<Vector3> > vortex_line_2;
        List<Vector3> points1 = List2_to_List(vortex_line_1);
        List<Vector3> points2;
        List<Vector3> v= new List<Vector3>();
        List<Vector3> k1 ;
        List<Vector3> k2 ;
        List<Vector3> k3 ;
        List<Vector3> k4 ;

        findLenght(vortex_line_1);
        moves.Add(float_to_Vector3(getParam()));
        moves.Add(points1);
        for (; t <= time; t += TimeStep)
        {
            //break;
            print("time:"+t);
            //v=FindV(vortex_line, points);
            //=========================================================Algo=============================
            switch (AlgoIndex)
            {
                case 1:
                    for (int i = 0; i < vortex_line_1.Count; i++)
                    {
                        algo.FindVector_of_speed(vortex_line_1[i], points1);
                        while (algo.isWork)
                        {
                            yield return new WaitForSeconds(0);
                        }
                        if (i == 0)
                            v = algo.lists_of_speed;
                        else
                            v=plus(v,algo.lists_of_speed);
                    }
                    vortex_line_1=plus(vortex_line_1, v, TimeStep);
                    break;
                case 2:
                    for (int i = 0; i < vortex_line_1.Count; i++)
                    {
                        algo.FindVector_of_speed(vortex_line_1[i], points1);
                        while (algo.isWork)
                        {
                            yield return new WaitForSeconds(0);
                        }
                        if (i == 0)
                            v = algo.lists_of_speed;
                        else
                            v=plus(v,algo.lists_of_speed);
                    }
                    k1 = v;

                    vortex_line_2=plus(vortex_line_1, k1, TimeStep/2);
                    points2 = List2_to_List(vortex_line_2);
                    for (int i = 0; i < vortex_line_2.Count; i++)
                    {
                        algo.FindVector_of_speed(vortex_line_2[i], points2);
                        while (algo.isWork)
                        {
                            yield return new WaitForSeconds(0);
                        }
                        if (i == 0)
                            v = algo.lists_of_speed;
                        else
                            v=plus(v,algo.lists_of_speed);
                    }
                    k2 = v;

                    vortex_line_2=plus(vortex_line_1, k2, TimeStep/2);
                    points2 = List2_to_List(vortex_line_2);
                    for (int i = 0; i < vortex_line_2.Count; i++)
                    {
                        algo.FindVector_of_speed(vortex_line_2[i], points2);
                        while (algo.isWork)
                        {
                            yield return new WaitForSeconds(0);
                        }
                        //print("================V=============");
                        for(int k=0;i<v.Count;i++)
                        {
                            break;
                            print(v[i]);
                        }
                        if (i == 0)
                            v = algo.lists_of_speed;
                        else
                            v=plus(v,algo.lists_of_speed);
                    }
                    k3 = v;

                    vortex_line_2=plus(vortex_line_1, k3, TimeStep);
                    points2 = List2_to_List(vortex_line_2);
                    for (int i = 0; i < vortex_line_2.Count; i++)
                    {
                        algo.FindVector_of_speed(vortex_line_2[i], points2);
                        while (algo.isWork)
                        {
                            yield return new WaitForSeconds(0);
                        }
                        //print("================V=============");
                        for(int k=0;i<v.Count;i++)
                        {
                            break;
                            print(v[i]);
                        }
                        if (i == 0)
                            v = algo.lists_of_speed;
                        else
                            v=plus(v,algo.lists_of_speed);
                    }
                    k4 = v;

                    vortex_line_1 = plus(vortex_line_1, k1, TimeStep / 6);
                    vortex_line_1 = plus(vortex_line_1, k2, 2*TimeStep / 6);
                    vortex_line_1 = plus(vortex_line_1, k3, 2*TimeStep / 6);
                    vortex_line_1 = plus(vortex_line_1, k4, TimeStep / 6);
                    break;
            }

            //==========================================================================================
            //print("===============Plus===============");
            points1 = List2_to_List(vortex_line_1);
           // print("before="+points[0]);
            //print("after=" + points[0]);
            moves.Add(points1);
        }
        print("time to save");
        File_Input file = getFile(name_of_file);
        file.Write(moves);
        isCreate = false;
        yield return new WaitForSeconds(0);
        if(play_after_create)
            StartPlayMove(name_of_file);

    }

    List<Vector3> FindV(List<List<Vector3> > vortex_line,List<Vector3> points)
    {
        List<Vector3> v=new List<Vector3>();
        for (int i = 0; i < vortex_line.Count; i++)
        {
            algo.FindVector_of_speed(vortex_line[i], points);
            while (algo.isWork)
            {
                //Wait(0);
                //yield return new WaitForSeconds(0);
            }
            //print("================V=============");
            if (i == 0)
                v = algo.lists_of_speed;
            else
                v=plus(v,algo.lists_of_speed);
            for(int k=0;i<v.Count;i++)
            {
                break;
                print(v[i]);
            }
        }
        return v;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void StartPlayMove(string name)
    {
        name_of_file = name;
        isPlay = true;
        StartCoroutine("_StartPlayMove");
    }

    IEnumerator _StartPlayMove()
    {
        File_Input file = getFile(name_of_file);
        List<List<Vector3> > moves = file.Read(); 
        if (moves.Count == 0)
        {
            print("File is Empty");
        }
        else
        {
            setParam(Vector3_to_float(moves[0]));
            List<List<Vector3> > state = List_to_List2(moves[1], list_of_lenght);
            cont.Clear_list_of_lines();
            for (int j = 0; j < state.Count; j++)
            {
                cont.addLines(state[j]);
            }

            List<Line> Lines = cont.getList_of_Lines();
            t = 0;
            float _dt=0;
            for (int i = 2; i < moves.Count; i++)
            {
                yield return new WaitForSeconds(dtime);
                t += dtime;
                _dt += dtime;
                if (_dt < minTimeStepForPlay)
                    continue;
                _dt = 0;
                print("play time:" + t);
                while (!isPlay)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                state = List_to_List2(moves[i], list_of_lenght);
                for (int j = 0; j < state.Count; j++)
                {
                    Lines[j].addFunctionPoints(state[j]);
                }
            }
        }
        isPlay = false;
        yield return new WaitForSeconds(0);
    }



    List<float> Vector3_to_float(List<Vector3> list)
    {
        List<float> res = new List<float>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].x == -1)
                return res;
            res.Add(list[i].x);
            if (list[i].y == -1)
                return res;
            res.Add(list[i].y);
            if (list[i].z == -1)
                return res;
            res.Add(list[i].z);
        }
        return res;
    }

    List<Vector3> float_to_Vector3(List<float> list)
    {
        List<Vector3> res = new List<Vector3>();
        int i = -1;
        Vector3 vec;
        while (true)
        {
            vec = new Vector3(-1, -1, -1);
            if (++i >= list.Count)
            {
                break;
            }
            vec.x = list[i];
            if (++i >= list.Count)
            {
                res.Add(vec);
                break;
            }
            vec.y=list[i];
            if (++i >= list.Count)
            {
                res.Add(vec);
                break;
            }
            vec.z=list[i];
            res.Add(vec);
        }
        for(i=0;i<res.Count;i++)
        {
            print("float_to_Vector3 "+res[i]);
        }
        return res;
    }

    List<Vector3> List2_to_List(List<List<Vector3> > list)
    {
        List<Vector3> res = new List<Vector3>();
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Count; j++)
            {
                res.Add(list[i][j]);
            }
        }
        return res;
    }

    List<List<Vector3> > List_to_List2(List<Vector3> list,List<int> lenghts)
    {
        List<List<Vector3> > res=new List<List<Vector3>>();
        List<Vector3> a=new List<Vector3>();
        int i = 0;
        int j = 0;
        int I = 0;
        while (true)
        {
            if (i >= lenghts.Count)
            {
                break;
            }
            if (j >= lenghts[i])
            {
                j = 0;
                i++;
                res.Add(a);
            }
            else
            {
                if (j == 0)
                {
                    a = new List<Vector3>();
                }
                a.Add(list[I]);
                j++;
                I++;
            }
                
        }
        return res;
    }

    void findLenght(List<List<Vector3> > list)
    {
        list_of_lenght = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            list_of_lenght.Add(list[i].Count);
        }
    }

    void setParam(List<float> list)
    {
        dtime = list[0];
        int n = (int)list[1];
        list_of_lenght = new List<int>();
        for (int i = 2; i < n + 2; i++)
        {
            list_of_lenght.Add((int)list[i]);
        }
    }

    List<float> getParam()
    {
        List<float> list = new List<float>();
        list.Add(TimeStep);
        list.Add(list_of_lenght.Count);
        for (int i = 0; i < list_of_lenght.Count; i++)
        {
            list.Add(list_of_lenght[i]);
        }
        for (int i = 0; i < list.Count; i++)
        {
            print("getParam "+list[i]);
        }
        return list;
    }

    File_Input getFile(string name)
    {
        File_Input file = new File_Input();
        file.setName(name, direc);
        return file;
    }
}
