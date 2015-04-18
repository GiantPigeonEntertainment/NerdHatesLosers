using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {
	
	public FormEnum formState;

	public GameObject nerdgard;
	public GameObject spacefighter;

	IFormController characterController;
	
	void Start () {
		ChangeForm (formState);
	}
	
	void Update () {

		// HORIZONTAL
		float x = Input.GetAxis("Horizontal");
		if(x > 0)
			characterController.Forward();
		else if(x < 0)
			characterController.Backward();
		else
			characterController.Idle();

		// VERTICAL
		float y = Input.GetAxis("Vertical");
		if(y > 0)
			characterController.Up();
		else if(y < 0)
			characterController.Down();
		else
			characterController.Front();

		// JUMP
		if(Input.GetButtonDown("Jump"))
			characterController.Jump();
		
		// Fire
		if(Input.GetButtonDown("Fire"))
			characterController.Fire();
	}

	public void ChangeForm(FormEnum form) {
		EnableForm(form);
		GetForm(form);
	}

	void EnableForm(FormEnum form) {
		//disable all
		nerdgard.SetActive(false);
		spacefighter.SetActive(false);

		//enable selected form
		switch(form) {
		case FormEnum.Nerdgard: nerdgard.SetActive(true); break;
		case FormEnum.Spacefighter: spacefighter.SetActive(true); break;
		default: nerdgard.SetActive(true); break;
		}
	}

	void GetForm(FormEnum form){
		IFormController formController;
		switch(form) {
			case FormEnum.Nerdgard: formController = nerdgard.GetComponent<NerdgardController> (); break;
			case FormEnum.Spacefighter: formController = spacefighter.GetComponent<SpacefighterController> (); break;
			default: formController = nerdgard.GetComponent<NerdgardController> (); break;
		}
		characterController = formController;
	}
}
