using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    GameObject thePlayer;
    public Transform portal;
    
    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            thePlayer.transform.position = portal.transform.position;
        }
    }
}
