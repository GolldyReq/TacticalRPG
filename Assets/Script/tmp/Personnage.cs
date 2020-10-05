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
    [SerializeField]public string pname;

    public Statistiques m_stats;
    public List<Attaque> m_attaques;
    public Attaque m_currentAtt;

    public int pvMax;
    public int pmMax;

    public List<Tile> tileToGo;



    public Personnage(string name, Statistiques stat,List<Attaque> latt) 
    {
        this.pname = name;
        this.m_stats = stat;
        this.m_attaques = latt;
        

    }


    // Start is called before the first frame update
    void Start()
    {
        /*
        IsMoving = false;
        this.currentTile = getTile();
        targetTile = null;
        
        if (this.pname == null)
            this.pname = "player";
        m_stats = new Statistiques(5,3,2);
        pvMax = m_stats.getPv();
        pmMax = m_stats.getPm();
        m_attaques = new List<Attaque>();
        m_attaques.Add(new Attaque("charge",2,2));
        m_attaques.Add(new Attaque("Glucoup", 3, 1, Attaque.RANGE_TYPE.Line,3));
        
        tileToGo = new List<Tile>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.m_Instance.m_State != GameManager.GAME_STATE.Play)
            return;

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
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 5))
        {
            t = hit.transform.gameObject.GetComponent<Tile>();
            t.empty = false;
            t.currentPlayer = this;
        }
        return t;
    }
    
    //Deplacement du personnage
    public void setTargetTile(Tile targetTile)
    {
        if (Tile.Distance(this.currentTile, targetTile) > this.m_stats.getMobility())
            return;

        if (this.currentTile == null)
            this.currentTile = getTile();

        Plateau.m_Instance.resetColorAllTile();
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

    //Ajoute récursivement dans une liste le chemin a parcourir pour atteindre la case target
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
                if (newdist < dist && t.empty==true && !path.Contains(t))
                {
                    dist = newdist;
                    NextMove = t;
                }
            }
            path.Add(NextMove);
            this.currentTile = NextMove;
            if (this.currentTile != this.targetTile)
                getPath(path);
        }
    }  
    
    public void attack(Personnage cible)
    {

        //Empecher d'attaquer plus loin que la portée de l'attaque
        if (!m_currentAtt.getCibles().Contains(cible.currentTile))
        {
            Debug.Log("Hors de portée");
            return;
        }
        //Empecher de se cibler soi-même
        if (cible == this)
            return;
        //Calcul des dommages
        cible.m_stats.setPv(cible.m_stats.getPv() - m_currentAtt.getDammage());
        //Diminution du mana
        this.m_stats.setPm(this.m_stats.getPm() - m_currentAtt.getCost());
        //joueur suivant
        m_currentAtt.resetCibles();
        m_currentAtt = null;

        GameController.m_Instance.hasAtt = true;
        GameController.m_Instance.ChangePhase(GameController.PHASEACTION.ChoixAction);
        //ToolsPannel.EraseSelectedPlayerUI();
        //GameController.m_Instance.NextPlayer();
    }

    public int getPvMax() { return pvMax; }
    public int getPv() { return m_stats.getPv(); }
    public int getPmMax() { return pmMax; }
    public int getPm() { return m_stats.getPm(); }
    public bool IsDead()
    {
        return this.m_stats.getPv() <= 0;
    }

}
