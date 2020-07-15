using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    //Savoir si le personnage se déplace
    public bool IsMoving;
    //Connaître la tuile sur lequel est le personnage
    [SerializeField]public Tile currentTile;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //faire sauter le personnage
        if (Input.GetButton("Jump"))
            StartCoroutine(Mouvement.Jump(gameObject));
        //Deplacer le personnage
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        if ( hInput != 0 && !IsMoving)
        {
            if (hInput < 0 && currentTile.hasVoisin('L'))
                StartCoroutine(Mouvement.Gauche(gameObject));
            if (hInput > 0 && currentTile.hasVoisin('R'))
                StartCoroutine(Mouvement.Droite(gameObject));
        }
        if(vInput != 0 && !IsMoving)
        {
            if (vInput < 0 && currentTile.hasVoisin('B'))
                StartCoroutine(Mouvement.Reculer(gameObject));
            if (vInput > 0 && currentTile.hasVoisin('F'))
                StartCoroutine(Mouvement.Avancer(gameObject));
        }
    }

    private Tile getCurrentTile()
    {
        return currentTile;
    }
}
