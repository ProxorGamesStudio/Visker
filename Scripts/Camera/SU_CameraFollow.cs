using UnityEngine;
using System.Collections;

public class SU_CameraFollow : MonoBehaviour {
	public enum UpdateMode { FIXED_UPDATE, UPDATE, LATE_UPDATE }
	public UpdateMode updateMode = UpdateMode.FIXED_UPDATE;
	public enum FollowMode { CHASE, SPECTATOR }	
	public FollowMode followMode = FollowMode.SPECTATOR;
	
	// Target to follow
	public Transform target;
		
	public float distance = 20.0f;	
	private float Maxdistance = 25.0f;
	private float Mindistance = 13.0f;
	// Height for chase mode camera
	public float chaseHeight = 5.0f;
	private float MaxHeight = 5.5f;
	private float MinHeight = 4.0f;
	public float ShagDistance;
	public float ShagHeight;
	
	// Follow (movement) damping. Lower value = smoother
	public float followDamping = 0.3f;
	// Look at (rotational) damping. Lower value = smoother
	public float lookAtDamping = 5.0f;	
	// Optional hotkey for freezing camera movement
	public KeyCode freezeKey = KeyCode.None;
		
	private Transform _cacheTransform;
	
	void Start () {
		// Cache reference to transform to increase performance
		_cacheTransform = transform;
	}
	
	void FixedUpdate () {
		if (updateMode == UpdateMode.FIXED_UPDATE) DoCamera();
	}	
	
	
	
	void Update () {//Mycode
		if(distance > Maxdistance)
		distance = Maxdistance;
		if(distance < Mindistance)
		distance = Mindistance;
		if(chaseHeight > MaxHeight)
		chaseHeight = MaxHeight;
		if(chaseHeight < MinHeight)
		chaseHeight = MinHeight;
		
		if(Input.GetKey(KeyCode.LeftShift)&&Input.GetAxis("Mouse ScrollWheel")<0){//прокрутка вверх
			distance+=ShagDistance;
			chaseHeight+=ShagHeight;
		}
		if(Input.GetKey(KeyCode.LeftShift)&&Input.GetAxis("Mouse ScrollWheel")>0){//прокрутка вниз
			distance-=ShagDistance;
			chaseHeight-=ShagHeight;
		}//Mycode
		if(distance > Maxdistance)
		distance = Maxdistance;
		if(distance < Mindistance)
		distance = Mindistance;
		if(chaseHeight > MaxHeight)
		chaseHeight = MaxHeight;
		if(chaseHeight < MinHeight)
		chaseHeight = MinHeight;
		
		
		if (updateMode == UpdateMode.UPDATE) DoCamera();
	}
	
	
	
	void LateUpdate () {
		if (updateMode == UpdateMode.LATE_UPDATE) DoCamera();
	}
	
	void DoCamera () {
		// Return if no target is set
		if (target == null) return;
		
		Quaternion _lookAt;
		
		switch (followMode) {
		case FollowMode.SPECTATOR:
			// Smooth lookat interpolation
			_lookAt = Quaternion.LookRotation(target.position - _cacheTransform.position);
			_cacheTransform.rotation = Quaternion.Lerp(_cacheTransform.rotation, _lookAt, Time.deltaTime * lookAtDamping);
			// Smooth follow interpolation
			if (!Input.GetKey(freezeKey)) {
				if (Vector3.Distance(_cacheTransform.position, target.position) > distance) {					
					_cacheTransform.position = Vector3.Lerp(_cacheTransform.position, target.position, Time.deltaTime * followDamping);
				}
			}
			break;			
		case FollowMode.CHASE:			
			if (!Input.GetKey(freezeKey)) {	
				// Smooth lookat interpolation
				_lookAt = target.rotation;
				_cacheTransform.rotation = Quaternion.Lerp(_cacheTransform.rotation, _lookAt, Time.deltaTime * lookAtDamping);						
				// Smooth follow interpolation
				_cacheTransform.position = Vector3.Lerp(_cacheTransform.position, target.position - target.forward * distance + target.up * chaseHeight, Time.deltaTime * followDamping * 10) ;
			}
			break;
		}						
	}	
}
