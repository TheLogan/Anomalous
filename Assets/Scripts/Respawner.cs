using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	public void Die(SpaceShip ship)
	{
		StartCoroutine(Dier(ship));
	}

	private IEnumerator Dier(SpaceShip ship)
	{
		ship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		ship.gameObject.SetActive(false);
		//spawn explosion
		//Empty inventory
		yield return new WaitForSeconds(3);
		ship.transform.position = transform.position - (Vector3.up * 5);
		yield return new WaitForSeconds(2);
		ship.gameObject.SetActive(true);
		ship.Respawn();
	}
}
