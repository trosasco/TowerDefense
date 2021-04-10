using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerController : MonoBehaviour
{
    public bool built = false;
    public GameObject tower;
    public int cost = 10;
    public PurseController pc;

    private bool entered;
    
    // Start is called before the first frame update
    void Start()
    {
        tower.SetActive(built);
        
        //Destroys self if there is no purse
        if (pc == null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if mouse is pressed when on the tower
        if (Input.GetMouseButtonDown(0) && entered)
        {
            if (pc.getCoins() >= cost)
            {
                built = true;
                pc.updateCoins(-cost);
                tower.SetActive(built);   
            }
        }  
    }

    //modifies a bool when mouse is on the tower or not
    private void OnMouseEnter()
    {
        entered = true;
    }

    private void OnMouseExit()
    {
        entered = false;
    }
}
