using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerLogin : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		Debug.Log("This is a debug message");

		FindObjectOfType<DialogueManagerLogin>().StartDialogue(dialogue);
	}

}
