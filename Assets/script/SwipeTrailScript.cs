using UnityEngine;
using System.Collections;

public class SwipeTrailScript : MonoBehaviour {

	// Use this for initialization
	public GameObject trailPrefab;
	GameObject thisTrail;
	public GameObject objCam;
	Vector3 startPos;
	Plane objPlane;

	void Start()
	{
//		objPlane = new Plane(Camera.main.transform.forward*-1, this.transform.position);
	}


	void Update () 
	{
	//	objCam.transform.Translate(0, 0, 1);
//		objPlane.transform.position.z = objCam.transform.position.z;
		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
			Input.GetMouseButtonDown(0))
		{
			objPlane = new Plane(Camera.main.transform.forward*-1, this.transform.position);
			thisTrail = (GameObject) Instantiate(trailPrefab, 
				this.transform.position, 
				Quaternion.identity);
			Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			if (objPlane.Raycast(mRay, out rayDistance))
				startPos = mRay.GetPoint(rayDistance);
		}
		else if(((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
			|| Input.GetMouseButton(0)))
		{

			Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			if (objPlane.Raycast(mRay, out rayDistance))
				thisTrail.transform.position = mRay.GetPoint(rayDistance);
		}
		else if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
			Input.GetMouseButtonUp(0))
		{
			if(Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
				Destroy(thisTrail);
		}
	}
}
