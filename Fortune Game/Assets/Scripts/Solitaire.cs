using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    public GameObject deckButton;
    
    public static string[] suits = new string[] {"C", "D", "H", "S"};
    public static string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] tops;
    public List<string> tripsOnDisplay = new List<string>(); // trois cartes du deck affichées 
    public List<List<string>> deckTrips = new List<List<string>>(); // liste des listes de cartes affichées du deck

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();    
    private List<string> bottom2 = new List<string>();    
    private List<string> bottom3 = new List<string>();    
    private List<string> bottom4 = new List<string>();    
    private List<string> bottom5 = new List<string>();    
    private List<string> bottom6 = new List<string>();        
    public List<string> deck;
    public List<string> discardPile = new List<string>();
    private int deckLocation; // où on se trouve dans le deck
    private int trips;
    private int tripsRemainder;
    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] {bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6}; 
        print("begin party");
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayCards()
    {
        foreach(List<string> list in bottoms)
        {
            list.Clear();
        }

        deck = GenerateDeck();
        Shuffle(deck);
        print("test");
        /*
        foreach(string card in deck)
        {
            print(card);
        }*/

        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        SortDeckIntoTrips();
        //NumberBottom();
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


    void Shuffle<T>(List<T> list) // create random here
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while(n > 1)
        {
            // rajouter ici probabilité pour suit et value
            // probabilité de suit
            // probabilité de value
            // forme une carte dont on recherche la value dans deck
            // retourne l'index de la carte choisie
            // si la carte n'est pas trouvée ( = -1) on recommence 
            // 0 --------------X-------- 1 rand = 0.6
            // 0 -7-- As-X-2-------------1 rand = X
            // 1 = noir
            // 0 = rouge
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
    /*
    public static int FindIndex(List<T> list, string card)
    {
        foreach(string s in deck)
        {
            // si on trouve la carte
            if()
        }
        return -1;
        // si on trouve pas la carte on doit recommencer
    }
    */

    void SolitaireSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = i; j < 7; j++ )
            {
                bottoms[j].Add(deck.Last <string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    IEnumerator SolitaireDeal()
    {
        for(int i = 0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 0.03f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.05f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().row = i;
                if(card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                yOffset = yOffset + 0.3f;
                zOffset = zOffset + 0.03f;
                discardPile.Add(card);
            }
        }

        foreach(string card in discardPile) // on enlève du deck les cartes déjà sur le jeu
        {
            if(deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
    }


    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear();

        int modifier = 0;
        for(int i = 0; i < trips; i ++)
        {
            List<string> myTrips = new List<string>();
            for(int j=0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }

            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }

        if(tripsRemainder != 0) // on rajoute les cartes qu'ils restent
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for(int k=0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier ++;
            }
            deckTrips.Add(myRemainders);
            trips ++;
        }

        deckLocation = 0;
    }

    public void DealFromDeck()
    {
        // add remaining cards to discard pile
        foreach(Transform child in deckButton.transform)
        {
            if(child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
           
        }
    
        if(deckLocation < trips)
        {
            // dessine 3 cartes
            tripsOnDisplay.Clear();
            float xOffset = 2.5f;
            float zOffset = -0.2f;
            foreach(string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                xOffset += 0.5f;
                zOffset -= 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;
            }

            deckLocation++;
        }
        else
        {
            // restack the top deck
            RestackTopDeck();
        } 
    }

    void RestackTopDeck()
    {
        deck.Clear();
        foreach(string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }
/*
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
*/
}
