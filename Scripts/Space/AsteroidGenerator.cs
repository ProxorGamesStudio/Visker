using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour {
	public GameObject Astariod;
	public int count;
	public float RadiosSpawn;
	int i;
	GameObject obj;
	public float Fource;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(i < count)
		{
			obj = Instantiate(Astariod, new Vector3(Random.Range(-RadiosSpawn, RadiosSpawn), Random.Range(-RadiosSpawn, RadiosSpawn),Random.Range(-RadiosSpawn, RadiosSpawn)) , Quaternion.identity) as GameObject;
			obj.AddComponent<Rigidbody>().useGravity = false;
			obj.GetComponent<Asteroid>().fource = Fource;
			i++;
		}
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, RadiosSpawn);
	}

}
