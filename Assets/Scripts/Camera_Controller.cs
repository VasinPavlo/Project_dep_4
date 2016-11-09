using System.Collections;
using UnityEngine;

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
		print(objects.mainCamera.ScreenToWorldPoint (new Vector3()));
		print(objects.mainCamera.WorldToScreenPoint(objects.mainCamera.ScreenToWorldPoint (new Vector3()))
			+"=="+Input.mousePosition);
		return objects.mainCamera.ScreenToWorldPoint (Input.mousePosition) + getK ();
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
    }
    [System.Serializable]
    public struct OPTIONS
    {
		public float rotation_speed;
		public float zoom_speed;

    }

}
