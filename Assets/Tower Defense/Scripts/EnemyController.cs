using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public PathController route;
  public float health;
  public float speed = .25f; 
  public PurseController pc;
  
  private Waypoint[] road;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;


  void Awake()
  {
    road = route.path;
    transform.position = road[index].transform.position;
    Recalculate();
  }

  void Update()
  {
    //Update enemy movement
    if (!stop)
    {
      if ((transform.position - road[index + 1].transform.position).magnitude < .1f)
      {
        index = index + 1;
        Recalculate();
      }

      Vector3 moveThisFrame = nextWaypoint * Time.deltaTime * speed;
      transform.Translate(moveThisFrame);
    }

    //Check for player mouse click
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      
      //Check for enemy under mouse
      if (Physics.Raycast(ray, out hit, 5000.0f))
      {
        
        if (hit.transform.CompareTag("BigEnemy"))
        {
          //Decrement health of enemy
          if (health > 2)
          {
            decrementEnemyHealth();
          }
          else
          {
            //Add coins to purse
            pc.updateCoins(10);
            //Destroy enemy if health is 2 or less
            Destroy(hit.transform.gameObject);
          }
        } else if (hit.transform.CompareTag("SmallEnemy"))
        {
          //Decrement health of enemy
          if (health > 2)
          {
            decrementEnemyHealth();
          }
          else
          {
            //Add coins to purse
            pc.updateCoins(5);
            //Destroy enemy if health is 2 or less
            Destroy(hit.transform.gameObject);
          }
        }
      }
    }

  }

  void Recalculate()
  {
    if (index < road.Length -1)
    {
      nextWaypoint = (road[index + 1].transform.position - road[index].transform.position).normalized;

    }
    else
    {
      stop = true;
    }
  }

  void decrementEnemyHealth()
  {
    health -= 2;
  }
  
  
}
