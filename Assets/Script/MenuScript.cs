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
        GameObject map = GameObject.Find("Map");
        if (map == null)
            map = new GameObject("Map");
        if (map.GetComponent<Plateau>()== null)
            Debug.Log("Il faut ajouter la classe Plateau à la map");
        else
            map.GetComponent<Plateau>().CreateEmptyMap();
    }
}
