﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tuile du plateau
public class Tile : MonoBehaviour
{
    //Chaque tuile a une coordonné sur x,y,z
    public float x, y, z;
    //Chaque tuile posséde un nom de type 'x:y:z'
    public string tname;
    //Chaque tuile posséde des voisins accessible
    //Voisins = Tableau de maximum 8 tuiles (pour les diagonales mais limité a 4 pour l'instant) 
    //Tuile 0 = Avant, 1=Arriere
    [SerializeField] public Tile Avant;
    [SerializeField] public Tile Arriere;
    [SerializeField] public Tile Gauche;
    [SerializeField] public Tile Droite;
    
    //Certaines tuiles pourront avoir une taille plus grande
    public int taille = 1;
    //Savoir si un joueur se trouve sur la tuile
    public bool empty;
    //Joueur se trouvant sur la tuile
    public Personnage currentPlayer;
    //Certaine tuile seront destructible 
    public bool destructible;


    // Start is called before the first frame update
    void Start()
    {
        this.x = transform.position.x/5;
        this.y = transform.position.y/5;
        this.z = transform.position.z/5;
        this.tname = x.ToString() +":"+ y.ToString()+":"+ z.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Retourne vrai si un joueur se situe sur la tuile
    public bool IsEmpty()
    {
        return empty;
    }

    //L->Gauche R->Droite F->Devant B->Derriere
    public bool hasVoisin(char direction)
    {
        bool has = false;
        if (direction == 'L' && this.Gauche != null || direction == 'R' && this.Droite != null || direction == 'F' && this.Avant != null || direction == 'B' && this.Arriere != null)
            return true;
        return has;
    }

    void OnMouseEnter()
    {
        this.GetComponent<Renderer>().material.color = Color.blue;

    }

    void OnMouseDown()
    {

        Debug.Log(name);
        this.GetComponent<Renderer>().material.color = Color.green;
        GameController.GetCurrentPlayer().setTargetTile(this);
        

    }

    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material.color = Color.white;

    }
}
