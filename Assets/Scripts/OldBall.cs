using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OldBall : MonoBehaviour
{
	[SerializeField]
	GameObject powerUp;
	[SerializeField]
	Collider2D left;
	[SerializeField]
	Collider2D right;
	Rigidbody2D rb;
	[SerializeField]
	Transform startPoint;
	[SerializeField]
	MovePlayer player;
	WaitForSeconds wait = new WaitForSeconds(0.5f);
	[SerializeField]
	LifeSupply lifeManager;
	[SerializeField]
	GameObject gameOverScreen;
	private void Start() 
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Block"))
		{
			Destroy(other.gameObject);
			int powerUpThreshold = Random.Range(0, 10);
			if (powerUpThreshold < 2)
			{
				Instantiate(powerUp, other.transform.position, Quaternion.identity);
			}
		}

		if (other.collider == left)
		{
			if (rb.velocity.x > 0)
			{
				rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
			}
			if (rb.velocity.x < 0)
			{
				rb.AddForce(new Vector2( -5, 0));
			}
		}
		if (other.collider == right)
		{
			if (rb.velocity.x > 0)
			{
				rb.AddForce(new Vector2( 5, 0));
			}
			if (rb.velocity.x < 0)
			{
				rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Catcher"))
		{
			if(lifeManager.lifes > 0){
				StartCoroutine(Died());
				lifeManager.RemoveLife();
			} 
			else
			{
				gameOverScreen.SetActive(true);
			}
		}
	}

	IEnumerator Died()
	{
		yield return wait;
		rb.velocity = Vector2.zero;
		player.GetComponent<Joint2D>().enabled = true;
		transform.position = startPoint.position;
	}
}
