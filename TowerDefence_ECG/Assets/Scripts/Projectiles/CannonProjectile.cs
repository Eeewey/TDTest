using System;
using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour {
	public float m_speed = 0.2f;
	public int m_damage = 10;
	public float destroyTime = 2f;

	private void Start()
	{
		StartCoroutine(DestroyProjectile());
	}
	
	void FixedUpdate () {
		var translation = transform.forward * m_speed * Time.deltaTime;
		transform.Translate (translation, Space.World);
	}

	void OnTriggerEnter(Collider other) {
		var monster = other.gameObject.GetComponent<Monster> ();
		if (monster == null)
			return;

		monster.m_hp -= m_damage;
		if (monster.m_hp <= 0) {
			Destroy (monster.gameObject);
		}

		Destroy (gameObject);
	}

	private IEnumerator DestroyProjectile()
	{
		yield return new WaitForSeconds (destroyTime);
		
		Destroy (gameObject);
	}
}
