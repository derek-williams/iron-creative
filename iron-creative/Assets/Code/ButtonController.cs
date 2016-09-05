using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour 
{
	public void OpenSPCA()
	{
		Application.OpenURL("https://www.sfspca.org/adoptions/dogs");
	}
}
