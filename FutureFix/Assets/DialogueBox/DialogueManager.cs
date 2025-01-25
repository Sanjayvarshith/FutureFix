using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{

		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class DialogueManager : MonoBehaviour
// {
//     public Text nameText;
//     public Text dialogueText;
//     public Animator animator;

//     public GameObject singleButtonBox; // Dialogue box with "Continue"
//     public GameObject choiceBox;       // Dialogue box with 3 buttons

//     public Button continueButton; // Continue button for single-button dialogues

//     private Queue<string> sentences;
//     private DialogueType currentDialogueType; // Track current dialogue type

//     void Start()
//     {
//         sentences = new Queue<string>();
//     }

//     public void StartDialogue(Dialogue dialogue)
//     {
//         animator.SetBool("IsOpen", true);
//         nameText.text = dialogue.name;

//         sentences.Clear();

//         foreach (string sentence in dialogue.sentences)
//         {
//             sentences.Enqueue(sentence);
//         }

//         currentDialogueType = dialogue.type; // Store dialogue type

//         DisplayNextSentence();
//     }

//     public void DisplayNextSentence()
//     {
//         if (sentences.Count == 0)
//         {
//             EndDialogue();
//             return;
//         }

//         string sentence = sentences.Dequeue();
//         StopAllCoroutines();
//         StartCoroutine(TypeSentence(sentence));

//         // Switch UI based on dialogue type
//         if (currentDialogueType == DialogueType.SingleButton)
//         {
//             singleButtonBox.SetActive(true);
//             choiceBox.SetActive(false);
//         }
//         else if (currentDialogueType == DialogueType.ChoiceButtons)
//         {
//             singleButtonBox.SetActive(false);
//             choiceBox.SetActive(true);
//         }
//     }

//     IEnumerator TypeSentence(string sentence)
//     {
//         dialogueText.text = "";
//         foreach (char letter in sentence.ToCharArray())
//         {
//             dialogueText.text += letter;
//             yield return null;
//         }
//     }

//     void EndDialogue()
//     {
//         animator.SetBool("IsOpen", false);
//         singleButtonBox.SetActive(false);
//         choiceBox.SetActive(true);
//     }
// }
