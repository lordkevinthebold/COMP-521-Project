﻿using UnityEngine;
using System.Collections;

public class HumanPlayer : AbstractPlayer
{
	private VisibilityPolygon vision;
	private const float ROT_SPEED = 180f;

	// Use this for initialization
	void Start () {		
		vision = new VisibilityPolygon(this, Environment.Walls);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Handle orientation
		Quaternion rot = Quaternion.Euler (0.0f,0.0f,transform.rotation.eulerAngles.z - Input.GetAxis ("Horizontal") * ROT_SPEED * Time.deltaTime);
		transform.rotation = rot;
		//dont know if we still need this
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit))
		{
			transform.LookAt(hit.point);
		}
			
		// Handle shooting
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Shoot();
		}
		
		// Handle movement
		Vector3 direction = Vector3.zero;
		direction.x = Input.GetAxis ("Vertical") * Time.deltaTime;

		
		Move(direction,rot);
		vision.RecomputePolygon(transform.position);
	}
	
	override protected void Move(Vector3 direction, Quaternion rot)
	{
		transform.position += rot * direction * Environment.Instance.PlayerMaxSpeed;
	}
}
