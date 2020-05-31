using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeImage : MonoBehaviour
{
    public GameObject imageGood;
    public GameObject imageBad;
    private int currentTime;

    private int begin = 0;
    private int fin = 10;
    private int middle = 5;

    private float parameter = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
       currentTime = 0; 
       
    }

    public void changeIntervalle(float value)
    {
        parameter = value;
        float intervalle = value * (fin-begin) + begin;
        middle = (int)intervalle;
        //print("middle : " + middle);

    }

    public float ReturnParameter()
    {
        return parameter;
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
        if(currentTime%100 == 0)
        {
            if(loiUniformeContinue(begin, fin, begin, middle) == true)
            {
                //print("bad");
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
