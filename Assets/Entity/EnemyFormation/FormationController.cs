using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefeab;
	public float width = 10;
	public float height = 5;
	public float speed = 5;
	public float SpawnDelay = 0.5f;

	private bool moveRight = true; 

	float xmin;
	float xmax;
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));


		xmin = leftmost.x;
		xmax = rightmost.x;
		SpawnUntilFull ();
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height, 0));
	}

	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform) 
		{
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}

		}
		return null;
	}

	void SpawnEnemies()
	{

		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefeab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child.transform;
		}
	}

	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition ();
		if(freePosition)
		{
			GameObject enemy = Instantiate (enemyPrefeab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if(NextFreePosition())
		{
			Invoke ("SpawnUntilFull", SpawnDelay);
		}
	}
	// Update is called once per frame
	void Update () {

		if (moveRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		if (leftEdgeOfFormation < xmin) {
			moveRight = true;
		} else if(rightEdgeOfFormation > xmax){
			moveRight = false;
		}


		float x = Mathf.Clamp(transform.position.x, xmin, xmax); 
		transform.position = new Vector3 (x, gameObject.transform.position.y, 0);


		if(AllMembersDead())
		{
			SpawnUntilFull ();
			print("All dead");
		}
	}

	bool AllMembersDead()
	{
		foreach (Transform childPositionGameObject in transform) 
		{
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
				
		}
		return true;
	}
}
