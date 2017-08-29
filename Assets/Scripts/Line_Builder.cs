using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line_Builder : MonoBehaviour 
{

    public List<Vector3> getSquare(Vector3 a,Vector3 b,Vector3 c, Vector3 d)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(d);
        //list.Add(a);
        return list;
    }

    public List<Vector3> getTriangle(Vector3 a,Vector3 b,Vector3 c)
    {
        List<Vector3> list=new List<Vector3>();
        list.Add(a);
        list.Add(b);
        list.Add(c);
        list.Add(a);
        return list;
    }

    public List<Vector3> getEllipse(Vector3 pos, float a, float b)
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 vec;
        for(float x=0;x<2*Mathf.PI;x+=0.1f)//2*Mathf.PI/40)
        {
            vec = new Vector3(a * Mathf.Cos(x) + pos.x, b * Mathf.Sin(x) + pos.y, pos.z);
            list.Add(vec);
        }
        vec = new Vector3(a * Mathf.Cos(2*Mathf.PI) + pos.x, b * Mathf.Sin(2*Mathf.PI) + pos.y, pos.z);
        list.Add(vec);
        return list;
    }

    public List<Vector3> getToreV(Vector3 pos,float start_gamma, float R,float r,float stepR,float stepr)
    {
        Vector3 point;
        List<Vector3> list=new List<Vector3>();
        float end_gamma = start_gamma + 2 * Mathf.PI;
        for(float gamma=start_gamma, alpha=0;gamma<end_gamma||alpha<2*Mathf.PI;gamma+=stepR,alpha+=stepr)
        {
            point = new Vector3((R + r * Mathf.Cos(alpha)) * Mathf.Cos(gamma), (R + r * Mathf.Cos(alpha)) * Mathf.Sin(gamma), r * Mathf.Sin(alpha));
            list.Add(point);
        }
        point=new Vector3((R + r * Mathf.Cos(0)) * Mathf.Cos(start_gamma), (R + r * Mathf.Cos(0)) * Mathf.Sin(start_gamma), r * Mathf.Sin(0));
        list.Add(point);
        return list;
    }

    public List<Vector3> getBall(Vector3 pos,float R,float stepR,float stepr)
    {
        Vector3 point;
        List<Vector3> list = new List<Vector3>();
        for (float gamma = 0, alpha = 0; gamma < 2*Mathf.PI||alpha<2*Mathf.PI; gamma += stepR,alpha += stepr)
        {
            point = new Vector3(R * Mathf.Cos(alpha) * Mathf.Sin(gamma), R * Mathf.Sin(alpha) * Mathf.Sin(gamma), R * Mathf.Cos(gamma));
            list.Add(point);
        }
        point = new Vector3(R * Mathf.Cos(0) * Mathf.Sin(0), R * Mathf.Sin(0) * Mathf.Sin(0), R * Mathf.Cos(0));
        list.Add(point);
        return list;

    }

}
