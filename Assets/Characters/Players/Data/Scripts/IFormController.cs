using UnityEngine;
using System.Collections;

public interface IFormController {

	void Idle();
	void Forward();
	void Backward();

	void Front();
	void Up();
	void Down();
	
	void Jump();
	void Fire();
	void Special();
}
