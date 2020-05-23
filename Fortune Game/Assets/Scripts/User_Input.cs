using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class User_Input : MonoBehaviour
{
    public GameObject slot1;
    private Solitaire solitaire;
    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if( Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit)
            {
                if(hit.collider.CompareTag("Deck")) // clicked Deck
                {
                    Deck();
                }
                else if(hit.collider.CompareTag("Card")) // clicked Card
                {
                    Card(hit.collider.gameObject);
                }
                if(hit.collider.CompareTag("Top")) // clicked Top
                {
                    Top(hit.collider.gameObject);
                }
                if(hit.collider.CompareTag("Bottom")) // clicked Bottom
                {
                    Bottom(hit.collider.gameObject);
                }
               
            }
        }
    }

    void Deck()
    {
        print("clicked on deck");
        solitaire.DealFromDeck();
        slot1 = this.gameObject;
    }

    void Card(GameObject selected)
    {
        print("clicked on card");

        if(!selected.GetComponent<Selectable>().faceUp)// if the card clicked on is facedown
        {
            if(!Blocked(selected))  // if the card is not blocked
            {
                // flip it over
                selected.GetComponent<Selectable>().faceUp = true;
                slot1 = this.gameObject;
            }
        }
        else if(selected.GetComponent<Selectable>().inDeckPile) // if the card clicked on is in the deck pile with the trips            
        {
            // if the card is not blocked
            if(!Blocked(selected))
            {
                print("select card");
                // select it
                slot1 = selected;
            }
            
        }
        else
        {
            if(slot1 == this.gameObject)
            {
                slot1 = selected;
            }
            
            else if(slot1 != selected) // if there is a card already selected ( and it's not the same card)
            {
                
                // if the new card is eligable to stack out on the old card
                
                if(Stackable(selected))
                {
                    // stack it
                    Stack(selected);
                }
                else
                {
                    // select the new card
                    slot1 = selected;
                } 
                
                
            

            }
        }
            

        // if the card is face up
            // if there is no cards currently selected 
            // select i t
       
        
        // else if there is already selected and it's the same card
            // if the time is short enough then it is a double click
                // if the card is eligible to fly up top then do it

    
    }

    void Top(GameObject selected)
    {
        print("clicked on top");
        if(slot1.CompareTag("Card"))
        {
            if(slot1.GetComponent<Selectable>().value == 1)
            {
                Stack(selected); 
            }
        }
    }

    void Bottom(GameObject selected)
    {
        print("clicked on bottom");
        // if the card is the king and the bottom is empty then stack it
        if(slot1.CompareTag("Card"))
        {
            if(slot1.GetComponent<Selectable>().value == 13)
            {
                Stack(selected);
            }
        }
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        // compare them if they can stack
        if(!s2.inDeckPile)
        {
            if(s2.top) // if in the top pile must stack suited Ace to King
            {
                if(s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if(s1.value == s2.value + 1)
                    {
                        return true;
                    }
                }
                else 
                {
                    return false;
                }
            }
            else // if in the bottom pile must stack alternate colours King to Ace 
            {
                if(s1.value == s2.value - 1)
                {
                    bool card1Red = true;
                    bool card2Red = true;

                    if(s1.suit == "C" || s1.suit == "S")
                    {
                        card1Red = false;
                    }
                    if(s2.suit == "C" || s2.suit == "S")
                    {
                        card2Red = false;
                    } 
                    if(card1Red == card2Red)
                    {
                        print("Not stackable");
                        return false;
                    }
                    else
                    {
                        print("Stackable");
                        return true;
                    }
                }
            }
        }
        
         
        return false;  
    }


     
    void Stack(GameObject selected)
    {
        // if on top of king or empty bottom stack the cards in place
        // else stack the cards with a negative y offset
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        float yOffset = 0.3f;
        if(s2.top || (!s2.top && s1.value == 13))
        {
            yOffset = 0;
        }
        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        slot1.transform.parent = selected.transform; // this makes the children move with the parents

        if(s1.inDeckPile) // removes the cards from the top pile to prevent duplicate cards
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
        }
        else if (s1.top && s2.top && s1.value == 1) // allows movement of cards between top spots
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = null;
        }
        else if(s1.top) // keeps track of the current value of the top decks as a card has been removed
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value - 1;
        }
        else // removes the card string from the bottom list
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }
        s1.inDeckPile = false; // you can't add cards to the trips pile 
        s1.row = s2.row;

        if(s2.top) // move a card to the top and assign value and suit
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = s1.suit;
            s1.top = true;
        }
        else 
        {
            s1.top = false;
        }

        slot1 = this.gameObject;
    }

    bool Blocked(GameObject selected)
    {
        Selectable s2 = selected.GetComponent<Selectable>();
        if(s2.inDeckPile == true)
        {
            if(s2.name == solitaire.tripsOnDisplay.Last()) // if it's the last trip, it's not blocked
            {
                return false;
            }
            else
            {
                print(s2.name + " is blocked by " + solitaire.tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if(s2.name == solitaire.bottoms[s2.row].Last()) // check if it's the bottom card
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

    }
    
}
