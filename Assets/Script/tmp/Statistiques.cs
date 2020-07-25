using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistiques 
{
    private int pv;
    private int mobility;
    private int att;
    private int def;
    private int vit;
    public enum PLAYER_STATE { normal,sleep,fire,poison,ice,paralyse }
    private PLAYER_STATE state;
    private int luck;

    public Statistiques(int pv)
    {
        this.pv = pv;
    }

    public void setPv(int pv) { this.pv = pv; }
    public int getPv() { return this.pv; }
}
