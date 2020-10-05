using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluant : Personnage
{

    
    public Gluant() : base("null",null,null)
    {

    }


    private void Start()
    {

        IsMoving = false;
        this.currentTile = getTile();
        targetTile = null;
        tileToGo = new List<Tile>();

        this.pname = "gluandet";
        this.m_stats = new Statistiques(5, 3, 2);
        this.pvMax = m_stats.getPv();
        this.pmMax = m_stats.getPm();
        this.m_attaques = new List<Attaque>();
        this.m_attaques.Add(new Attaque("Charge", 2, 1));
        this.m_attaques.Add(new Attaque("Glucoup", 3, 1, Attaque.RANGE_TYPE.Line, 3));
    }
}
