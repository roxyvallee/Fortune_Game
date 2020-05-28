using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	// Queue = systeme de FIFO
	private Queue<string> sentences;

	// On va faire une arraylist
	//private Arraylist<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();

		//sentences = new Arraylist<string>();
	}

	// Display the first sentence of the dialogue
	// Ici on peut laisser toujours la même
	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			// Display the first element of the queue
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	// Display the next sentences
	public void DisplayNextSentence ()
	{
		// If there is no more sentences, the dialogue is end
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		// Display the next sentences
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
