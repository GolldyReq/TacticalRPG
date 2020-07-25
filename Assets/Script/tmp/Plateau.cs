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
                Debug.Log(i + ":" + j);
            }
        }
        Tile.LoadVoisin(tiles);
    }
}
