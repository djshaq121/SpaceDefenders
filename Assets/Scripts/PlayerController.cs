using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	public float Health = 100;
	public float speed = 5.0f;
	public float padding = 1.0f;
	public float projectileSpeed = 5.0f;
	public float FireRate = 0.5f;

	public AudioClip FireSound;
	public AudioClip PlayerHit;

	public GameObject Projectile;

	float xmin;
	float xmax;
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));

		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Shoot()
	{
		GameObject Beam = Instantiate(Projectile, transform.position + new Vector3 (0, 1.0f, 0), Quaternion.identity) as GameObject;
		Beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (FireSound, transform.position);
		Destroy (Beam, 3.0f);
	}

	// Update is called once per frame

	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{ 
			InvokeRepeating("Shoot",0.000001f,FireRate);

		}

		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke ("Shoot");
		}
		if(Input.GetKey( KeyCode.A))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		 if(Input.GetKey( KeyCode.D))
		{
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//Restricts the player to gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position  = new Vector3(newX,gameObject.transform.position.y,0);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print ("Hit");
		Projectile missle = collider.gameObject.GetComponent<Projectile> ();
		if(missle)
		{
			OnTakeDamage (missle.GetDamage ());
			missle.Hit ();
		}

	}

	void OnTakeDamage(float DamageAmount)
	{
		Health -= DamageAmount;

		if(Health <= 0)
		{
			OnDeath ();
			return;
		}
		if(PlayerHit)
		{
			AudioSource.PlayClipAtPoint (PlayerHit, transform.position);
		}

	}

	void OnDeath()
	{
		Destroy (gameObject);
		GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel("EndMenu");
	}


}
