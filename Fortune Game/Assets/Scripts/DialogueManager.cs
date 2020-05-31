using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public GameObject DialogueBox;

	public Animator animator;

	// On va faire une list
	private List<string> sentences;

	private int cpt;

	private int sizeTabBinomiale = 10;
	private float[] tabBinomiale;
	private float parameter = 0.1f;

	private int[] indexBinomial;
	// Use this for initialization
	void Start () 
	{
		
		sentences = new List<string>();
	}

	public void SetParameter()
    {
		/*
        int level = FindObjectOfType<Level>().ReturnLevel();
        if(level == 1)
        {
            parameter = 0.8f;
        }
        if(level == 2)
        {
            parameter = 0.5f;
        }
        if(level == 3)
        {
            parameter = 0.2f;
        }*/
	
		parameter = 1.0f - FindObjectOfType<TimeImage>().ReturnParameter();
        print("parameter : " + parameter);
    }
    
	public static float Factoriel(float n) 
    {
    	return n > 1?n * Factoriel(n-1):1;
    }

    public static float Combination(int n, int k)
    {
        return Factoriel(n)/ (Factoriel(k)*Factoriel(n-k));
    }

	private float binomiale(float p, int n, int k){
		float nk = Combination(n, k);
		return nk * Mathf.Pow(p, k) * Mathf.Pow((1 - p), n - k);
	}

	private void loiBinomiale(){
		tabBinomiale = new float[sizeTabBinomiale];
		for(int i=0; i < sizeTabBinomiale ; i++){
			tabBinomiale[i] = binomiale(parameter, sizeTabBinomiale, i);
		}
		orderTable();
	}

	private void printTable()
	{
		foreach(float var in tabBinomiale)
		{
			print("tab : " + var);
		}
	}

	private void indexTab()
	{
		indexBinomial = new int[sizeTabBinomiale];
		for(int i = 0; i < sizeTabBinomiale; i ++)
		{
			indexBinomial[i] = i;
		}
	}

	private void orderTable()
	{
		indexTab();
		for(int i = 0; i < sizeTabBinomiale; i++)
		{
			for(int j = 0; j < sizeTabBinomiale; j++)
			if(tabBinomiale[i] < tabBinomiale[j])
			{
				// changer la position de l'index
				int index = indexBinomial[i];
				indexBinomial[i] = indexBinomial[j];
				indexBinomial[j] = index;

				// changer la position de la proba
				float tmp = tabBinomiale[i];
				tabBinomiale[i] = tabBinomiale[j];
				tabBinomiale[j] = tmp;
				j--;
			}
		}

		//printTable();
	}
	private int returnIndexBinomiale(){
		float rand = Random.Range(0.000000000f, 1.000000000f);
		//print("rand : " + rand);
		for(int i=0; i < sizeTabBinomiale ; i++){
			if(rand <= tabBinomiale[i]){
				return indexBinomial[i];
			}
		}
		return -1;
	}
	

	public void StartDialogue (Dialogue dialogueGood, Dialogue dialogueBad)
	{
		animator.SetBool("IsOpen", true);
		DialogueBox.SetActive(true);
		SetParameter();

		nameText.text = dialogueGood.name;

		sentences.Clear();

		loiBinomiale();
		int goodSentence = -1;

		while(goodSentence < 0){
			goodSentence = returnIndexBinomiale();
			print("goodsentence : " + goodSentence);
		}

		int badSentence = sizeTabBinomiale - goodSentence;

		for (int i = 0; i < goodSentence; i++)
        {
            sentences.Add(dialogueGood.sentences[i]);
        }

        for (int i = 0; i < badSentence; i++)
        {
            sentences.Add(dialogueBad.sentences[i]);
        }

		cpt = sentences.Count;
		DisplayNextSentence();
	}

	// Display the next sentences
	public void DisplayNextSentence ()
	{
		// If there is no more sentences, the dialogue is end
		if (cpt == 0)
		{
			EndDialogue();
			return;
		}

		// Display the next sentences
		//int rand = Random.Range(0, sentences.Count);
		int index = 0;
		foreach(string value in sentences){
			if(index == cpt){
				string sentence = value;
				StopAllCoroutines();
				StartCoroutine(TypeSentence(sentence));
			}
			index++;
		}
		cpt--;
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
		DialogueBox.SetActive(false);
	}
}
