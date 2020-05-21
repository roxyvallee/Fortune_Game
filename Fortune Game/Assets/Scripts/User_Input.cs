using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Top();
                }
                if(hit.collider.CompareTag("Bottom")) // clicked Bottom
                {
                    Bottom();
                }
               
            }
        }
    }

    void Deck()
    {
        print("clicked on deck");
        solitaire.DealFromDeck();
    }

    void Card(GameObject selected)
    {
        print("clicked on card");

        // if the card clicked on is facedown
            // if the card is not blocked
            // flip it over

        // if the card clicked on is in the deck pile with the trips
            // if the card is not blocked
            // select it

        // if the card is face up
            // if there is no cards currently selected 
            // select it
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
            }
            else
            {
                // select the new card
                slot1 = selected;
            } 
            
            
           

        }
        
        // else if there is already selected and it's the same card
            // if the time is short enough then it is a double click
                // if the card is eligible to fly up top then do it

    
    }

    void Top()
    {
        print("clicked on top");
    }

    void Bottom()
    {
        print("clicked on bottom");
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        // compare them if they can stack
        
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
         
        return false;  
    }
}
