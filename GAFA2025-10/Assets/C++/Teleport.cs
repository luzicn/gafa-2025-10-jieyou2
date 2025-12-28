using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject currentTeleporter; //´«ËÍÆ÷
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentTeleporter != null)
        }

    }

    private void NewMethod()
    {
        transform.position = (Vector3)currentTeleporter.GetComponent<PORTAL>().GetDeationation()position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject==currentTeleporter)
            {
                currentTeleporter = null;
            }
            
        }
    }
}
