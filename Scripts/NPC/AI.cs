using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class AI : MonoBehaviour {
	public float speed, rotationSpeed , RadiosGo, DistPodxod;
	bool attack;
	Vector3 pointGo, dir;
	Transform player;
	public Transform Stancia, Oblivion;
	float angle;
	public GameObject Bullet;
	public Transform Dulo;
	public float uron;
	float tim = 0.1f;
	bool go;
	public Texture2D Normal, Active;
	Texture2D main;
	Vector2 MP;
	Rect rect;
	public int width, height;
	[HideInInspector]
	public bool select;
	GameObject[] astrs, enemys;
	public float hearth;
	[HideInInspector]
	public float distanceView;
	float dst;
	public GameObject Exp;

	// Use this for initialization
	void Start () {
		if(Random.Range(0,100) > 50)
			pointGo = Stancia.position;
		if(Random.Range(0,100) < 50)
			pointGo = Oblivion.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(hearth <= 0)
		{
			Instantiate(Exp, transform.position, transform.rotation);
			Destroy(gameObject);
		}

		//distanceView = GameObject.FindGameObjectWithTag("Player").GetComponent<_Controller>().DistanceView;
		dst = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
		astrs = GameObject.FindGameObjectsWithTag("Meteor");
		enemys = GameObject.FindGameObjectsWithTag("Enemy");
		MP = Input.mousePosition;
		MP.y = Screen.height - Input.mousePosition.y;
		rect = new Rect(0,0,width, height);
		rect.center = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Screen.height-Camera.main.WorldToScreenPoint(transform.position).y);
		Vector3 forward = GameObject.FindGameObjectWithTag("Player").transform.TransformDirection(Vector3.forward);
		Vector3 toOther =  transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
		if(rect.Contains(MP) && Vector3.Dot(forward, toOther) > 0 && dst <= distanceView)
		{
			if(Input.GetMouseButtonDown(0))
			{
				foreach(GameObject obj in astrs)
				{
					obj.GetComponent<Asteroid>().select = false;
				}
				foreach(GameObject obj in enemys)
				{
					obj.GetComponent<AI>().select = false;
				}
			//	GameObject.FindGameObjectWithTag("Player").GetComponent<_Controller>().target = transform;
				select = true;
			}
		}

		player = GameObject.FindGameObjectWithTag("Player").transform;
		angle = Vector3.Angle(transform.forward, player.position - transform.position);
	

		if(!attack)
		dir = transform.position - pointGo;
		else
			dir = transform.position - player.position;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (-dir), rotationSpeed * Time.deltaTime);
		if(!attack)
		GetComponent<Rigidbody>().velocity = (-dir.normalized * speed);
		if(attack && Vector3.Distance(transform.position, player.position) > DistPodxod)
			GetComponent<Rigidbody>().velocity = (-dir.normalized * speed);
		if(Vector3.Distance(transform.position, player.position) <= DistPodxod)
			GetComponent<Rigidbody>().velocity = (dir*0);
		if(Vector3.Distance(transform.position, pointGo) <= 20)
			pointGo = new Vector3(Random.Range(-RadiosGo, RadiosGo), Random.Range(-RadiosGo, RadiosGo), Random.Range(-RadiosGo, RadiosGo));

		if(Vector3.Distance(transform.position, player.position) <= RadiosGo)
		{
			if(angle <= 30 && !go)
			{
				GameObject ob;
				ob = Instantiate(Bullet, Dulo.transform.position, Quaternion.identity) as GameObject;
				ob.GetComponent<Rocket>().target = player;
				ob.GetComponent<Rocket>().uron = uron;
				go = true;
			}
			if(go)
			{
				tim -=Time.deltaTime;
				if(tim <= 0)
				{
					tim = 0.1f;
					go = false;
				}
			}
			attack = true;
		}
		else
			attack = false;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, RadiosGo);
	}

	void OnGUI()
	{
		Vector3 forward = GameObject.FindGameObjectWithTag("Player").transform.TransformDirection(Vector3.forward);
		Vector3 toOther =  transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
		if (Vector3.Dot(forward, toOther) > 0 && dst <= distanceView)
		{
			GUI.DrawTexture(rect, main);
			if(select)
				main = Active;
			else
				main = Normal;
		}
	}
}
