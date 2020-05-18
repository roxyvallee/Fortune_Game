using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneGame : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject cardEmpty;
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    
    public static string[] suits = new string[] {"C", "D", "H", "S"};
    public static string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "V", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] tops;
    
    public List<string> deck;
    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] 
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        foreach(string card in deck)
        {
            print(card);
        }

        SolitaireDeal();
        NumberBottom();
    }

    // Création de la pioche avec toutes les cartes
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach(string v in values)
            {
                newDeck.Add(s+v);
            }
        }
        return newDeck;
    }


    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }


    void SolitaireDeal()
    {
        
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = true;
        }
    }

    void NumberBottom()
    {
        float xOffset = 0;
        float yOffset = 0;
        float xposition = -8.0f;
        int random = 17;
        for(int i=0; i < random; i++)
        {
            if( i == 9)
            {
                yOffset = 3.0f;
                xOffset = 0.0f;
            }
            GameObject newEmpty = Instantiate(cardEmpty, new Vector3(xposition + xOffset, -0.1f - yOffset, transform.position.z), Quaternion.identity );
            xOffset += 2.0f; 
        }
    }
}
