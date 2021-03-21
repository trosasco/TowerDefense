using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
  public Waypoint[] path;

  void Awake()
  {
    Initialize();
  }

  [ContextMenu("Initialize")]
  private void Initialize()
  {
    path = gameObject.GetComponentsInChildren<Waypoint>();
  }
}
