using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController m_Instance;
    public static GameController Instance { get { return m_Instance; } }

    public enum PHASEACTION {None,ChoixAction,ChoixDeplacement, Mouvement , ChoixCible , ListAtt , ListObj,ListSort}
    public PHASEACTION m_Phase;
    public event Action<PHASEACTION> OnGamePhaseChange;

    //Booleen pour savoir si la partie a ete lancée
    public bool IsGameStarted;

    public List<Personnage> m_characters;
    private int m_actualPlayer;
    private Personnage m_currentPlayer;
    private int m_nbcharacters;

    private int nbTour;

    //si le joueur s'est déja deplacé
    public bool hasMove;
    public bool hasAtt;



    public void ChangePhase(PHASEACTION phase)
    {
        m_Phase = phase;
        if (OnGamePhaseChange != null)
            OnGamePhaseChange(m_Phase);
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (MenuManager.Instance.IsReady == false)
            yield return null;
        ChangePhase(PHASEACTION.None);

    }

    //Melanger les elements d'une liste de personnage
    public static List<Personnage> Randomize<Personnage>(List<Personnage> list)
    {
        List<Personnage> randomizedList = new List<Personnage>(); System.Random rnd = new System.Random(); while (list.Count > 0)
        {
            int index = rnd.Next(0, list.Count); //pick a random item from the master list 
            randomizedList.Add(list[index]); //place it at the end of the randomized list
            list.RemoveAt(index); 
        }
        return randomizedList; 
    }

      
    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
        /*
        foreach (Transform character in GameObject.Find("Characters").transform)
        {
            if (character.gameObject.GetComponent<Personnage>() != null)
            {
                m_characters.Add(character.gameObject.GetComponent<Personnage>());
            }
        }
        */
        
        foreach (Transform character in GameObject.Find("PlayerTeam").transform)
        {
            if (character.gameObject.GetComponent<Personnage>() != null)
            {
                m_characters.Add(character.gameObject.GetComponent<Personnage>());
            }
        }
        foreach (Transform character in GameObject.Find("EnnemiTeam").transform)
        {
            if (character.gameObject.GetComponent<Personnage>() != null)
            {
                m_characters.Add(character.gameObject.GetComponent<Personnage>());
            }
        }
        //melanger l'ordre
        m_characters = Randomize(m_characters);
        m_nbcharacters = m_characters.Count;
        if (m_characters.Count > 0)
        {
            m_currentPlayer = m_characters[0];
            m_actualPlayer = 0;
            hasMove = false;
            hasAtt = false;
            ToolsPannel.ChangeCurrentPlayerUI(GameController.m_Instance.m_currentPlayer);
            ChangePhase(PHASEACTION.ChoixAction);
        }
        else
            Debug.Log("Pas de personnages");
        nbTour = 0;

    }


    // Update is called once per frame
    void Update()
    {

        if (this.m_currentPlayer.IsDead())
        {
            RemovePlayer(m_currentPlayer);
            NextPlayer();
        }

        if (this.m_Phase == PHASEACTION.ChoixDeplacement)
            Tile.ShowDeplacementTile(GameController.m_Instance.m_currentPlayer);
        if (GameManager.m_Instance.m_State == GameManager.GAME_STATE.Play)
            ToolsPannel.ChangePhaseUI();

        Transform[] ennemi = GameObject.Find("EnnemiTeam").transform.GetComponentsInChildren<Transform>();
        Debug.Log(ennemi.Length);
        
    }

    public void NextPlayer()
    {
        Plateau.m_Instance.resetColorAllTile();
        if (m_nbcharacters >= 2)
        {
            Tile.HideDeplacementTile(GameController.m_Instance.m_currentPlayer);
            GameController.m_Instance.m_actualPlayer = (GameController.m_Instance.m_actualPlayer + 1) % GameController.m_Instance.m_nbcharacters;
            GameController.m_Instance.m_currentPlayer = GameController.m_Instance.m_characters[GameController.m_Instance.m_actualPlayer];
            ToolsPannel.ChangeCurrentPlayerUI(GameController.m_Instance.m_currentPlayer);
            this.nbTour++;
            ToolsPannel.ChangeTourUI(this.nbTour);
            hasMove = false;
            hasAtt = false;
            ChangePhase(PHASEACTION.ChoixAction);
        }
    }

    //Retourne le personnage qui joue actuellement
    public Personnage GetCurrentPlayer()
    {
        Personnage player;
        player = GameController.m_Instance.m_currentPlayer.GetComponent<Personnage>();

        return player;
    }

    public static void RemovePlayer(Personnage player)
    {
        foreach (Personnage p in GameController.m_Instance.m_characters)
        {
            if(p==player)
            {
                //Debug.Log("Mort du personnage : " + p.name);
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

    private void ShowListAtt()
    {
        foreach (UIStruct.UIPanel uip in MenuManager.m_Instance.m_UIPanels)
        {
            if (uip.NameData == GameController.PHASEACTION.ListAtt)
            {
                int posy = -450;
                foreach (Attaque att in m_currentPlayer.m_attaques)
                {
                    var button = (GameObject)Instantiate(Resources.Load("AttButton", typeof(GameObject))) as GameObject;
                    button.GetComponentInChildren<Text>().font = Resources.Load<Font>("font/8-bit-pusab") as Font;
                    button.GetComponentInChildren<Text>().resizeTextForBestFit = true;

                    button.transform.SetParent(uip.PanelData.transform);
                    button.GetComponent<RectTransform>().localPosition = new Vector3(-700, posy, 0);
                    posy = posy + 75;
                    button.GetComponentInChildren<Text>().text = att.getName();
                    button.GetComponent<Button>().onClick.AddListener(() => {m_currentPlayer.m_currentAtt=att; ShowCible(att); });
                }
            }
        }
        
    }


    public void ShowCible(Attaque att)
    {
        if(att.getCost()>m_currentPlayer.m_stats.getPm())
        {
            Debug.Log("Pas assez de mana");
            return;
        }
        Debug.Log("Lancement de l'attaque " + att.getName());
        ChangePhase(PHASEACTION.ChoixCible);
        List<Tile> listCible = new List<Tile>();
        switch(att.getType())
        {
            case Attaque.RANGE_TYPE.Line:
                for(int i=0;i<=att.getRange();i++)
                {
                    Tile t = Tile.getVoisinTile(m_currentPlayer.currentTile , m_currentPlayer.currentTile.x.ToString() + "_" + (m_currentPlayer.currentTile.z + i).ToString());
                    if (t != null)
                        listCible.Add(t);
                    t = Tile.getVoisinTile(m_currentPlayer.currentTile, m_currentPlayer.currentTile.x.ToString() + "_" + (m_currentPlayer.currentTile.z - i).ToString());
                    if (t != null)
                        listCible.Add(t);
                    t = Tile.getVoisinTile(m_currentPlayer.currentTile, (m_currentPlayer.currentTile.x+i).ToString() + "_" + (m_currentPlayer.currentTile.z).ToString());
                    if (t != null)
                        listCible.Add(t);
                    t = Tile.getVoisinTile(m_currentPlayer.currentTile, (m_currentPlayer.currentTile.x-i).ToString() + "_" + (m_currentPlayer.currentTile.z).ToString());
                    if (t != null)
                        listCible.Add(t);
                }
                break;
        }
        foreach(Tile t in listCible)
        {
            t.color = true;
        }
        att.setCibles(listCible);
           
    }

    public void GoOnMovePhase()
    {
        ChangePhase(PHASEACTION.ChoixDeplacement);

    }
    public void GoOnActionPhase()
    {
        if (m_Phase == PHASEACTION.ChoixDeplacement)
            Tile.HideDeplacementTile(m_currentPlayer);
        if(m_Phase == PHASEACTION.ChoixCible)
        {
            m_currentPlayer.m_currentAtt.resetCibles();
            m_currentPlayer.m_currentAtt = null;
        }
        Plateau.m_Instance.resetColorAllTile();
        ChangePhase(PHASEACTION.ChoixAction);

    }
    public void GoOnListAttPhase()
    {
        if (!hasAtt)
        {
            ChangePhase(PHASEACTION.ListAtt);
            ShowListAtt();
        }
    }

}
