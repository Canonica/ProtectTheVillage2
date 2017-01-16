using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
	[SerializeField]
	float speed;

    GameObject target;
	float damages;

	public void setTarget(GameObject value, float dvalue)
	{
		target = value;
		damages = dvalue;
	}

	void Update()
	{
		transform.position = Vector3.Lerp (transform.position, target.transform.position, speed * Time.deltaTime);
		if (Vector3.Distance (transform.position, target.transform.position) <= 0.1f)
		{
			if (target.GetComponent<CharacterEntity> () != null)
			{
				target.GetComponent<CharacterEntity> ().TakeDamage (damages);
			}
		}
	}
}