using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour
{

    public static Plateau m_Instance;
    public static Plateau Instance { get { return m_Instance; } }


    [SerializeField] public GameObject tile;
    GameObject[,] tiles;
    public List<Tile> Map;

    int x_max = 5;
    int y_max = 5;

    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Map = new List<Tile>();
        for (int i = 0; i < x_max; i++)
        {
            for (int j = 0; j < y_max; j++)
            {
                if (GameObject.Find(i.ToString() + "_" + j.ToString()))
                {
                    Map.Add(GameObject.Find(i.ToString()+"_"+j.ToString()).GetComponent<Tile>());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEmptyMap()
    {
       
        
        //Debug.Log("Géneration du terrain");
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
                    if(Tile.GetComponent<Tile>()==null)
                        Tile.AddComponent<Tile>();
                    //Tile.GetComponent<Tile>().x = i*5;
                    //Tile.GetComponent<Tile>().z = j*5;
                    Tile.transform.parent = GameObject.Find("Map").gameObject.transform;
                    tiles[i, j] = Tile;
                }
                else
                {
                    tiles[i, j] = GameObject.Find(i.ToString() + "_" + j.ToString());
                }
                //Debug.Log(i + ":" + j);
            }
        }
        Tile.LoadVoisin(tiles);
    }

    public void resetColorAllTile()
    {
        foreach(Tile t in Map)
        {
            t.color = false;
            t.GetComponent<MeshRenderer>().enabled = false;
            //t.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
