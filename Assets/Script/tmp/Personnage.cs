﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    //Savoir si le personnage se déplace
    public bool IsMoving;

    //Connaître la tuile sur lequel est le personnage
    public Tile currentTile;
    
    //Tuile sur laquelle on veut se rendre 
    //cette tuile vaux null tant que l'on ne clique pas sur une tuile
    public Tile targetTile;

    //Nom du personnage
    [SerializeField] string pname;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        currentTile = getTile();
        Debug.Log(currentTile.name);
        targetTile = null;
        if (this.pname == null)
            this.pname = "player";

        Debug.Log(this.pname);
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
        if (hInput != 0 && !IsMoving)
        {
            if (hInput < 0 && currentTile.hasVoisin('L'))
                StartCoroutine(Mouvement.Gauche(gameObject));
            if (hInput > 0 && currentTile.hasVoisin('R'))
                StartCoroutine(Mouvement.Droite(gameObject));
        }
        if (vInput != 0 && !IsMoving)
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

    public Tile getTile()
    {
        Tile t = null;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 5))
        {
            t = hit.transform.gameObject.GetComponent<Tile>();
            t.empty = false;
            t.currentPlayer = this;
        }
        return t;
    }

    public void setTargetTile(Tile targetTile)
    {
        if (targetTile.tname != currentTile.tname)
        {
            this.targetTile = targetTile;
            //Liste des mouvements à effectuer pour bouger jusqu'a la target
            List<Tile> liste_move = new List<Tile>(); 
            getPath(liste_move);
            Debug.Log("Fin du pathfinding");
            Debug.Log(liste_move.Count);
            Debug.Log("Liste du chemin :");
            foreach(Tile t in liste_move)
            {
                Debug.Log(t.tname);
            }
            Debug.Log("Debut Deplacement");
            StartCoroutine(Mouvement.GoTO(gameObject, liste_move));
        }
    }
    private void getPath(List<Tile> path)
    {
        //Cases voisines a la case actuelle
        Tile[] caseVoisine = currentTile.getAllVoisins();
        if (caseVoisine.Length > 0)
        {
            //Le premier voisin est par défault le premier choix 
            //Debug.Log("Case actuelle : " + this.currentTile.tname);
            Tile NextMove = caseVoisine[0];
            //Mesure de la distance entre la case choisi et la case target
            float dist = Vector3.Distance(NextMove.transform.position, targetTile.transform.position);
            foreach (Tile t in caseVoisine)
            {
                float newdist = Vector3.Distance(t.transform.position, targetTile.transform.position);
                //Debug.Log("Distance entre " + targetTile.tname + " et " + t.tname + " = " + newdist.ToString());
                if (newdist < dist)
                {
                    dist = newdist;
                    NextMove = t;
                }
            }
            Debug.Log("Ajout de la case " + NextMove.tname);
            path.Add(NextMove);
            this.currentTile = NextMove;
            //Debug.Log("Prochaine case : " + this.currentTile.tname);
            if (this.currentTile != this.targetTile)
                getPath(path);
                //setTargetTile(this.targetTile);
            
        }
    }
}
