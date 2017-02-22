using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Camera_Controller : MonoBehaviour {
    public OBJECTS Obj;
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
		move ();
		true_zoom ();

        if (Input.GetButtonUp("Panel_in_scene"))
        {
            inScene();
        }
	}

	public Vector3 getMousePosition()
	{
		//print (getK ());
		//print(objects.mainCamera.ScreenToWorldPoint (new Vector3()));
		//print(objects.mainCamera.WorldToScreenPoint(objects.mainCamera.ScreenToWorldPoint (new Vector3()))
		//	+"=="+Input.mousePosition);
		return Obj.mainCamera.ScreenToWorldPoint (Input.mousePosition) + getK ();
	}

	public List<Vector3> getVector_of_point()
	{
		List<Vector3> list=new List<Vector3>();
		float h = Obj.rt_canvas.offsetMax.y-Obj.rt_canvas.offsetMin.y;
		float w = Obj.rt_canvas.offsetMax.x-Obj.rt_canvas.offsetMin.x;
		float steph = h/(options.m-1);
		float stepw = w / (options.n - 1);
        steph = stepw;
		Vector3 vec;
		for (float x = Obj.rt_canvas.offsetMin.x; x <= Obj.rt_canvas.offsetMax.x; x+=stepw) {
			for (float y = Obj.rt_canvas.offsetMin.y; y <= Obj.rt_canvas.offsetMax.y; y+=steph) {
				vec = new Vector3 (x, y, 0);
				vec = Obj.canvas.transform.TransformPoint (vec);
				list.Add (vec);
			}
		}
		//print (list.Count);
		return list;
	}

    public void inScene()
    {
        Obj.animator.SetBool("inScene", !Obj.animator.GetBool("inScene"));
    }

	void rotation()
	{
		if (Input.GetButton("Rotation left and right"))
		{
			tiltAroundY -= Input.GetAxis("Rotation left and right") * options.rotation_speed*Time.deltaTime;
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
		return Obj.canvas.transform.position - Obj.mainCamera.transform.position;
	}

	void zoom()
	{
		if (Input.GetButton ("Zoom")) 
		{
			float f = Input.GetAxis ("Zoom") * options.zoom_speed * Time.deltaTime;
			Obj.canvas.transform.localPosition = new Vector3 (Obj.canvas.transform.localPosition.x,
				Obj.canvas.transform.localPosition.y,
				Obj.canvas.transform.localPosition.z + f);
		}
	}
	Vector3 start_position,last_click,first_click;
    public Vector3 getProjection(Vector3 V)
    {
        return Obj.mainCamera.ScreenToWorldPoint(Obj.mainCamera.WorldToScreenPoint(V)) - Obj.mainCamera.ScreenToWorldPoint(Obj.mainCamera.WorldToScreenPoint(new Vector3()));
    }

    public Vector3 normal()
    {
        return Obj.mainCamera.transform.forward;
    }
	void move()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			start_position = transform.position;
			first_click = getMousePosition ();
			return;
		}
		if (Input.GetMouseButton (0)) 
		{
			last_click = getMousePosition ();
			transform.position += (first_click-last_click);
		}
	}
	void true_zoom()
	{
		float a=-Input.GetAxis ("Mouse ScrollWheel");
		Obj.mainCamera.orthographicSize += a * options.true_zoom_speed ;
		Obj.camera_2.orthographicSize += a * options.true_zoom_speed;
		if (Obj.mainCamera.orthographicSize < options.min_true_zoom)
			Obj.camera_2.orthographicSize=Obj.mainCamera.orthographicSize = options.min_true_zoom;
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
		public Camera camera_2;
		public GameObject canvas;
		public RectTransform rt_canvas;
        public Animator animator;
	}
    [System.Serializable]
    public struct OPTIONS
    {
		public float rotation_speed;
		public float zoom_speed;
		public float true_zoom_speed;
		public float min_true_zoom;
		public int n;
		public int m;
		public Vector2 min;
		public Vector2 max;
    }

}
