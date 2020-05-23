using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        // remove all the cards
        UpdateCard[] cards = FindObjectsOfType<UpdateCard>();
        foreach(UpdateCard card in cards)
        {
            Destroy(card.gameObject);
        }
        ClearTopValues();
        // recreate new game
        FindObjectOfType<Solitaire>().PlayCards();

    }

    void ClearTopValues()
    {
        Selectable[] selectables = FindObjectsOfType<Selectable>();
        foreach(Selectable selectable in selectables)
        {
            if(selectable.CompareTag("Top"))
            {
                selectable.suit = null;
                selectable.value = 0;
            }
        }
    }
}
