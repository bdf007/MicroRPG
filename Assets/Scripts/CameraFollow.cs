using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
 public GameObject target;

 void Update()
 {
    //flollows the target position
    transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
 }
}
