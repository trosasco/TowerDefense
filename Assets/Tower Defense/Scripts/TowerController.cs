using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TowerController : MonoBehaviour
{
    public bool built = false;
    public GameObject tower;
    public int cost = 10;
    public PurseController pc;
    private List<GameObject> attackQueue;
    private bool entered;
    public GameObject healthBar;

    public float health = 20;
    private float startingHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        startingHealth = health;
        attackQueue = new List<GameObject>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmallEnemy") || other.CompareTag("BigEnemy"))
        {
            if (built)
            {
                attackQueue.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SmallEnemy") || other.CompareTag("BigEnemy"))
        {
        }
        
    }

    public void updateHealth(int amount)
    {
        health += amount;
        
        //If the tower is destroyed
        if (health < 0)
        {
            built = false;
            health = startingHealth;
            tower.SetActive(built);
        }
        
        float ratio = 1 / startingHealth;
        healthBar.transform.localScale = new Vector3( health * ratio,0.25f,1);
    }

}
