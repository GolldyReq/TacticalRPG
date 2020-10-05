using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Monsters
{
    public class Gluant : Personnage
    {
        public Gluant(string name, Statistiques stats, List<Attaque> att) : base(name, stats, att)
        {
            name = "gluant";
            stats = new Statistiques(3, 2, 2, 1, 1, 1);
            att.Add(new Attaque("charge", 2, 1));
            att.Add(new Attaque("Glucoup", 3, 1, Attaque.RANGE_TYPE.Line, 3));
        }

    }
}