using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue ()
	{
		Debug.Log("This is a debug message");

		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}
