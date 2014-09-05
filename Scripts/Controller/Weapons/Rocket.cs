using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	public Transform target;
	Vector3 dir, tr;
	public float speed, uron;
	public float timerDestroy;
	public GameObject Explosion;
	public float distanceExp;
	float dist;
	public bool bullet;
	
	// Use this for initialization
	void Start () {
		tr = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
		{
			dir = transform.position - target.position;
			dist = Vector3.Distance(transform.position, target.position);
		}
		else
			dist = 0;
		transform.Translate(-dir.normalized*speed);
		
		if(dist <= distanceExp)
		{
			if(target.tag == "Meteor")
			target.gameObject.GetComponent<Asteroid>().hearth -= uron;
			if(target.tag == "Player")
		//	target.gameObject.GetComponent<_Controller>().life -= uron;
			if(target.tag == "Player")
		//		target.gameObject.GetComponent<_Controller>().life -= uron;
			if(target.tag == "Enemy")
				target.gameObject.GetComponent<AI>().hearth -= uron;
			if(!bullet)
				Instantiate(Explosion, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
		
		timerDestroy -= Time.deltaTime;
		if(timerDestroy <= 0 || !target.gameObject.activeSelf)
		{
			if(!bullet)
				Instantiate(Explosion, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
	}
}