using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
    private Vector3 posMouse;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask collisionLayer;

    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if(isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            posMouse = Input.mousePosition;
        }else if (Input.GetMouseButton(0))
        {
            var drag = Input.mousePosition - posMouse;
            if(drag.magnitude >= 50f)
            {
                drag.Normalize();
                if(Mathf.Abs(drag.x) > Mathf.Abs(drag.y))
                {
                    if(drag.x > 0)
                    {
                        moveDirection = Vector3.right;
                    }
                    else
                    {
                        moveDirection = Vector3.left;
                    }
                }
                else
                {
                    if (drag.y > 0)
                    {
                        moveDirection = Vector3.forward;
                    }
                    else
                    {
                        moveDirection = Vector3.back;
                    }
                }

            }
        }

        Ray ray = new Ray(transform.position, moveDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, collisionLayer))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}