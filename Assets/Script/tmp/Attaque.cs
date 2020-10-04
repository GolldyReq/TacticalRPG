using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attaque
{
    private string name;
    private int dammage;
    private int range;
    private int cost;
    private float miss;
    public enum RANGE_TYPE { Line , Circle , Square,Spray,Horse}
    private RANGE_TYPE rangeType;
    public enum ATT_TYPE { fire, ice, earth, electric, wind, light, darkness }
    private ATT_TYPE type;
    public List<Tile> cibles;



    public Attaque(string name, int dammage, int range, RANGE_TYPE rangeType, int cost)
    {
        this.name = name;
        this.dammage = dammage;
        this.range = range;
        this.rangeType = rangeType;
        this.cost = cost;
        cibles = new List<Tile>();
    }
    public Attaque(string name,int dammage,int range) : this(name,dammage,range, RANGE_TYPE.Line,0)
    {

    }
    public Attaque(string name, int dammage) : this(name, dammage, 1, RANGE_TYPE.Line, 0) { }


    public int getDammage() { return this.dammage; }
    public int getRange() { return this.range; }
    public string getName() { return this.name; }
    public RANGE_TYPE getType() { return this.rangeType; }
    public int getCost() { return this.cost; }

    public List<Tile> getCibles() { return this.cibles; }
    public void setCibles(List<Tile> list)
    { this.cibles = list; }
    public void resetCibles() { cibles = null; }
}
