using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_Instance;
    public static GameController Instance { get { return m_Instance; } }

    public enum PHASEACTION { ChoixDeplacement, Mouvement , ChoixAtt , ChoixCible}
    public PHASEACTION m_Phase;

    public List<Personnage> m_characters;
    private int m_actualPlayer;
    private Personnage m_currentPlayer;
    private int m_nbcharacters;

    private int nbTour;


    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);

        foreach (Transform character in GameObject.Find("Characters").transform)
        {
            if (character.gameObject.GetComponent<Personnage>() != null)
            {
                m_characters.Add(character.gameObject.GetComponent<Personnage>());
                //Debug.Log("Ajout de " + character.gameObject.name);
            }
        }
        m_nbcharacters = m_characters.Count;
        if (m_characters.Count > 0)
        {
            m_currentPlayer = m_characters[0];
            m_actualPlayer = 0;
            ToolsPannel.ChangeCurrentPlayerUI(GameController.m_Instance.m_currentPlayer);
        }
        else
            Debug.Log("Pas de personnages");
        nbTour = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.m_currentPlayer.IsDead())
        {
            RemovePlayer(m_currentPlayer);
            NextPlayer();
        }

        if(this.m_Phase == PHASEACTION.ChoixDeplacement)
            Tile.ShowDeplacementTile(GameController.m_Instance.m_currentPlayer);

    }

    public void NextPlayer()
    {
        if (m_nbcharacters > 1)
        {
            GameController.m_Instance.m_actualPlayer = (GameController.m_Instance.m_actualPlayer + 1) % GameController.m_Instance.m_nbcharacters;
            GameController.m_Instance.m_currentPlayer = GameController.m_Instance.m_characters[GameController.m_Instance.m_actualPlayer];
            ToolsPannel.ChangeCurrentPlayerUI(GameController.m_Instance.m_currentPlayer);
            this.nbTour++;
            ToolsPannel.ChangeTourUI(this.nbTour);
            GameController.m_Instance.m_Phase = PHASEACTION.ChoixDeplacement;
        }
    }

    //Retourne le personnage qui joue actuellement
    public Personnage GetCurrentPlayer()
    {
        /*
        Personnage player = null;
        player = GameObject.Find("Player").GetComponent<Personnage>();
        */
        Personnage player;
        player = GameController.m_Instance.m_currentPlayer.GetComponent<Personnage>();
        //GameController.m_Instance.NextPlayer();
        //Debug.Log("retour du perso : " + GameController.m_Instance.m_currentPlayer);
        return player;
    }

    public static void RemovePlayer(Personnage player)
    {
        foreach (Personnage p in GameController.m_Instance.m_characters)
        {
            if(p==player)
            {
                Debug.Log("Mort du personnage : " + p.name);
                //suppresion de la case
                p.getTile().empty = true;
                //on enleve le joueur de la liste
                GameController.m_Instance.m_characters.Remove(p);
                GameController.m_Instance.m_nbcharacters--;
                //Destruction du joueur
                Destroy(p.gameObject);
                return;
            }
        }
    } 
   
}
