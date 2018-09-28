using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{

    public bool isSuperDot = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            //if superDot
            if (isSuperDot)
            {
                //TODO

                GameManager.Instantce.OnEatSuperPacdot();
                GameManager.Instantce.OnEatDot(gameObject);
                
                Destroy(gameObject);

            }
            else
            {
                GameManager.Instantce.OnEatDot(gameObject);

                Destroy(gameObject);


            }


        }
    }


}
