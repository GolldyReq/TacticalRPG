using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsPannel : MonoBehaviour
{

    void Update()
    {
        
    }


    public static void ChangeCurrentPlayerUI(Personnage p)
    {


        if (GameManager.m_Instance.m_State != GameManager.GAME_STATE.Play)
            return;
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                //Debug.Log("Mise a jour data");
                foreach (Transform child in play.PanelData.transform)
                {
                    foreach (Transform data in child)
                    {
                        //Debug.Log(child.name);
                        if (data.name == "CurrentName")
                            data.GetComponent<Text>().text = "Name : " + p.pname;
                        if (data.name == "CurrentPv" && p.m_stats!=null)
                            data.GetComponent<Text>().text = p.m_stats.getPv() + " PV";
                        if (data.name == "CurrentPm" && p.m_stats != null)
                            data.GetComponent<Text>().text = p.m_stats.getPm() + " PM";
                        if(data.name == "CurrentHealthBar")
                            data.GetComponent<Image>().fillAmount = (float)Mathf.Clamp(p.getPv(), 0, p.getPvMax()) / p.getPvMax();
                        if (data.name == "CurrentManaBar")
                            data.GetComponent<Image>().fillAmount = (float)Mathf.Clamp(p.getPm(), 0, p.getPmMax()) / p.getPmMax();

                    }
                }
            }
        }
    }
    public static void ChangeSelectedPlayerUI(Personnage p)
    {
        Debug.Log(p.pname);
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                //Debug.Log("Mise a jour data");
                foreach (Transform child in play.PanelData.transform)
                {
                    foreach (Transform data in child)
                    {
                        //Debug.Log(child.name);
                        if (data.name == "SelectedName")
                            data.GetComponent<Text>().text = "Name : " + p.pname;
                        if (data.name == "SelectedPv" && p.m_stats != null)
                            data.GetComponent<Text>().text = p.m_stats.getPv() + " PV";
                        if (data.name == "SelectedPm" && p.m_stats != null)
                            data.GetComponent<Text>().text = p.m_stats.getPm() + " PM";
                        if (data.name == "SelectedHealthBar")
                        {
                            data.GetComponent<Image>().gameObject.SetActive(true);
                            data.GetComponent<Image>().fillAmount = (float)Mathf.Clamp(p.getPv(), 0, p.getPvMax()) / p.getPvMax();
                        }
                        if (data.name == "SelectedManaBar")
                        {
                            data.GetComponent<Image>().gameObject.SetActive(true);
                            data.GetComponent<Image>().fillAmount = (float)Mathf.Clamp(p.getPm(), 0, p.getPmMax()) / p.getPmMax();
                        }
                        if (data.name == "SelectedTotalHealthBar")
                            data.GetComponent<Image>().gameObject.SetActive(true);
                        if (data.name == "SelectedTotalManaBar")
                            data.GetComponent<Image>().gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public static void EraseSelectedPlayerUI()
    {
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                //Debug.Log("Mise a jour data");
                foreach (Transform child in play.PanelData.transform)
                {
                    foreach (Transform data in child)
                    {
                        //Debug.Log(child.name);
                        if (data.name == "SelectedName")
                            data.GetComponent<Text>().text = ""; 
                        if (data.name == "SelectedPv")
                            data.GetComponent<Text>().text = "";
                        if (data.name == "SelectedPm")
                            data.GetComponent<Text>().text = "";
                        if (data.name == "SelectedHealthBar")
                            data.GetComponent<Image>().gameObject.SetActive ( false );
                        if (data.name == "SelectedManaBar")
                            data.GetComponent<Image>().gameObject.SetActive(false);
                        if (data.name == "SelectedTotalHealthBar")
                            data.GetComponent<Image>().gameObject.SetActive(false);
                        if (data.name == "SelectedTotalManaBar")
                            data.GetComponent<Image>().gameObject.SetActive(false);
                    }
                }
            }
        }
    }


    public static void ChangeTourUI(int tour)
    {
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                //Debug.Log(play.PanelData.name);
                foreach (Transform child in play.PanelData.transform)
                {
                    if (child.gameObject.name == "Tour")
                        child.gameObject.GetComponent<Text>().text = "Tour : "+tour;
                }
                //play.PanelData.GetComponent<Text>().text = "OUI OUI OUI";

            }
        }
    }

    public static void ChangePhaseUI()
    {
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                //Debug.Log(play.PanelData.name);
                foreach (Transform child in play.PanelData.transform)
                {
                    if (child.gameObject.name == "Phase")
                    {
                        switch (GameController.m_Instance.m_Phase)
                        {
                            case GameController.PHASEACTION.ChoixDeplacement:
                                child.gameObject.GetComponent<Text>().text = "Phase : Choix Cases" ;
                                break;
                            case GameController.PHASEACTION.Mouvement:
                                child.gameObject.GetComponent<Text>().text = "Phase : En Mouvement";
                                break;
                            case GameController.PHASEACTION.ChoixAction:
                                child.gameObject.GetComponent<Text>().text = "Phase : Choix Action";
                                break;
                            case GameController.PHASEACTION.ChoixCible:
                                child.gameObject.GetComponent<Text>().text = "Phase : Choix Cible";
                                break;
                            case GameController.PHASEACTION.ListAtt:
                                child.gameObject.GetComponent<Text>().text = "Phase : Choix Attaque";
                                break;

                        }
                    }
                }
                //play.PanelData.GetComponent<Text>().text = "OUI OUI OUI";

            }
        }
       
    }
}
