using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    public bool IsMoving;
    //Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        //animation = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            //animation.Play("Jump");
            StartCoroutine(Mouvement.Jump(gameObject));
        }

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        if ( hInput != 0 && !IsMoving)
        {
            if (hInput < 0)
                StartCoroutine(Mouvement.Gauche(gameObject));
            else
                StartCoroutine(Mouvement.Droite(gameObject));
        }

        if(vInput != 0 && !IsMoving)
        {
            if (vInput < 0)
                StartCoroutine(Mouvement.Reculer(gameObject));
            else
                StartCoroutine(Mouvement.Avancer(gameObject));
        }
        //StartCoroutine(SlimeCoroutine.Idle(gameObject));
    }
}
