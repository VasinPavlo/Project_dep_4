using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class table_of_grad : MonoBehaviour {

    public GridLayoutGroup glGroup;
    public RectTransform rect;
    //RectTransform rect;
    public GameObject clone_of_grad;
    public element_of_speed_table clone_of_element_of_speed ;
    List<Color_grad> list_of_grad;

    public Color teststart;
    public Color testend;

    float maxH = 1082f;
    //public GridLayoutGroup glGroup;
    List<element_of_speed_table> list;

    public Color start;
    public Color end;

    int _n=1;

	void Start () 
    {
        //list_of_grad = new List<Color_grad>();
	}
	
	// Update is called once per frame
    public void create_table(int n,Color start,Color end,float v1,float v2)
    {
        if (list_of_grad==null)
            list_of_grad = new List<Color_grad>();
        if (list==null)
            list = new List<element_of_speed_table>();
        des();
        Vector3 _start = fromColortoVector(start);
        Vector3 _end = fromColortoVector(end);
        Vector3 iter_color = _end;
        Vector3 step_color = -(_end - _start) / (n - 1);
        float iter_speed = v2;
        float step_speed = -(v2 - v1) / (n - 1);
        for (int i = 0; i < n; i++)
        {
            //print(iter);
            Color_grad grad = MonoBehaviour.Instantiate(clone_of_grad).GetComponent<Color_grad>();
            grad.transform.SetParent(transform);
            grad.transform.localScale = new Vector3(1, 1, 1);
            grad.setColor(fromVectortoColor(iter_color));
            list_of_grad.Add(grad);
            iter_color += step_color;


            element_of_speed_table elem = MonoBehaviour.Instantiate(clone_of_element_of_speed).GetComponent<element_of_speed_table>();
            elem.transform.SetParent(transform);
            elem.transform.localScale = new Vector3(1, 1, 1);
            list.Add(elem);
            elem.setNumber(iter_speed);
            iter_speed += step_speed;
        }
        if (rect == null)
        {
            //rect = GetComponent<RectTransform>();
        }
        _n = n;

    }

    public void create_table(int n, float v1, float v2)
    {
        if (list==null)
            list = new List<element_of_speed_table>();
        des();
        float cellsizeY = glGroup.cellSize.y;
        int spacing_size = (int)(maxH / n - cellsizeY);
        glGroup.padding.top = spacing_size;
        glGroup.spacing = new Vector2(glGroup.spacing.x,spacing_size);
        float iter_speed = v2;
        float step_speed = -(v2 - v1) / (n - 1);
        for (int i = 0; i < n; i++)
        {
            element_of_speed_table elem = MonoBehaviour.Instantiate(clone_of_element_of_speed).GetComponent<element_of_speed_table>();
            elem.transform.SetParent(transform);
            elem.transform.localScale = new Vector3(1, 1, 1);
            list.Add(elem);
            elem.setNumber(iter_speed);
            iter_speed += step_speed;
        }

    }

    Vector3 fromColortoVector(Color col)
    {
        return new Vector3(col.r, col.g, col.b);
    }

    Color fromVectortoColor(Vector3 vec)
    {
        return new Color(vec.x, vec.y, vec.z);
    }

    public void des()
    {
        foreach (Color_grad grad in list_of_grad)
        {
            MonoBehaviour.Destroy(grad.gameObject);
        }
        list_of_grad.Clear();
        foreach (element_of_speed_table elem in list)
        {
            MonoBehaviour.Destroy(elem.gameObject);
        }
        list.Clear();
    }
	void Update () 
    {
        maxH = rect.rect.height;
        //print(maxH);
        if (_n == 0)
            _n = 1;
        glGroup.cellSize = new Vector2(glGroup.cellSize.x, maxH / _n);
	}
}
