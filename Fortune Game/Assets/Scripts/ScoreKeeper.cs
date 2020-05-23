using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public Selectable[] topStacks;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        if(HasWon())
        {
            Win();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasWon()
    {
        int i = 0;
        foreach(Selectable topstack in topStacks)
        {
            i+= topstack.value;
        }
        if( i >= 52)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Win()
    {
        winPanel.SetActive(true);
        print("You have won!");
    }
}
