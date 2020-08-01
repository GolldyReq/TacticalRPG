using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    public enum GAME_STATE {MenuPrincipal,Play,Pause,Loading, Victory , GameOver , Equipe , TerrainView }
    public GAME_STATE m_State;

    public event Action<GAME_STATE> OnGameStateChange;


    public bool IsPlaying { get { return m_State == GAME_STATE.Play; } }

    void ChangeState(GAME_STATE state)
    {
        m_State = state;
        if (OnGameStateChange != null)
            OnGameStateChange(m_State);
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        //if (Input.GetButton("Pause"))
            //ChangeState(GAME_STATE.Pause);
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (MenuManager.Instance.IsReady == false)
            yield return null;
        

        MenuManager.Instance.OnStartPlayButtonHasBeenClicked += OnStartPlayButtonHasBeenClicked;
        ChangeState(GAME_STATE.MenuPrincipal);
    }

    private void OnStartPlayButtonHasBeenClicked()
    {
        Debug.Log("J'appuie la sur le bouton");
        ChangeState(GAME_STATE.Play);
        ToolsPannel.ChangeCurrentPlayerUI(GameController.m_Instance.GetCurrentPlayer());
        ToolsPannel.ChangeTourUI(0);
    }
}
