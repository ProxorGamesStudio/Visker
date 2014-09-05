using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	[HideInInspector]
	public float fource;
	float x,y,z;
	float rx, ry, rz;
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
	public GameObject Explosion;
	// Use this for initialization
	void Start () {
		x = Random.Range(-fource,fource);
		y = Random.Range(-fource,fource);
		z = Random.Range(-fource,fource);
		rx = Random.Range(-fource*Time.deltaTime*100, fource*Time.deltaTime*100);
		ry = Random.Range(-fource*Time.deltaTime*100, fource*Time.deltaTime*100);
		rz = Random.Range(-fource*Time.deltaTime*100, fource*Time.deltaTime*100);
	}
	
	// Update is called once per frame
	void Update () {
	//	distanceView = GameObject.FindGameObjectWithTag("Player").GetComponent<_Controller>().DistanceView;
	//	dst = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
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
		
		
		GetComponent<Rigidbody>().velocity = new Vector3(x, y,z);
		transform.Rotate(rx, ry, rz);
		
		if(hearth <= 0)
		{
			Instantiate(Explosion, transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}
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