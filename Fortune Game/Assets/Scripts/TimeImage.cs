using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeImage : MonoBehaviour
{
    public GameObject imageGood;
    public GameObject imageBad;
    private int currentTime;
    // Start is called before the first frame update
    void Start()
    {
       currentTime = 0; 
    }

    private bool loiUniformeContinue(int a, int b, int c, int d)
    {
        float rand = Random.Range(0.0f, 1.0f)* (b-a) + a; 
        if(c <= rand && rand <= d)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Obtain the current time.
        //int currentTime = (int)Time.time;
        //print("Time is: " + currentTime.ToString() + " sec.");
        if(currentTime%1000 == 0)
        {
            if(loiUniformeContinue(0, currentTime, 0, currentTime/2) == true)
            {
                imageGood.SetActive(false);
                imageBad.SetActive(true);
                
            }
            else
            {
                imageGood.SetActive(true);
                imageBad.SetActive(false);
                
            }
            
        }
        currentTime ++;
      
      
    }
}
