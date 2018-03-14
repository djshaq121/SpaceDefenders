using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {


	float Health = 100f;

	public float projectileSpeed = 5.0f;
	public float FireRate = 0.5f;
	public GameObject Projectile;

	public int ScoreValue = 125;
	public AudioClip FireSound;
	public AudioClip DestroySound;

	private ScoreKeeper scoreKeeper;

	void Start()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update () {
		float prob = Time.deltaTime * FireRate;
		if(Random.value < prob)
		{
			Shoot ();
		}

	}

	void OnTriggerEnter2D(Collider2D collider)
	{

		Projectile missle = collider.gameObject.GetComponent<Projectile> ();
		if(missle)
		{
			HandleDamage (missle.GetDamage ());
			missle.Hit ();
		}

	}

	void Shoot()
	{
		GameObject Beam = Instantiate(Projectile, transform.position + new Vector3 (0, -1, 0), Quaternion.identity) as GameObject;
		Beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (FireSound, transform.position);
		Destroy (Beam, 3.0f);
	}


	void HandleDamage(float damageAmount)
	{
		Health -= damageAmount;

		if(Health <= 0)
		{
			scoreKeeper.AddScore (ScoreValue);
			AudioSource.PlayClipAtPoint (DestroySound, transform.position);
			Destroy (gameObject);
			return;
		}
			
	}

	void Hit()
	{
		Destroy (gameObject);
	}
}
