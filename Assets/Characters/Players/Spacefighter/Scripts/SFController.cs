using UnityEngine;
using System.Collections;

public class SFController : MonoBehaviour, IFormController {
	
	Animator anim;
	Animator legs;
	Rigidbody2D rb;
	SpriteRenderer sr;
	MyCharacterController characterController;
	
	bool isFacingRight = true;
	bool isGrounded = true;
	bool isJumping = false;

	bool isMoving = false;
	bool isCrouched = false;
	bool isLookingUp = false;

	public float normalSpeed = 5f;
	float speed;
	public float crouchedSpeed = 2f;
	public float jumpForce = 700f;
	
	void Start () {
		anim = GetComponent<Animator>();
		legs = transform.FindChild ("Legs").gameObject.GetComponent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		characterController = GetComponentInParent<MyCharacterController>();

		speed = normalSpeed;
	}
	
	void Update () {
		float yVelocity = rb.velocity.y;
		if(yVelocity != 0)
			isGrounded = false;
		else
			isGrounded = true;
	}

	// ASSE X
	public void Idle() {
		rb.velocity = new Vector2(0, rb.velocity.y);
		isMoving = false;
		anim.SetBool ("isMoving", isMoving);
		legs.SetBool ("isMoving", isMoving);
	}
	
	public void Forward(){
		rb.velocity = new Vector2(speed, rb.velocity.y);
		isMoving = true;
		anim.SetBool ("isMoving", isMoving);
		legs.SetBool ("isMoving", isMoving);
		if(!isFacingRight)
			Flip();
	}
	
	public void Backward(){
		rb.velocity = new Vector2(-speed, rb.velocity.y);
		isMoving = true;
		anim.SetBool ("isMoving", isMoving);
		legs.SetBool ("isMoving", isMoving);
		if(isFacingRight)
			Flip();
	}

	// ASSE Y
	public void Front() {
		isLookingUp = false;
		anim.SetBool ("isLookingUp", isLookingUp);
		if (isCrouched) {
			isCrouched = false;
			speed = normalSpeed;
			anim.SetBool("isCrouched", isCrouched);
			legs.SetBool("isCrouched", isCrouched);
		}
	}
	
	public void Up() {
		isCrouched = false;
		speed = normalSpeed;
		anim.SetBool("isCrouched", isCrouched);
		legs.SetBool("isCrouched", isCrouched);
		isLookingUp = true;
		anim.SetBool ("isLookingUp", isLookingUp);
	}
	
	public void Down(){
		if (isGrounded) {
			isCrouched = true;
			speed = crouchedSpeed;
			anim.SetBool("isCrouched", isCrouched);
			legs.SetBool("isCrouched", isCrouched);
		}
	}

	// BUTTONS
	public void Jump(){
		if(rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, jumpForce));
		}
	}
	public void Fire(){
		Debug.Log ("shoot");
		anim.SetTrigger ("shoot");
	}
	public void Special(){}


	// FUNCTIONS
	void Flip() {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
