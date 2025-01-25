using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}

// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class Dialogue
// {
//     public string name;

//     [TextArea(3, 10)]
//     public string[] sentences;

//     public DialogueType type; // Defines the type of dialogue (SingleButton or ChoiceButtons)
// }

// public enum DialogueType
// {
//     SingleButton,  // A dialogue box with a single "Continue" button
//     ChoiceButtons  // A dialogue box with three choice buttons
// }
