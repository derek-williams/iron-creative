using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	[Header("Look Acceleration")]
	public float horizontalSpeed;
	public float verticalSpeed;

	[Header("Animation")]
	public bool havePlayedOpenAnimation;

	[Header("Camera Rotation Limits")]
	public float xMax = .83f;
	public float xMin = .64f;
	public float yMax = .67f;
	public float yMin = -.91f;

	private void Awake()
	{
		Screen.fullScreen = true;
	}

	private void Update() 
	{
			//if using a desktop then camera is controlled by a mouse
			var hDesktop = horizontalSpeed * Input.GetAxis ("Mouse X");
			var vDesktop = verticalSpeed * Input.GetAxis ("Mouse Y");
			var rotateTowards = new Vector3(vDesktop,hDesktop,0f);
			transform.Rotate(RotationCorrection(rotateTowards, vDesktop, hDesktop));
	

			//if using mobile use accelerometer
			var hMobile = Input.acceleration.y * horizontalSpeed;
      var vMobile = Input.acceleration.x * verticalSpeed;
			rotateTowards = new Vector3(vMobile,hMobile,0f);
			transform.Rotate(RotationCorrection(rotateTowards, vMobile, hMobile));

			if(!havePlayedOpenAnimation)
			{
				//listen for input and play animation
			}
	}

	private Vector3 RotationCorrection(Vector3 rotateTowards, float verticalVelocity, float horizontalVelocity)
	{
		//we need to make sure it doesn't get past it's Limits
		if(this.transform.rotation.x < xMax)
		{
			//only rotate toward the  and xMin
			if(verticalVelocity > 0f)
			{
				rotateTowards = new Vector3(0f,horizontalVelocity,0f);
			}
		}
		else if (this.transform.rotation.x > xMin )
		{
			//only rotate toward the y and xMax
			if(verticalVelocity < 0f)
			{
				rotateTowards = new Vector3(0f,horizontalVelocity,0f);
			}
		}

		Debug.LogFormat("{0} is my y rotation and {1} is the max", this.transform.rotation.y, yMax);
		if(this.transform.rotation.y < yMax)
		{
			//only rotate toward the x and the yMin
			if(horizontalVelocity < 0f)
			{
				rotateTowards = new Vector3(verticalVelocity,0f,0f);
			} 
		}
		else if (this.transform.rotation.y > yMin)
		{
			//only rotate toward the x and the yMax
			if(horizontalVelocity > 0f)
			{
				rotateTowards = new Vector3(verticalVelocity,0f,0f);
			} 
		}

		return rotateTowards;
	}
}