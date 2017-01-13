using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour
{

	public peterlavalle.InputProperty forward = new peterlavalle.InputProperty();
	public peterlavalle.InputProperty strafe = new peterlavalle.InputProperty();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		var position = transform.position;
		
		position.x += strafe.Axis * Time.deltaTime;
		position.z += forward.Axis * Time.deltaTime;

		transform.position = position;
	}
}
