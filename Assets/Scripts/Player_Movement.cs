using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Animator animator;
    Vector2 movement;

    public LayerMask stopMovement;

    private void Start() 
    {
        movePoint.parent = null;
        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        Client.instance.SendPlayerMessage(movePoint.position.x, movePoint.position.y);
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(movement.x) == 1f)
            {
                Vector3 newPos = new Vector3(movement.x, 0f, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + newPos, .2f, stopMovement))
                    movePoint.position += newPos;
                else
                    movePoint.position = transform.position;

            }
            if (Mathf.Abs(movement.y) == 1f)
            {
                Vector3 newPos = new Vector3(0f, movement.y, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + newPos, .2f, stopMovement))
                    movePoint.position += newPos;
                else
                    movePoint.position = transform.position;

            }
        }
    }
}
