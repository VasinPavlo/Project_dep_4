using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main_Canvas : MonoBehaviour {

	// Use this for initialization
	public Objects Obj;

	void Start () 
	{
		setText ("");
	}
	bool isTime_to_update_text=false;
	void Update()
	{
		if (isTime_to_update_text) 
		{
			Obj.text.text = text_for_update;
			isTime_to_update_text = false;
		}
	}
	string text_for_update;

	public void setText(string text)
	{
		if (!isTime_to_update_text) 
		{
			print ("AAAAAAAAAAAAAAAAAAA");
			text_for_update = text;
			isTime_to_update_text = true;
		}
	}

	[System.Serializable]
	public struct Objects
	{
		public Text text;
	}
}
