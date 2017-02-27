using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class table_of_speed : MonoBehaviour {

    public element_of_speed_table clone_of_element_of_speed ;
    public GridLayoutGroup glGroup;
    List<element_of_speed_table> list;

    public Color start;
    public Color end;

    const float maxH = 1082f;

    public void Start()
    {
        //list = new List<element_of_speed_table>();

    }

    public void create_table(int n, float v1, float v2)
    {
        if (list==null)
            list = new List<element_of_speed_table>();
        float cellsizeY = glGroup.cellSize.y;
        int spacing_size = (int)(maxH / n - cellsizeY);
        glGroup.padding.top = spacing_size;
        glGroup.spacing = new Vector2(glGroup.spacing.x,spacing_size);
        float iter = v2;
        float step = -(v2 - v1) / (n - 1);
        for (int i = 0; i < n; i++)
        {
            element_of_speed_table elem = MonoBehaviour.Instantiate(clone_of_element_of_speed).GetComponent<element_of_speed_table>();
            elem.transform.SetParent(transform);
            elem.transform.localScale = new Vector3(1, 1, 1);
            list.Add(elem);
            elem.setNumber(iter);
            iter += step;
        }

    }

    public void des()
    {
        foreach (element_of_speed_table elem in list)
        {
            MonoBehaviour.Destroy(elem.gameObject);
        }
        list.Clear();
    }
}
