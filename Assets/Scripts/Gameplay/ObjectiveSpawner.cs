using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawner : MonoBehaviour {

	public float fallSpeed;

	public GameObject prefab;
	GameObject _currentObject;

	public static ObjectiveSpawner instance;

	void Awake() {
		instance = this;
	}
	
	void Update () {
		// Checks if vegetable exists
		// If not - generates a new one
		if (_currentObject == null) {
			// Generate vegetable
			_currentObject = GameObject.Instantiate(prefab);
			BoxCollider2D collider = _currentObject.AddComponent<BoxCollider2D>();
			Rigidbody2D rb = _currentObject.AddComponent<Rigidbody2D>();
			Animator anim = _currentObject.GetComponent<Animator>();

			float x = Random.Range(-5, 6);
			_currentObject.transform.position = new Vector2(x, 3.5f);
			bool isParsley = Random.Range(0, 11) > 5 ? true : false;
			anim.SetBool("Parsley", isParsley);		
			
			rb.gravityScale = 0;
			collider.size = _currentObject.GetComponent<SpriteRenderer>().size;
			collider.isTrigger = true;
			ObjectiveData objectiveData = _currentObject.AddComponent<ObjectiveData>();
			rb.AddForce(new Vector2(0, fallSpeed * -1));
			objectiveData.isParsley = isParsley;
		}
	}
}
