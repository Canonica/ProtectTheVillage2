using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BoneProjectile : MonoBehaviour {
    public float damage;
    public float speed;
    public Skeleton owner;

    public bool hasFinished;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<CharacterEntity>().TakeDamage(damage);
            owner.StartCoroutine("Knockback", 0.2f);
            Destroy(this.gameObject);
        }
        else if (other.tag == "SwordCol")
        {
            Destroy(this.gameObject);
        }
    }

    public void DoAnimation(Vector3 startPosition, Vector3 targetPosition)
    {
        owner.canMove = false;
        transform.DOMove(targetPosition, Vector3.Distance(startPosition, targetPosition) / speed)
            .OnComplete(() => hasFinished = true);
    }

    void Update()
    {
		if (owner == null)
		{
			Destroy (this.gameObject);
		}
        if (hasFinished)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, owner.transform.position, step);
            if (Vector3.Distance(this.transform.position, owner.transform.position) < 0.3f)
            {
                End();
            }
        }
    }

    void End()
    {
        owner.canMove = true;
        Destroy(this.gameObject);
    }
}
