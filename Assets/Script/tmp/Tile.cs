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
    /*
    [SerializeField] public Tile Avant;
    [SerializeField] public Tile AvantGauche;
    [SerializeField] public Tile AvantDroite;
    [SerializeField] public Tile Gauche;
    [SerializeField] public Tile Droite;
    [SerializeField] public Tile Arriere;
    [SerializeField] public Tile ArriereGauche;
    [SerializeField] public Tile ArriereDroite;
    */

    public List<Tile> m_voisins;
    //Tableau des voisins
    //private Tile[] voisins;

    //Certaines tuiles pourront avoir une taille plus grande
    public int taille = 1;
    //Savoir si un joueur se trouve sur la tuile
    public bool empty;
    //Joueur se trouvant sur la tuile
    public Personnage currentPlayer;
    //Certaine tuile seront destructible 
    public bool destructible;

    private bool coolDown;


    public void SetPos(float x,float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    void Awake()
    {
        this.tname = x.ToString() + ":" + y.ToString() + ":" + z.ToString();
        coolDown = false;
        this.empty = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        this.x = transform.position.x/5;
        this.y = transform.position.y;
        this.z = transform.position.z/5;
        */

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

    void OnMouseEnter()
    {
        this.GetComponent<Renderer>().material.color = Color.blue;

    }

    void OnMouseDown()
    {
        if (!coolDown && GameManager.m_Instance.m_State == GameManager.GAME_STATE.Play)
        {
            coolDown = true;
            Debug.Log(name);
            this.GetComponent<Renderer>().material.color = Color.green;
            //GameController.m_Instance.GetCurrentPlayer();
            if (this.IsEmpty() && this != GameController.m_Instance.GetCurrentPlayer().currentTile)
                GameController.m_Instance.GetCurrentPlayer().setTargetTile(this);
            else if(this != GameController.m_Instance.GetCurrentPlayer().currentTile)
                GameController.m_Instance.GetCurrentPlayer().attack(this.currentPlayer);
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
                //float x = current.x / 5;
                //float z = current.z / 5;
                current.SetPos(i, 0, j);
                Debug.Log("Attribution des voisins de la tuile : " + current.tname);
                Debug.Log("current : " + current.x + " : " + current.z);
                current.getVoisins();
                
            }
        }
    }
    private void getVoisins()
    {
        for(int i = (int)this.x-1;i<=(int)this.x+1;i++)
        {
            for(int j=(int)this.z-1;j<=(int)this.z+1;j++)
            {
                if (i == x && j == z)
                    continue;
                try
                {
                    Debug.Log("Recherche du voisin : " + i + " : " + j);
                    GameObject voisin = GameObject.Find(i.ToString() + "_" + j.ToString());
                    Debug.Log("Trouvé : " + voisin.name);
                    this.m_voisins.Add(voisin.GetComponent<Tile>());
                }
                catch (Exception e)
                {
                    Debug.Log("Impossible de trouver " + i.ToString() + "_" + j.ToString());
                }
                
            }
        }
    }

    private Tile getVoisin(float x, float z)
    {
        Tile t = null;
        try
        {

            GameObject voisin = GameObject.Find(x.ToString() + "_" + z.ToString());
            //Debug.Log("Trouvé : " + voisin.name);
            t = voisin.GetComponent<Tile>();
        }
        catch (Exception e)
        {
            Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
        }
        return t;
    }

    public Tile[] getAllVoisins()
    {
        Tile[] liste_voisins = new Tile[m_voisins.Count];
        int i = 0;
        foreach (Tile t in m_voisins)
        {
            liste_voisins[i] = t;
            i++;
            
        }
        return liste_voisins;
    }  

    //Distance entre 2 cases
    public static int Distance(Tile start, Tile end)
    {
        int d = 0;
       
        while(start != end)
        {
            Tile next = null;
            next = start.m_voisins[0];
            float dist = Vector3.Distance(start.transform.position, end.transform.position);
            foreach (Tile t in start.m_voisins)
            {
                float newdist = Vector3.Distance(t.transform.position, end.transform.position);
                if (newdist < dist)
                {
                    next = t;
                    dist = newdist;
                }
            }
            start = next;
            d++;
        }
        return d;
    }

 }
