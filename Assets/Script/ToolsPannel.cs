using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsPannel : MonoBehaviour
{

    public static void ChangeCurrentPlayerUI(Personnage p)
    {
        MenuManager.Panel play;
        foreach (MenuManager.Panel panel in MenuManager.m_Instance.m_Panels)
        {
            if (panel.NameData == GameManager.GAME_STATE.Play)
            {
                play = panel;
                Debug.Log("Mise a jour data");
                foreach (Transform child in play.PanelData.transform)
                {
                    foreach (Transform data in child)
                    {
                        Debug.Log(child.name);
                        if (data.name == "CurrentName")
                            data.GetComponent<Text>().text = "Name : " + p.pname;
                        if (data.name == "CurrentPv" && p.m_stats!=null)
                            data.GetComponent<Text>().text = p.m_stats.getPv() + " PV";
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
                Debug.Log(play.PanelData.name);
                foreach (Transform child in play.PanelData.transform)
                {
                    if (child.gameObject.name == "Tour")
                        child.gameObject.GetComponent<Text>().text = "Tour : "+tour;
                }
                //play.PanelData.GetComponent<Text>().text = "OUI OUI OUI";

            }
        }
    }
}
