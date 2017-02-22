using UnityEngine;
using System.Collections;

public class Arrow : Line 
{
    public void setColor(Color begin,Color end)
    {
        SetColors (begin, end);
        if(options.arrow)
            options.arrow.SetColors(begin, end);
    }

    public void setColor(float r,float g,float b,float a)
    {
        Color col = new Color(r, g, b, a);
        SetColors(col,col);
        //line_renderer.SetColors(col, col);
        if (options.arrow)
        {
            options.arrow.SetColors(col,col);
            //options.arrow.line_renderer.SetColors(col, col);
        }
    }

    public Vector4 toVector(Color col)
    {
        Vector4 vec = new Vector4(col.r,col.g,col.b,col.a);
        return vec;
    }

    public void setColorV(float V,Color start,Color end,float maxV)
    {
        //print("Hello");
        Vector4 _start = toVector(start);
        Vector4 _end = toVector(end);
        Vector4 vec = _end - _start;
        V=(V>=maxV?maxV:V);
        vec = _start+vec * V / maxV;
        //print(vec);
        setColor(vec.x, vec.y, vec.z, vec.w);
    }
}
