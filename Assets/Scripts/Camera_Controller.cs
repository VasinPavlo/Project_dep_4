using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Camera_Controller : MonoBehaviour {
    public OBJECTS Obj;
    public SCRIPTS scripts;
    public OPTIONS options;
	// Use this for initialization
    float K;
	void Start ()
    {

        K = pixel_to_real_size(Obj.mainCamera, 1) ;

        print("K:" + K);
	}

    float pixel_to_real_size(Camera camera,float z=0)
    {
        Vector3 current = camera.transform.localPosition;
        camera.transform.localPosition = new Vector3(0,0,-z);
        camera.nearClipPlane = z;
        Vector3 a = new Vector3(0,0,1);
        a = camera.ScreenToWorldPoint(a);
        Vector3 b = new Vector3(camera.pixelWidth,camera.pixelHeight,z);
        b = camera.ScreenToWorldPoint(b);
        camera.transform.localPosition = current;
        return (b - a).magnitude;
    }
	
	// Update is called once per frame
    float tiltAroundZ=0;
	float tiltAroundY=0;
	float tiltAroundX=0;
    int index_of_printscr=0;
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
        if (Input.GetButtonUp("Camera_move"))
        {
            camera_move = !camera_move;
        }
        if (Input.GetButtonUp("PrintScreen"))
        {
            print("printScreen");
            DirectoryInfo dire=new DirectoryInfo("image//PrintScreen//");
            if (!dire.Exists)
                dire.Create();
            Application.CaptureScreenshot(dire.FullName + "//" + "Pr_" +index_of_printscr.ToString("N") + ".png");
            index_of_printscr++;
        }
        if (Input.GetButtonUp("set_Enabled"))
        {
            setEnabled();
        }

	}

	public Vector3 getMousePosition()
	{
		//print (getK ());
        //print(Obj.mainCamera.ScreenToWorldPoint (new Vector3()));
        //print(Obj.mainCamera.WorldToScreenPoint(Obj.mainCamera.ScreenToWorldPoint (new Vector3()))
			//+"=="+Input.mousePosition);
        //print(Obj.mainCamera.ScreenToWorldPoint (Input.mousePosition));
        return Obj.mainCamera.ScreenToWorldPoint (Input.mousePosition+new Vector3(0,0,Mathf.Abs(Obj.mainCamera.transform.localPosition.z)) 
                                                    + getK ());
	}

	public List<Vector3> getVector_of_point()
	{
        return getVector_of_pointCamera();
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

    public List<Vector3> getVector_of_pointCamera()
    {
        List<Vector3> list = new List<Vector3>();
        float h = Obj.mainCamera.pixelHeight;
        float w = Obj.mainCamera.pixelWidth;
        print(h + " " + w);
        float steph = h / (options.m - 1);
        float stepw = w / (options.n - 1);
        steph = stepw;
        Vector3 vector;
        //Vector3 fow = Obj.mainCamera.transform.forward/Obj.mainCamera.transform.forward.magnitude*Mathf.Abs(Obj.mainCamera.transform.localPosition.z);
        //fow = fow / fow.magnitude;
        for (float x = 0; x <= w; x+=stepw)
        {
            for (float y = 0; y <= h; y+=steph)
            {
                vector = new Vector3(x, y, Mathf.Abs(Obj.mainCamera.transform.localPosition.z)) ;
                Vector3 vec = vector;
                vector = Obj.mainCamera.ScreenToWorldPoint(vector);
                //print(vec+"->"+(vector));
                list.Add(vector);
            }
        }
        print(list.Count);
        return list;
    }

    public void inScene()
    {
        Obj.animator.SetBool("inScene", !Obj.animator.GetBool("inScene"));
    }



	void rotation()
	{
        _rotation();
        return;
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

        if (Input.GetButton("Rotation"))
        {
            tiltAroundZ -= Input.GetAxis("Rotation") * options.rotation_speed*Time.deltaTime;
        }
        Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
	}

    bool camera_move=true;
    void _rotation()
    {
        Vector3 vec = new Vector3();
        if (!camera_move)
        {
            if (Input.GetButton("Rotation left and right"))
            {
                //tiltAroundY -= Input.GetAxis("Rotation left and right") * options.rotation_speed*Time.deltaTime;
                //vec = vec + transform.up*Input.GetAxis("Rotation left and right");
                transform.RotateAround(transform.position, transform.up, options.rotation_speed * Time.deltaTime * Input.GetAxis("Rotation left and right"));
            }
            if (Input.GetButton("Rotation up and down"))
            {
                tiltAroundX += Input.GetAxis("Rotation up and down") * options.rotation_speed * Time.deltaTime;
                if (tiltAroundX >= 90)
                    tiltAroundX = 90;
                if (tiltAroundX <= -90)
                    tiltAroundX = -90;
                //vec = vec + transform.right*Input.GetAxis("Rotation up and down");
                transform.RotateAround(transform.position, transform.right, options.rotation_speed * Time.deltaTime * Input.GetAxis("Rotation up and down"));
            }

            if (Input.GetButton("Rotation"))
            {
                //tiltAroundZ -= Input.GetAxis("Rotation") * options.rotation_speed*Time.deltaTime;
                //vec = vec + transform.forward*Input.GetAxis("Rotation");
                transform.RotateAround(transform.position, transform.forward, options.rotation_speed * Time.deltaTime * Input.GetAxis("Rotation"));
            }
        }
        else
        {
            if (Input.GetButtonUp("Rotation left and right"))
            {
                //tiltAroundY -= Input.GetAxis("Rotation left and right") * options.rotation_speed*Time.deltaTime;
                //vec = vec + transform.up*Input.GetAxis("Rotation left and right");
                transform.RotateAround(transform.position, transform.up, options.rotation_speed_proc * Mathf.Sign( Input.GetAxis("Rotation left and right")));
            }
            if (Input.GetButtonUp("Rotation up and down"))
            {
                //tiltAroundX += Input.GetAxis("Rotation up and down") * options.rotation_speed_proc * Time.deltaTime;
                if (tiltAroundX >= 90)
                    tiltAroundX = 90;
                if (tiltAroundX <= -90)
                    tiltAroundX = -90;
                //vec = vec + transform.right*Input.GetAxis("Rotation up and down");
                transform.RotateAround(transform.position, transform.right, options.rotation_speed_proc * Mathf.Sign(Input.GetAxis("Rotation up and down")));
            }

            if (Input.GetButtonUp("Rotation"))
            {
                //tiltAroundZ -= Input.GetAxis("Rotation") * options.rotation_speed*Time.deltaTime;
                //vec = vec + transform.forward*Input.GetAxis("Rotation");
                transform.RotateAround(transform.position, transform.forward, options.rotation_speed_proc * Mathf.Sign( Input.GetAxis("Rotation")));
            }
        }
        //Quaternion target = new Quaternion(vec.x, vec.y, vec.z, options.rotation_speed * Time.deltaTime);//Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);
        //transform.rotation = transform.rotation*target;//Quaternion.Slerp(transform.rotation, target, 1);

    }
    bool ena=true;
    void setEnabled()
    {
        ena = !ena;
        Obj.camera_2.gameObject.SetActive(ena);
    }

	Vector3 getK()
	{
		return Obj.canvas.transform.position - Obj.mainCamera.transform.position;
	}

	void zoom()
	{
		if (Input.GetButton ("Zoom")) 
		{
            //print("zoom");
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
            print("GetMouseButtonDown 0");
			start_position = transform.position;
			first_click = getMousePosition ();
			return;
		}
		if (Input.GetMouseButton (0)) 
		{
			last_click = getMousePosition ();
			transform.position += (first_click-last_click);
            print(first_click+" "+last_click+" "+(first_click-last_click));
		}
	}
	void true_zoom()
	{
		float a=-Input.GetAxis ("Mouse ScrollWheel");
		//Obj.mainCamera.orthographicSize += a * options.true_zoom_speed ;
        //print(a * options.true_zoom_speed);
		Obj.camera_2.orthographicSize += a * options.true_zoom_speed;
		if (false&&Obj.mainCamera.orthographicSize < options.min_true_zoom)
			Obj.camera_2.orthographicSize=Obj.mainCamera.orthographicSize = options.min_true_zoom;
        //
        //float f = Input.GetAxis ("Mouse ScrollWheel") * options.true_zoom_speed_2 * Time.deltaTime;
        float f=-fromOrthographicSize_to_Perspective();
        Obj.mainCamera.transform.localPosition = new Vector3 (Obj.mainCamera.transform.localPosition.x,
            Obj.mainCamera.transform.localPosition.y,
                f);
        Obj.mainCamera.nearClipPlane = -1*Obj.mainCamera.transform.localPosition.z;
        //
        //Obj.mainCamera.scre
        //Obj.mainCamera.si
	}

    float fromOrthographicSize_to_Perspective()
    {
        float len = pixel_to_real_size(Obj.camera_2);
        return len / K;
    }

    string name_of_gif;

    public bool Gif_is_creating;

    public void CreateGif(string file_name,float k_of_speed=1)
    {

        name_of_gif = file_name;
        Gif_is_creating = true;
        speed_mode = k_of_speed;
        StartCoroutine("_CreateGif");
    }

    public void CreateGif(string file_name,Controller.Parameter_for_create_gif param)
    {
        _param = param;
        name_of_gif = file_name;
        Gif_is_creating = true;
        StartCoroutine("_CreateGif2");
    }

    float speed_mode=1;
    Controller.Parameter_for_create_gif _param;
    IEnumerator _CreateGif()
    {
        string file_name = name_of_gif;
        DirectoryInfo dire=new DirectoryInfo("image//Term//"+file_name);
        if (!dire.Exists)
            dire.Create();
        float t = 0;
        float time_step = 1.0f / options.number_of_frame_in_one_sec;
        float d = options.turn_degree;//360.0f / 11*12;
        options.turn_degree=d;
        speed_mode = 2;
        float degree_turn_in_one_frame = options.turn_degree*speed_mode*time_step / (options.number_of_sec);
        //print("degree_turn_in_one_frame="+degree_turn_in_one_frame);
        print("degree_turn_in_one_frame:"+degree_turn_in_one_frame);
        Quaternion start = transform.localRotation;
        int index = 0;
        for (float degree=0 ;degree<options.turn_degree;degree+=degree_turn_in_one_frame,t+=time_step)//(float t = 0; t <= options.number_of_sec; t += time_step)
        {
            index++;
            Application.CaptureScreenshot(dire.FullName + "//" + file_name + "_" +t.ToString("N") + ".png");
            //print(dire.FullName + "//" + file_name + "_" + t.ToString() + ".png");

            yield return new WaitForSeconds(0);
            //print("time:" + t);
            transform.RotateAround(transform.position, new Vector3(0,0,1), degree_turn_in_one_frame*Mathf.Sign(options.turn ));
            if ((degree + degree_turn_in_one_frame >= options.turn_degree && index < options.number_of_frame_in_one_sec))
            {
                options.turn_degree *= 2;
            }
            //break;
        }
        options.turn_degree = d;
        /*/
        print(degree_turn_in_one_frame * Mathf.Sign(options.turn));
        transform.localRotation = start;
        transform.RotateAround(transform.position, new Vector3(0,0,1), options.turn_degree*Mathf.Sign(options.turn ));
        Application.CaptureScreenshot(dire.FullName + "//" + file_name + "_" + t.ToString("N") + ".png");
        print(dire.FullName + "//" + file_name + "_" + t.ToString() + ".png");

        yield return new WaitForSeconds(0);
        /*/
        Gif_is_creating = false;
    }

    IEnumerator _CreateGif2()
    {
        string file_name = name_of_gif;
        DirectoryInfo dire=new DirectoryInfo("image//Term//"+file_name);
        if (!dire.Exists)
            dire.Create();
        float t = 0;
        float time_step = 1.0f / options.number_of_frame_in_one_sec;
        float degree_turn_in_one_frame;// = options.turn_degree*speed_mode*time_step / (options.number_of_sec);
        Quaternion start = transform.localRotation;
        int index = 0;

        float max_turn;
        if (_param.isVer)
        {
            if (_param.k == 1)
            {
                max_turn = 360.0f / _param.n;
            }
            else
            {
                max_turn = 360.0f / _param.k;
            }
        }
        else
        {
            if (_param.k == 1)
            {
                max_turn = 360.0f * _param.n;
            }
            else
            {
                max_turn = 360.0f * _param.k;
            }
            max_turn = 360.0f;

            //if(360.0f/degree_turn_in_one_frame<options.number_of_frame_in_one_sec
        }
        int size = (int)(1.0f / (time_step * _param.speed_mode) / options.number_of_frame_in_one_sec+0.5f);
        while (size == 0)
        {
            _param.speed_mode /= 2;
            max_turn *= 2;
            size = (int)(1.0f / (time_step * _param.speed_mode) / options.number_of_frame_in_one_sec+0.5f);
        }
        degree_turn_in_one_frame = max_turn /(size*options.number_of_frame_in_one_sec);
        options.turn_degree = max_turn*2;

        for (float degree=0 ;degree<options.turn_degree;degree+=degree_turn_in_one_frame,t+=time_step)//(float t = 0; t <= options.number_of_sec; t += time_step)
        {
            index++;
            Application.CaptureScreenshot(dire.FullName + "//" + file_name + "_" +t.ToString("N") + ".png");
            //print(dire.FullName + "//" + file_name + "_" + t.ToString() + ".png");

            yield return new WaitForSeconds(0);
            //print("time:" + t);
            transform.RotateAround(transform.position, new Vector3(0,0,1), degree_turn_in_one_frame*Mathf.Sign(options.turn ));
            /*/
            if ((degree + degree_turn_in_one_frame >= options.turn_degree && index < options.number_of_frame_in_one_sec))
            {
                options.turn_degree *= 2;
            }
            /*/
            //break;
        }
        /*/
        print(degree_turn_in_one_frame * Mathf.Sign(options.turn));
        transform.localRotation = start;
        transform.RotateAround(transform.position, new Vector3(0,0,1), options.turn_degree*Mathf.Sign(options.turn ));
        Application.CaptureScreenshot(dire.FullName + "//" + file_name + "_" + t.ToString("N") + ".png");
        print(dire.FullName + "//" + file_name + "_" + t.ToString() + ".png");

        yield return new WaitForSeconds(0);
        /*/
        Gif_is_creating = false;
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
        public float rotation_speed_proc;
		public float zoom_speed;
		public float true_zoom_speed;
        public float true_zoom_speed_2;
		public float min_true_zoom;
		public int n;
		public int m;
		public Vector2 min;
		public Vector2 max;

        public int number_of_frame_in_one_sec;
        public float number_of_sec;
        public float turn_degree;
        public float turn;
    }

}
