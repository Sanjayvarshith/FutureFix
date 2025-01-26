using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerLogin : MonoBehaviour {

	public GameObject player;
	public GameObject ambulance;
	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	public Animator animatorfail;
	public Animator animatorsuccess;

	public GameObject canvas;
	 public Camera miniMapCamera; 
    public Camera fullScreenMapCamera;

	public GameObject test;
	public GameObject Garbage;

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

	public void newDialogue2 ()
	{
		animator.SetBool("IsOpen", true);
		animatorsuccess.SetBool("IsOpen", false);
		animatorfail.SetBool("IsOpen", false);
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
		Vector3 playerPosition = player.transform.position;
		animator.SetBool("IsOpen", false);
		 if (playerPosition.x < -0.552626 && playerPosition.x > -10.552626 && playerPosition.z > 23.85155 && playerPosition.z < 33.85155)
        {
            if (miniMapCamera.gameObject.activeSelf)
            {
                // Switch to fullscreen map
                miniMapCamera.gameObject.SetActive(false);
                fullScreenMapCamera.gameObject.SetActive(true);
				canvas.SetActive(false);
				return;
            }
            else
            {
                // Switch back to minimap
                fullScreenMapCamera.gameObject.SetActive(false);
                miniMapCamera.gameObject.SetActive(true);
            }
        }
		if(playerPosition.x < 59.29271 && playerPosition.x > 40.29271 && playerPosition.z < -0.060452  && playerPosition.z >-6.960452 )
		{
			//    GameObject ambulance = GameObject.Find("Ambulance_no_damage");
			ambulance.gameObject.SetActive(true);
		// dialogueBoxsuccess.SetActive(true);
		animatorsuccess.SetBool("IsOpen", true);
	}
	 if (playerPosition.x < -45 && playerPosition.x > -51 && playerPosition.z < -110 && playerPosition.z > -120)
            {
                Garbage.SetActive(false);
            }
	animatorsuccess.SetBool("IsOpen", true);
	// dialogueBoxsuccess.SetActive(true);
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

