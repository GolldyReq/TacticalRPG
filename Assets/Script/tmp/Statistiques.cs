﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistiques 
{
    private int pv;
    private int pm;
    private int mobility;
    private int att;
    private int def;
    private int vit;
    public enum PLAYER_STATE { normal,sleep,fire,poison,ice,paralyse }
    private PLAYER_STATE state;
    private int luck;
    public enum PLAYER_TYPE {fire,ice,earth,electric,wind,light,darkness}
    private PLAYER_TYPE type;

<<<<<<< HEAD

    public Statistiques(int pv,int pm,int mobility,int att, int def,int vit)
=======
    public Statistiques(int pv,int pm,int mobility)
>>>>>>> 132807f040b1838e36085176e2042d6932ffd4d1
    {
        this.pv = pv;
        this.pm = pm;
        this.mobility = mobility;
<<<<<<< HEAD
        this.att = att;
        this.def = def;
        this.vit = vit;
    }

    public Statistiques(int pv,int pm,int mobility):this(pv,pm,mobility,1,1,1)
=======
    }

    public Statistiques(int pv, int mobility):this(pv,10,mobility)
>>>>>>> 132807f040b1838e36085176e2042d6932ffd4d1
    {

    }

<<<<<<< HEAD
    public Statistiques(int pv, int mobility):this(pv,10,mobility,1,1,1)
    {

    }

    public Statistiques(int pv):this(pv,10,1,1,1,1)
=======
    public Statistiques(int pv):this(pv,10,1)
>>>>>>> 132807f040b1838e36085176e2042d6932ffd4d1
    {
        
    }

    public void setPv(int pv) { this.pv = pv; }
    public int getPv() { return this.pv; }
    public void setPm(int pm) { this.pm = pm; }
    public int getPm() { return this.pm; }
    public int getMobility() { return this.mobility; }

}
