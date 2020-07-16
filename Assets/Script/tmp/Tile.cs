using System;
using System.Collections;
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
    [SerializeField] public Tile AvantGauche;
    [SerializeField] public Tile AvantDroite;
    [SerializeField] public Tile Gauche;
    [SerializeField] public Tile Droite;
    [SerializeField] public Tile Arriere;
    [SerializeField] public Tile ArriereGauche;
    [SerializeField] public Tile ArriereDroite;

    //Tableau des voisins
    private Tile[] voisins;

    //Certaines tuiles pourront avoir une taille plus grande
    public int taille = 1;
    //Savoir si un joueur se trouve sur la tuile
    public bool empty;
    //Joueur se trouvant sur la tuile
    public Personnage currentPlayer;
    //Certaine tuile seront destructible 
    public bool destructible;

    private bool coolDown;

    // Start is called before the first frame update
    void Start()
    {
        this.x = transform.position.x / 5;
        this.y = transform.position.y ;
        this.z = transform.position.z / 5;
        this.tname = x.ToString() + ":" + y.ToString() + ":" + z.ToString();
        voisins = new Tile[] { Avant, AvantGauche, AvantDroite, Gauche, Droite, Arriere, ArriereGauche, ArriereDroite };
        coolDown = false;

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
        if (!coolDown)
        {
            coolDown = true;
            Debug.Log(name);
            this.GetComponent<Renderer>().material.color = Color.green;
            GameController.GetCurrentPlayer().setTargetTile(this);
            coolDown = false;
        }
       
        
    }

    void OnMouseExit()
    {
        this.GetComponent<Renderer>().material.color = Color.white;

    }

    public static void LoadVoisin(GameObject[,] tiles)
    {
        //for(int i=0;i<tiles.Rank;i++)
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            //for(int j=0;j<tiles.GetLength(i);j++)
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Tile current = tiles[i, j].GetComponent<Tile>();
                float x = current.x / 5;
                float z = current.z / 5;
                Debug.Log("Attribution des voisins de la tuile : " + current.tname);
                //Trouver le voisin de gauche
                current.Gauche = current.getVoisin(x - 1, z);
                //Trouver le voisin de droite
                current.Droite = current.getVoisin(x + 1, z);
                //Trouver le voisin de derriere 
                current.Arriere = current.getVoisin(x, z - 1);
                //Trouver le voisin de devant
                current.Avant = current.getVoisin(x, z + 1);

                //Trouver le voisin de devant-gauche
                current.AvantGauche = current.getVoisin(x - 1, z + 1);
                //Trouver le voisin de devant-droite
                current.AvantDroite = current.getVoisin(x + 1, z + 1);
                //Trouver le voisin de derrière-gauche
                current.ArriereGauche = current.getVoisin(x - 1, z - 1);
                //Trouver le voisin de derrière-droite
                current.ArriereDroite = current.getVoisin(x + 1, z - 1);

            }
        }
    }

    private Tile getVoisin(float x, float z)
    {
        Tile t = null;
        try
        {

            GameObject voisin = GameObject.Find(x.ToString() + "_" + z.ToString());
            Debug.Log("Trouvé : " + voisin.name);
            t = voisin.GetComponent<Tile>();
        }
        catch (Exception e)
        {
            Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
        }
        return t;
    }

    private int nbVoisin()
    {
        int nbVoisin = 0;
        foreach(Tile t in voisins)
        {
            if (t != null)
                nbVoisin++;
        }
        return nbVoisin;
    }

    public Tile[] getAllVoisins()
    {
        Tile[] liste_voisins = new Tile[nbVoisin()];
        int i = 0;
        foreach(Tile t in voisins)
        {
            if(t!= null)
            {
                liste_voisins[i] = t;
                i++;
            }
        }
        return liste_voisins;
    }

}
