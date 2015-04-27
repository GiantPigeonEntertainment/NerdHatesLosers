using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {
	
	public FormEnum formState;

	public GameObject ng;
	public GameObject sf;
	public GameObject wd;

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
		ng.SetActive(false);
		sf.SetActive(false);
		wd.SetActive(false);

		//enable selected form
		switch(form) {
		case FormEnum.Nerdgard: ng.SetActive(true); break;
		case FormEnum.Spacefighter: sf.SetActive(true); break;
		case FormEnum.WarriorDwarf: wd.SetActive(true); break;
		default: ng.SetActive(true); break;
		}
	}

	void GetForm(FormEnum form){
		IFormController formController;
		switch(form) {
			case FormEnum.Nerdgard: formController = ng.GetComponent<NGController> (); break;
			case FormEnum.Spacefighter: formController = sf.GetComponent<SFController> (); break;
			case FormEnum.WarriorDwarf: formController = wd.GetComponent<WDController> (); break;
			default: formController = ng.GetComponent<NGController> (); break;
		}
		characterController = formController;
	}
}
