using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public static MenuManager m_Instance;
    public static MenuManager Instance { get { return m_Instance; } }

    bool m_IsReady;
    public bool IsReady { get { return m_IsReady; } }
    List<GameObject> m_Panels = new List<GameObject>();

    EventSystem eventSystem;

    public event Action OnStartPlayButtonHasBeenClicked;
    void GameStateChange(GameManager.GAME_STATE state)
    {
        switch (state)
        {
            
        }
    }
    private void Awake()
    {

        m_IsReady = false;
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);

        eventSystem = GameObject.FindObjectOfType<EventSystem>();


    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChange += GameStateChange;
        m_IsReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonHasBeenClicked()
    {
        if (OnStartPlayButtonHasBeenClicked != null) OnStartPlayButtonHasBeenClicked();
    }
}
