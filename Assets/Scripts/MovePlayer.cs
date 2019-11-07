using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
	[SerializeField]
	GameObject ball;
	public float speed;
	Rigidbody2D rb;
	[SerializeField]
	float xBound;
	Vector2 movement;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		movement.y = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x > xBound)
		{
			transform.position = new Vector2(xBound, transform.position.y);
		}
		if (transform.position.x < -xBound)
		{
			transform.position = new Vector2(-xBound, transform.position.y);
		} 
		
		movement.x = Input.GetAxisRaw("Horizontal");

		if (Input.GetKeyUp(KeyCode.D))
		{
			rb.velocity = Vector2.zero;
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			rb.velocity = Vector2.zero;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Joint2D>().enabled = false;
			ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(30, 50));
		}
	}

	private void FixedUpdate() 
	{
		rb.MovePosition(rb.position + speed * movement * Time.fixedDeltaTime);
	}
}
	