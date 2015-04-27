using UnityEngine;
using System.Collections;

public class WDController : MonoBehaviour, IFormController {
	
	Animator bodyAnim, legsAnim;
	Rigidbody2D rb;
	MyCharacterController characterController;

	bool isFacingRight = true;
	bool isGrounded = true;
	bool isJumping = false;

	public float speed = 5f;
	public float jumpForce = 700f;
	public GameObject body;
	public GameObject legs;

	void Start () {
		bodyAnim = body.GetComponent<Animator>();
		legsAnim = legs.GetComponent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
		characterController = GetComponentInParent<MyCharacterController>();
	}
	
	void Update () {
		float yVelocity = rb.velocity.y;
		if(yVelocity != 0)
			 isGrounded = false;
		else
			isGrounded = true;

	}

	public void Idle() {
		rb.velocity = new Vector2(0, rb.velocity.y);
		legsAnim.SetBool("IsMoving", false);
	}

	public void Forward(){
		rb.velocity = new Vector2(speed, rb.velocity.y);
		legsAnim.SetBool("IsMoving", true);
		if(!isFacingRight)
			Flip();
	}
	
	public void Backward(){
		rb.velocity = new Vector2(-speed, rb.velocity.y);
		legsAnim.SetBool("IsMoving", true);
		if(isFacingRight)
			Flip();
	}

	public void Front() {
		bodyAnim.SetBool("isLookingUp", false);
		bodyAnim.SetBool("isLookingDown", false);
	}

	public void Up() {
		bodyAnim.SetBool("isLookingUp", true);
	}

	public void Down(){
		bodyAnim.SetBool("isLookingDown", true);
	}
	
	public void Jump(){
		if(rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, jumpForce));
			legsAnim.SetBool("IsMoving", false);
		}
	}
	public void Fire(){
		bodyAnim.SetTrigger("Shoot");
	}
	public void Special(){}

	void Flip() {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Formbox") {
			Destroy(coll.gameObject);
		characterController.ChangeForm(FormEnum.Spacefighter);
		}
	}
}
