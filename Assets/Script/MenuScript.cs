using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuScript: MonoBehaviour
{
    [MenuItem("Tools/CreateMap")]
    public static void CreateMap()
    {
        DestroyImmediate(GameObject.Find("Map"));
        GameObject map = GameObject.Find("Map");
        if (map == null)
            map = new GameObject("Map");
        if (map.GetComponent<Plateau>() == null)
        {
            //Debug.Log("Ajout de la classe Plateau à la map");
            map.AddComponent<Plateau>();
            Plateau plateau = map.GetComponent<Plateau>();
            plateau.tile = (GameObject) Resources.Load("tmp/BasicTile", typeof(GameObject));
        }
        else
        {
            map.GetComponent<Plateau>().Map = null;
        }
        map.GetComponent<Plateau>().CreateEmptyMap();
    }

    [MenuItem("Tools/Create3x3Map")]
    public static void Create3x3Map()
    {
        DestroyImmediate(GameObject.Find("Map"));
        GameObject map = GameObject.Find("Map");
        if (map == null)
            map = new GameObject("Map");
        if (map.GetComponent<Plateau>() == null)
        {
            //Debug.Log("Ajout de la classe Plateau à la map");
            map.AddComponent<Plateau>();
            Plateau plateau = map.GetComponent<Plateau>();
            plateau.tile = (GameObject)Resources.Load("tmp/BasicTile", typeof(GameObject));
        }
        else
        {
            map.GetComponent<Plateau>().Map = null;
        }
        map.GetComponent<Plateau>().CreateBasicEmptyMap(3, 3);
    }
    [MenuItem("Tools/Create5x5Map")]
    public static void Create5x5Map()
    {
        DestroyImmediate(GameObject.Find("Map"));
        GameObject map = GameObject.Find("Map");
        if (map == null)
            map = new GameObject("Map");
        if (map.GetComponent<Plateau>() == null)
        {
            //Debug.Log("Ajout de la classe Plateau à la map");
            map.AddComponent<Plateau>();
            Plateau plateau = map.GetComponent<Plateau>();
            plateau.tile = (GameObject)Resources.Load("tmp/BasicTile", typeof(GameObject));
        }
        else
        {
            map.GetComponent<Plateau>().Map = null;
        }
        map.GetComponent<Plateau>().CreateBasicEmptyMap(5,5);
    }

    [MenuItem("Tools/Create10x10Map")]
    public static void Create10x10Map()
    {
        DestroyImmediate(GameObject.Find("Map"));
        GameObject map = GameObject.Find("Map");
        map = null;
        if (map == null)
            map = new GameObject("Map");
        if (map.GetComponent<Plateau>() == null)
        {
            //Debug.Log("Ajout de la classe Plateau à la map");
            map.AddComponent<Plateau>();
            Plateau plateau = map.GetComponent<Plateau>();
            plateau.tile = (GameObject)Resources.Load("tmp/BasicTile", typeof(GameObject));
        }
        else
        {
            map.GetComponent<Plateau>().Map = null;
        }
        map.GetComponent<Plateau>().CreateBasicEmptyMap(10, 10);
    }
    
}
