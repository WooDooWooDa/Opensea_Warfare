using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

public class Main : MonoBehaviour
{
    private Main()
    {
        m_data = new SaveData();
    }

    public static Main Instance { get => m_instance; }
    
    private static Main m_instance;

    private bool m_initialized = false;
    public bool IsInitialized => m_initialized;
    public Action OnInitialized;
    public GameInputs.BattleMapActions BattleMapInputs => m_inputActions.BattleMap;
    private SaveData m_data;

    private SaveManager m_saveManager = null;

    private List<Manager> m_managers = new List<Manager>();
    private GameInputs m_inputActions;

    private void Awake()
    {
        if (m_instance == null) {
            m_instance = this;
        }

        Initialize();
    }

    private void Initialize()
    {
        if (m_initialized)
            return;

        m_inputActions = new GameInputs();

        //Main managers
        m_saveManager = gameObject.AddComponent<SaveManager>();
        //Game related managers
        m_managers.Add(gameObject.GetComponentInChildren<PlayerFleet>());
        m_managers.Add(FindObjectOfType<ScreenManager>());

        m_saveManager.Initialize();
        foreach (Manager manager in m_managers) {
            manager.Initialize();
        }

        LoadGame();

        m_initialized = true;
        OnInitialized?.Invoke();
    }

    public void RegisterManager(Manager m)
    {
        m_managers.Add(m);
    } 

    public T GetManager<T>()
    {
        foreach (var manager in m_managers.Where(manager => manager.GetType() == typeof(T)).OfType<T>())
        {
            return (T)Convert.ChangeType(manager, typeof(T));
        }
        return default;
    }

    private bool LoadGame()
    {
        if (!m_saveManager.Load(ref m_data)) {
            Debug.Log("Failed to load or create save file");
            return false;
        }

        foreach (Manager manager in m_managers) {
            //manager.Load(ref m_data);
        }

        return true;
    }

    private void SaveGame()
    {
        foreach (Manager manager in m_managers) {
            //manager.Save(ref m_data);
        }
        m_saveManager.Save(ref m_data);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void OnDestroy()
    {
        foreach (Manager manager in m_managers) {
            manager.OnDestroy();
        }
    }
}
