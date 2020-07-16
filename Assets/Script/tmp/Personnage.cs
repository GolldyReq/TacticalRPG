using System.Collections;
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

    public void setTargetTile (Tile targetTile){
        this.targetTile = targetTile;
        Debug.Log(this.pname + " doit se rendre sur la case : " + this.targetTile.tname);
    }
}
