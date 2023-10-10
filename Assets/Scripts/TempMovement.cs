using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMovement : MonoBehaviour
{
   public float MoveSpeed = 5f;

   private float MoveHorizontal;
   private float MoveVertical;

void Update()
{
    MoveHorizontal = Input.GetAxis("Horizontal");
    MoveVertical = Input.GetAxis("Vertical");

    Vector2 movement = new Vector2(MoveHorizontal, MoveVertical).normalized;

    transform.Translate(movement * MoveSpeed * Time.deltaTime);
}

}
