using UnityEngine;
using System.Collections;

public class AzarashiController : MonoBehaviour {

	Rigidbody2D rb2D;
	Animator animator;
	float angle;
	bool isDead;

	public float maxHeight;
	public float flapVelocity;
	public float relativeVelocityX;
	public GameObject sprite; 

	void Awake(){

		rb2D = GetComponent<Rigidbody2D> ();
		animator = sprite.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( Input.GetButtonDown("Fire1") && transform.position.y < maxHeight){
			Flap ();
		}
		ApplyAngle ();
		animator.SetBool ("flap", angle >= 0.0f);
	}

	public void Flap(){
		
		if(isDead){
			return;
		}

		if (rb2D.isKinematic)
			return;
		
		rb2D.velocity = new Vector2 (0.0f, flapVelocity);
	}

	void ApplyAngle(){

		float targetAngle;

		if(isDead){
			
			targetAngle = -90.0f;

		}else{
			
			targetAngle = Mathf.Atan2 (rb2D.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
		}
	
		angle = Mathf.Lerp (angle, targetAngle, Time.deltaTime * 10.0f);

		//rotation
		sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);

	}

	public bool IsDead(){

		return isDead;
	}

	void OnCollisionEnter2D( Collision2D collision){

		if(isDead){
			return;
		}
		isDead = true;
	}

	public void SetSteerActive(bool active){

		rb2D.isKinematic = !active;
	}
}
