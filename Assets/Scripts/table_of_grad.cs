using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class table_of_grad : MonoBehaviour {

    public GridLayoutGroup glGroup;
    RectTransform rect;
    public GameObject clone_of_grad;
    List<Color_grad> list_of_grad;

    public Color teststart;
    public Color testend;

    const float maxH = 1082f;
	void Start () 
    {
        //list_of_grad = new List<Color_grad>();
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
    public void create_table(int n,Color start,Color end)
    {
        if (list_of_grad==null)
            list_of_grad = new List<Color_grad>();
        Vector3 _start = fromColortoVector(start);
        Vector3 _end = fromColortoVector(end);
        Vector3 iter = _end;
        Vector3 step = -(_end - _start) / (n - 1);
        for (int i = 0; i < n; i++)
        {
            //print(iter);
            Color_grad grad = MonoBehaviour.Instantiate(clone_of_grad).GetComponent<Color_grad>();
            grad.transform.SetParent(transform);
            grad.transform.localScale = new Vector3(1, 1, 1);
            grad.setColor(fromVectortoColor(iter));
            list_of_grad.Add(grad);
            iter += step;
        }

        glGroup.cellSize = new Vector2(glGroup.cellSize.x, maxH / n);

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
    }
	void Update () 
    {
	
	}
}
