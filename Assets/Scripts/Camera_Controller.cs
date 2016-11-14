using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Camera_Controller : MonoBehaviour {
    public OBJECTS objects;
    public SCRIPTS scripts;
    public OPTIONS options;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	float tiltAroundY=0;
	float tiltAroundX=0;
	void Update ()
    {
		//tiltAroundZ = Input.GetAxis("Rotation left and right") * tiltAngle;
		//tiltAroundX = Input.GetAxis("Rotation up and down") * tiltAngle;
		rotation();
		zoom ();
	}

	public Vector3 getMousePosition()
	{
		//print (getK ());
		//print(objects.mainCamera.ScreenToWorldPoint (new Vector3()));
		//print(objects.mainCamera.WorldToScreenPoint(objects.mainCamera.ScreenToWorldPoint (new Vector3()))
		//	+"=="+Input.mousePosition);
		return objects.mainCamera.ScreenToWorldPoint (Input.mousePosition) + getK ();
	}

	public List<Vector3> getVector_of_point()
	{
		List<Vector3> list=new List<Vector3>();
		float h = objects.rt_canvas.offsetMax.y-objects.rt_canvas.offsetMin.y;
		float w = objects.rt_canvas.offsetMax.x-objects.rt_canvas.offsetMin.x;
		float steph = h/(options.m-1);
		float stepw = w / (options.n - 1);
		Vector3 vec;
		for (float x = objects.rt_canvas.offsetMin.x; x <= objects.rt_canvas.offsetMax.x; x+=stepw) {
			for (float y = objects.rt_canvas.offsetMin.y; y <= objects.rt_canvas.offsetMax.y; y+=steph) {
				vec = new Vector3 (x, y, 0);
				vec = objects.canvas.transform.TransformPoint (vec);
				list.Add (vec);
			}
		}
		//print (list.Count);
		return list;
	}

	void rotation()
	{
		if (Input.GetButton("Rotation left and right"))
		{
			tiltAroundY += Input.GetAxis("Rotation left and right") * options.rotation_speed*Time.deltaTime;
		}
		if (Input.GetButton("Rotation up and down"))
		{
			tiltAroundX+= Input.GetAxis("Rotation up and down") * options.rotation_speed*Time.deltaTime;
			if (tiltAroundX >= 90)
				tiltAroundX = 90;
			if (tiltAroundX <= -90)
				tiltAroundX = -90;
		}
		Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
	}

	Vector3 getK()
	{
		return objects.canvas.transform.position - objects.mainCamera.transform.position;
	}

	void zoom()
	{
		if (Input.GetButton ("Zoom")) 
		{
			float f = Input.GetAxis ("Zoom") * options.zoom_speed * Time.deltaTime;
			objects.canvas.transform.localPosition = new Vector3 (objects.canvas.transform.localPosition.x,
				objects.canvas.transform.localPosition.y,
				objects.canvas.transform.localPosition.z + f);
		}
	}


    //===============struct
    [System.Serializable]
    public struct SCRIPTS
    {
        
    }
    [System.Serializable]
    public struct OBJECTS
    {
        public Camera mainCamera;
		public GameObject canvas;
		public RectTransform rt_canvas;
	}
    [System.Serializable]
    public struct OPTIONS
    {
		public float rotation_speed;
		public float zoom_speed;
		public int n;
		public int m;
		public Vector2 min;
		public Vector2 max;
    }

}
