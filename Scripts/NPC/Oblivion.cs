using UnityEngine;
using System.Collections;

public class Oblivion : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * speed * Time.deltaTime);
	}
}
