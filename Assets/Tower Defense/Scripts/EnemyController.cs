using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
  public PathController route;
  public float health;
  public float speed = .25f; 
  public PurseController pc;
  public GameObject healthBar;
  
  private Waypoint[] road;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;
  private float startingHealth;

  private bool shot;
  public int dmg;
  public float attackRate;
  public GameObject bullet;
  private TowerController aim;

  void Awake()
  {
    startingHealth = health;
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
        
        if (hit.transform == transform)
        {
          //Decrement health of enemy
          if (health > 2)
          {
            decrementEnemyHealth();
          }
          else
          {
            //Add coins to purse
            addCoins();
            //Destroy enemy if health is 2 or less
            Destroy(hit.transform.gameObject);
          }
        }
      }
    }

  }
  
  void FixedUpdate()
  {
    if (shot && Time.time > attackRate)
    {
      attackRate = Time.time + Random.Range(1, 5);
      StartCoroutine(shootTower());
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

    float ratio = 1 / startingHealth; 
        
    healthBar.transform.localScale = new Vector3( health * ratio,0.25f,1);
  }

  void addCoins()
  {
    if (this.CompareTag("SmallEnemy"))
    {
      pc.updateCoins(5);
    } else if (this.CompareTag("BigEnemy"))
    {
      pc.updateCoins(10);
    }
  }
  
  IEnumerator shootTower()
  {
    yield return new WaitForSeconds(Random.Range(3f, 15f));
    aim.decrementTowerHealth(-dmg);
    shot = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Tower"))
    {
      aim = other.GetComponent<TowerController>();
    }
  }
}
