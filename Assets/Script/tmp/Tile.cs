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

    private bool select;
    public bool color;


    public void SetPos(float x,float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    void Awake()
    {
        this.select = false;
        this.color = false;
        this.tname = x.ToString() + ":" + y.ToString() + ":" + z.ToString();
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
        if (!select)
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            this.GetComponent<MeshRenderer>().enabled = false ;
        }
        if (color && !select)
        {

            //this.GetComponent<Renderer>().material.color = Color.yellow;
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<Renderer>().material =(Material) Resources.Load("TileMaterial");
            this.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,0f,0.7f);
        }
        if (this == GameController.m_Instance.GetCurrentPlayer().currentTile)
        {
            //this.GetComponent<Renderer>().material.color = Color.red;
            //this.GetComponent<Renderer>().material.color = Color.yellow;
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<Renderer>().material = (Material)Resources.Load("TileMaterial");
            this.GetComponent<Renderer>().material.color = new Color(1.0f, 0f, 0f, 0.7f);
        }
    }

    //Retourne vrai si un joueur se situe sur la tuile
    public bool IsEmpty()
    {
        return empty;
    }

    void OnMouseEnter()
    {
        select = true;
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Renderer>().material = (Material)Resources.Load("TileMaterial");
        this.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1.0f, 0.7f);
        //Afficher Stats Personnage
        //if (!this.IsEmpty() && this != GameController.m_Instance.GetCurrentPlayer().currentTile && GameController.m_Instance.m_Phase == GameController.PHASEACTION.ChoixAction)
        if (!this.IsEmpty() && this != GameController.m_Instance.GetCurrentPlayer().currentTile )
        {
            ToolsPannel.ChangeSelectedPlayerUI(this.currentPlayer);
        }
    }

    void OnMouseDown()
    {
        select = true;
        //Deplacement
        if (GameController.m_Instance.m_Phase == GameController.PHASEACTION.ChoixDeplacement && GameManager.m_Instance.m_State == GameManager.GAME_STATE.Play && !GameController.Instance.hasMove)
        {
            //this.GetComponent<Renderer>().material.color = Color.green;
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<Renderer>().material = (Material)Resources.Load("TileMaterial");
            this.GetComponent<Renderer>().material.color = new Color(0f, 1.0f, 0f, 0.7f);
            //GameController.m_Instance.GetCurrentPlayer();
            if (this.IsEmpty() && this != GameController.m_Instance.GetCurrentPlayer().currentTile)
                GameController.m_Instance.GetCurrentPlayer().setTargetTile(this);
            
        }
        //Attaque
        if (GameController.m_Instance.m_Phase == GameController.PHASEACTION.ChoixCible)
        {
            //Debug.Log("Attaque");
            if (!this.IsEmpty() && this != GameController.m_Instance.GetCurrentPlayer().currentTile)
                GameController.m_Instance.GetCurrentPlayer().attack(this.currentPlayer);
            else
                Debug.Log("aucun joueur sur la case choisie");
        }  
        /*
        //Afficher Stats Personnage
        if(!this.IsEmpty() && this!= GameController.m_Instance.GetCurrentPlayer().currentTile &&GameController.m_Instance.m_Phase == GameController.PHASEACTION.ChoixAction)
        {
            ToolsPannel.ChangeSelectedPlayerUI(this.currentPlayer);
        }
        */
    }

    void OnMouseExit()
    {
        select = false;
        this.GetComponent<Renderer>().material.color = Color.white;
        ToolsPannel.EraseSelectedPlayerUI();

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
                //Debug.Log("Attribution des voisins de la tuile : " + current.tname);
                //Debug.Log("current : " + current.x + " : " + current.z);
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
                    //Debug.Log("Recherche du voisin : " + i + " : " + j);
                    GameObject voisin = GameObject.Find(i.ToString() + "_" + j.ToString());
                    //Debug.Log("Trouvé : " + voisin.name);
                    this.m_voisins.Add(voisin.GetComponent<Tile>());
                }
                catch (Exception e)
                {
                    //Debug.Log("Impossible de trouver " + i.ToString() + "_" + j.ToString());
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
            //Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
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

    public static void getListTileMove(Personnage p ,Tile tile, List<Tile> l)
    {
        if (Tile.Distance(p.currentTile, tile) > p.m_stats.getMobility())
            return;
        if (tile.IsEmpty() && !l.Contains(tile))
        {
            l.Add(tile);
            foreach (Tile newtile in tile.m_voisins)
                Tile.getListTileMove(p, newtile, l);
        }
    }
    public static void ShowDeplacementTile(Personnage p)
    {
        List<Tile> listMove = new List<Tile>();
        foreach(Tile t in p.currentTile.m_voisins)
            Tile.getListTileMove(p,t, listMove);

        foreach(Tile t in listMove)
        {
            t.color = true;
        }
        /*
        foreach(Tile t in p.currentTile.m_voisins)
        {
           t.color = true;
        }
        */
    }
    public static void HideDeplacementTile(Personnage p)
    {
        List<Tile> listMove = new List<Tile>();
        foreach (Tile t in p.currentTile.m_voisins)
            Tile.getListTileMove(p, t, listMove);

        foreach (Tile t in listMove)
        {
            t.color = false;
        }
        //Debug.Log(p.currentTile.name);
        /*
        foreach (Tile t in p.currentTile.m_voisins)
        {
            t.color = false;
        }
        */
    }

    public static Tile getFrontVoisin(Tile actual)
    {
        Tile t = null;
        Debug.Log(actual.x);
        try
        {

            GameObject voisin = GameObject.Find(actual.x.ToString() + "_" + (actual.z+1).ToString());
            //Debug.Log("Trouvé : " + voisin.name);
            t = voisin.GetComponent<Tile>();
        }
        catch (Exception e)
        {
            //Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
        }
        return t;
    }

    public static Tile getVoisinTile(Tile actual , string searchVoisin)
    {
        Tile t = null;
        //Debug.Log(actual.x);
        try
        {

            GameObject voisin = GameObject.Find(searchVoisin);
            //Debug.Log("Trouvé : " + voisin.name);
            t = voisin.GetComponent<Tile>();
        }
        catch (Exception e)
        {
            //Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
        }
        return t;
    }
   
    public static Tile getTileWithCoord(int x, int z)
    {
        Tile t = null;
        return t;
    }
    public static bool IsTileExist()
    {
        bool res = false;
        return res;
    }

    
}
