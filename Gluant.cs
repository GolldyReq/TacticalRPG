using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluant : Personnage
{
    private Personnage p;
    private Statistiques stats;
    private List<Attaque> ListAtt;


    private void Start()
    {
        stats = new Statistiques(3, 2, 2, 1, 1, 1);
        ListAtt.Add(new Attaque("charge", 2, 1));
        ListAtt.Add(new Attaque("Glucoup", 3, 1, Attaque.RANGE_TYPE.Line, 3));

        p = new Personnage("Gluant",stats,ListAtt);
        
    }
}
