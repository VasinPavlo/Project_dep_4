using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Online_Options : MonoBehaviour {
    public Option options;
    public Obj obj;

    public void create_table(int n,Color s,Color e,float v1,float v2)
    {
        obj.grad.create_table(n, s, e);
        obj.speed.create_table(n, v1, v2);
        options.minV = v1;
        options.maxV = v2;
        obj.minV.text = v1.ToString();
        obj.maxV.text = v2.ToString();
    }

    public void setMinV(string v)
    {
        float.TryParse(v,out options.minV);
        Refresh();
    }

    public void setMaxV(string v)
    {
        float.TryParse(v,out options.maxV);
        print(options.maxV);
        Refresh();
    }

    public void Refresh()
    {
    }

    [System.Serializable]
    public struct Option
    {
        public bool everytime_refresh_color;
        public bool everytime_refresh_vector;
        public float minV;
        public float maxV;
    }

    [System.Serializable]
    public struct Obj
    {
        public table_of_grad grad;
        public table_of_speed speed;
        public InputField minV;
        public InputField maxV;
        public Controller cont;
    }

}
