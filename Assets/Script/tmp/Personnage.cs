using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    //Savoir si le personnage se déplace
    public bool IsMoving;

    //Connaître la tuile sur lequel est le personnage
    public Tile currentTile;
    
    //Tuile sur laquelle on veut se rendre 
    //cette tuile vaux null tant que l'on ne clique pas sur une tuile
    public Tile targetTile;

    //Nom du personnage
    [SerializeField] string pname;

    public Statistiques m_stats;
    public List<Attaque> m_attaques;


    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        this.currentTile = getTile();
        targetTile = null;
        if (this.pname == null)
            this.pname = "player";
        m_stats = new Statistiques(1);
        m_attaques = new List<Attaque>();
        m_attaques.Add(new Attaque("charge",2,1));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position , -Vector3.up*100,Color.red,5f);

        //ajout de la suppression dans la liste des joueurs du GameController
        if (this.m_stats.getPv() <= 0)
            GameController.RemovePlayer(this);
        //faire sauter le personnage
        if (Input.GetButton("Jump"))
            StartCoroutine(Mouvement.Jump(gameObject));
        //Deplacer le personnage
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
    }

    private Tile getCurrentTile()
    {
        return currentTile;
    }

    public Tile getTile()
    {
        Tile t = null;
        RaycastHit hit;
        //if (Physics.Raycast(transform.position/, -Vector3.up, out hit, 5))
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 5))
        {
            //Debug.Log("Hit Ray");
            t = hit.transform.gameObject.GetComponent<Tile>();
            t.empty = false;
            t.currentPlayer = this;
        }
        return t;
    }
    
    public void setTargetTile(Tile targetTile)
    {
        if (this.currentTile == null)
            this.currentTile = getTile();
        this.currentTile.empty = true;
        this.currentTile.currentPlayer = null;

        if (targetTile.tname != currentTile.tname)
        {
            this.targetTile = targetTile;
            //Liste des mouvements à effectuer pour bouger jusqu'a la target
            List<Tile> liste_move = new List<Tile>(); 
            getPath(liste_move);
            StartCoroutine(Mouvement.GoTO(gameObject, liste_move));
            
        }
    }


    private void getPath(List<Tile> path)
    {
        if (currentTile.m_voisins.Count > 0)
        {
            //Le premier voisin est par défault le premier choix 
            Tile NextMove = null;
            int tmp = 0;
            while (NextMove == null)
            {
                if (currentTile.m_voisins[tmp].empty == true)
                    NextMove = currentTile.m_voisins[tmp];
                else
                    tmp++;
                if(tmp==this.currentTile.m_voisins.Count)
                {
                    Debug.Log("Tout les voisins sont occupés");
                    return;
                }
            }
            //Mesure de la distance entre la case choisi et la case target
            float dist = Vector3.Distance(NextMove.transform.position, targetTile.transform.position);
            foreach (Tile t in currentTile.m_voisins)
            {
                float newdist = Vector3.Distance(t.transform.position, targetTile.transform.position);
                if (newdist < dist && t.empty==true)
                {
                    dist = newdist;
                    NextMove = t;
                }
            }
            path.Add(NextMove);
            this.currentTile = NextMove;
            //Debug.Log("Prochaine case : " + this.currentTile.tname);
            if (this.currentTile != this.targetTile)
                getPath(path);
            //setTargetTile(this.targetTile);
        }
    }  
    
    public void attack(Personnage cible)
    {

        Debug.Log("Distance de l'attaque : " + Tile.Distance(this.currentTile, cible.currentTile));
        //Empecher d'attaquer plus loin que la portée de l'attaque
        if (Tile.Distance(this.currentTile,cible.currentTile) > this.m_attaques[0].getRange())
        {
            Debug.Log("Hors de portée");
            return;
        }
        //Empecher de se cibler soi-même
        if (cible == this)
            return;
        //Calcul des dommages
        cible.m_stats.setPv(cible.m_stats.getPv() - this.m_attaques[0].getDammage());
        //joueur suivant
        if(!cible.IsDead())
            GameController.m_Instance.NextPlayer();
    }

    public bool IsDead()
    {
        return this.m_stats.getPv() <= 0;
    }
}
