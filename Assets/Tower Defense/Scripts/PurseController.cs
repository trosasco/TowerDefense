using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurseController : MonoBehaviour
{
    public Text coins;
    
    private int coinNumber = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCoins(int x)
    {
        string zeros = "";
        if (x < 10)
        {
            zeros = "000";
        } else if (x < 100)
        {
            zeros = "00";
        } else if (x < 1000)
        {
            zeros = "0";
        }
        
        coinNumber += x;
        coins.text = zeros + coinNumber;
    }
}
