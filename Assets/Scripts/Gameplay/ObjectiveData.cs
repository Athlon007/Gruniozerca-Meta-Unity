using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveData : MonoBehaviour {

	Animator _anim;
	Rigidbody2D _rb;

	public bool isParsley;
	public float fallSpeed;

	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player") {
			if ((isParsley && PlayerStatistics.instance.isDida) || (!isParsley && !PlayerStatistics.instance.isDida)) {
				PlayerStatistics.instance.Score++;
				Destroy(this.gameObject);
			}
		}
		else {
			// We're considering that other objects is the floor
			PlayerStatistics.instance.Health--;
			Destroy(this.gameObject);
		}
	}
}
