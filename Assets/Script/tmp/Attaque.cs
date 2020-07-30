using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque
{
    private string name;
    private int dammage;
    private int range;
    private float miss;


    public Attaque(string name,int dammage,int range)
    {
        this.name = name;
        this.dammage = dammage;
        this.range = range;
    }

    public int getDammage() { return this.dammage; }
    public int getRange() { return this.range; }
}
