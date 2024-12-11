using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] PlayerGridMovement playerGridMovement;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    // Update is called once per frame
    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.position += transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.position -= transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.position += transform.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.position -= transform.up;
        }
    }
}
