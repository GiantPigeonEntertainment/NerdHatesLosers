using UnityEngine;
using System.Collections;

public class CameraParallax : MonoBehaviour {
	public Transform[] backgrounds;      // List of all the back and foreground to be parallaxed
	private float[] parallaxScales;       // The proportion of the camera's movement to move the backround by
	public float smoothing = 1f;         // How smooth the parallax is going to be. Make sure to set this above 0
	
	private Transform cam;               // Reference to the main cameras transform
	private Vector3 previousCamPos;      // the position of the camera in the previous frame
	
	//Awake = Called before start
	void Awake (){
		//set up the camerarefrence
		cam = Camera.main.transform;
		
	}
	
	
	// Use this for initialization
	void Start () {
		//the previous frame had the current frames camera position
		previousCamPos = cam.position;
		
		// assingning corespongding parallaxScales
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++){
			parallaxScales[i] = backgrounds[i].position.z*-1f;
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// for each background
		for (int i = 0; i < backgrounds.Length; i++){
			// the parallax is the opposite of the cameramovement because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
			
			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			
			// creaye a target position which is the background's current position with it's target x position 
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y,backgrounds[i].position.z);
			
			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		
		// set the previousCamPos to the cameras position at the end of the frame
		previousCamPos = cam.position;
	}
}