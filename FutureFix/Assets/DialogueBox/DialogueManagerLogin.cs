using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerLogin : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	public Animator animatorfail;
	public Animator animatorsuccess;

	public GameObject canvas;

	public GameObject test;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}
	public Dialogue dialogue2;

	public void StartDialogue (Dialogue dialogue)
	{

		animator.SetBool("IsOpen", true);
		test.SetActive(false);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void newDialogue ()
	{
		animator.SetBool("IsOpen", true);
		test.SetActive(false);

		nameText.text = dialogue2.name;

		sentences.Clear();

		foreach (string sentence in dialogue2.sentences)
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

	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		// dialogueBox2.transform.position = new Vector3(0, 1000, 0);
	}

	public void EndDialogueFail()
	{
		animator.SetBool("IsOpen", false);
		// dialogueBoxfail.SetActive(true);
		animatorfail.SetBool("IsOpen", true);
	}

	public void EndDialogueSuccess()
	{
		animator.SetBool("IsOpen", false);
		// dialogueBoxsuccess.SetActive(true);
		animatorsuccess.SetBool("IsOpen", true);
	}

	public void Fail()
	{
		animatorfail.SetBool("IsOpen", false);
		animatorsuccess.SetBool("IsOpen", false);
		test.SetActive(true);
	}

	public void Success()
	{
		animatorfail.SetBool("IsOpen", false);
		animatorsuccess.SetBool("IsOpen", false);
		canvas.SetActive(false);
	}

}
