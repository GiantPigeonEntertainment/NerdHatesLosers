using UnityEngine;
using System.Collections;

public class NGController : MonoBehaviour, IFormController {
	
	Animator anim;
	Rigidbody2D rb;
	SpriteRenderer sr;
	MyCharacterController characterController;

	bool isFacingRight = true;
	bool isGrounded = true;
	bool isJumping = false;

	public float speed = 5f;
	public float jumpForce = 700f;
	public GameObject body;
	public GameObject legs;

	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponentInParent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
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
		SwitchToSingleAnimation(false);
	}

	public void Forward(){
		rb.velocity = new Vector2(speed, rb.velocity.y);
		SwitchToSingleAnimation(true);
		if(!isFacingRight)
			Flip();
	}
	
	public void Backward(){
		rb.velocity = new Vector2(-speed, rb.velocity.y);
		SwitchToSingleAnimation(true);
		if(isFacingRight)
			Flip();
	}

	public void Front() {
	}

	public void Up() {
	}

	public void Down(){
	}
	
	public void Jump(){
		if(rb.velocity.y == 0) {
			rb.AddForce(new Vector2(0, jumpForce));
		}
	}
	public void Fire(){}
	public void Special(){}

	void Flip() {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void SwitchToSingleAnimation(bool isSingle) {
		sr.enabled = !isSingle;
		body.SetActive(isSingle);
		legs.SetActive(isSingle);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Formbox") {
			Destroy(coll.gameObject);
		characterController.ChangeForm(FormEnum.Spacefighter);
		}
	}
}
