using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
public class _Controller : MonoBehaviour {
	public float speed;

	void Update()
	{
		transform.GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
	}

}