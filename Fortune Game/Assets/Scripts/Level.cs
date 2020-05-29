using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    static public int level;
    public int chosenlevel;
    // Start is called before the first frame update
    public void ChoseLevel()
    {
        level = chosenlevel;
        print(level);
        //FindObjectOfType<Solitaire>().SetParameter(level);
    }

    public int ReturnLevel()
    {
        return level;
    }
}
