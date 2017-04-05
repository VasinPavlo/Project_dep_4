using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class element_of_speed_table : MonoBehaviour {

    public Text text;

    public void setNumber(float num)
    {
        string t = num.ToString("###0.#");
        text.text = t;
    }
}
