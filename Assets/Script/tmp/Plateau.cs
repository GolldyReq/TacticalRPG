using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour
{

    [SerializeField] public GameObject tile;
    GameObject[,] tiles;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tile.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEmptyMap()
    {
        int x_max = 4;
        int y_max = 4;
        
        Debug.Log("Géneration du terrain");
        tiles = new GameObject[x_max, y_max];
        for (int i = 0; i < x_max; i++)
        {
            for (int j = 0; j < y_max; j++)
            {
                if (!GameObject.Find(i.ToString() + "_" + j.ToString()))
                {
                    GameObject Tile = (GameObject)Instantiate(tile);
                    Tile.transform.position = new Vector3(i * 5, 0, j * 5);
                    Tile.name = i.ToString() + "_" + j.ToString();
                    Tile.AddComponent<Tile>();
                    Tile.GetComponent<Tile>().x = i * 5;
                    Tile.GetComponent<Tile>().z = j * 5;
                    Tile.transform.parent = GameObject.Find("Map").gameObject.transform;
                    tiles[i, j] = Tile;
                }
                else
                {
                    tiles[i, j] = GameObject.Find(i.ToString() + "_" + j.ToString());
                }
                Debug.Log(i + ":" + j);
            }
        }
        Tile.LoadVoisin(tiles);
    }

    private Tile getVoisin(float x,float z)
    {
        Tile t = null;
        try
        {

            GameObject voisin = GameObject.Find(x.ToString() + "_" + z.ToString());
            Debug.Log("Trouvé : " + voisin.name);
            t = voisin.GetComponent<Tile>();
        }
        catch(Exception e)
        {
            Debug.Log("Impossible de trouver " + x.ToString() + "_" + z.ToString());
        }
        return t;
    }

    private void LoadVoisin()
    {
        //for(int i=0;i<tiles.Rank;i++)
        for(int i = 0 ; i < tiles.GetLength(0) ; i++)
        {
            //for(int j=0;j<tiles.GetLength(i);j++)
            for(int j = 0 ; j < tiles.GetLength(1); j++)
            {
                Tile current = tiles[i,j].GetComponent<Tile>();
                float x = current.x / 5;
                float z = current.z / 5;
                Debug.Log("Attribution des voisins de la tuile : " + current.tname);
                //Trouver le voisin de gauche
                current.Gauche = getVoisin(x-1,z);
                //Trouver le voisin de droite
                current.Droite = getVoisin(x + 1, z);
                //Trouver le voisin de devant 
                current.Arriere = getVoisin(x, z-1);
                //Trouver le voisin de derrière
                current.Avant = getVoisin(x , z+1);

            }
        } 
    }
}
