using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Color_grad : MonoBehaviour {
    public Image image;

    public void setColor(Color col)
    {
        image.color = col;
    }

    public void setColor(int r,int g,int b)
    {
        Color col = new Color(r, g, b);
        image.color = col;
    }

}
