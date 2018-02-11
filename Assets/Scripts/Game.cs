using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    #region UI

    public GameObject mainMenu, deathScreen;
    public Button newGameButton;
    public Text scoreText, recordText;

    #endregion

    public static Game Instance { get; private set; }
    public Settings settings;
    public Transform grass;

    [HideInInspector]
    public int  gamePoints;
    int gameRecord;
    [HideInInspector]
    public bool isPlaying;

    Soldier soldier;
    ZombieSpawner spawner;  

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        newGameButton.onClick.AddListener(NewGame);
        soldier = GameObject.Find("Soldier").GetComponent<Soldier>();
        spawner = GameObject.Find("Spawner").GetComponent<ZombieSpawner>();
    }

    private void Start()
    {
        gameRecord = 0;

        if (settings.isSaving)
            Load();

        spawner.settings = settings;
        soldier.settings = settings;        
    }

    #region new Game

    void NewGame()
    {
        isPlaying = true;
        mainMenu.SetActive(false);
        gamePoints = 0;
        spawner.StartSpawn(soldier.transform.position);

        StartCoroutine(soldier.Shot());
        StartCoroutine(spawner.TimeCoroutine());
    }

    public void DeathScreen()
    {
        isPlaying = false;
        mainMenu.SetActive(true);
        deathScreen.SetActive(true);
        spawner.KillAll();

        if (gamePoints > gameRecord)
        {
            gameRecord = gamePoints;
            scoreText.text = "Новый рекорд " + gamePoints + " очков!";
            recordText.text = "Ваш рекорд : " + gameRecord;
        }

        else
        {
            scoreText.text = "Вы набрали " + gamePoints + " очков";
        }

        if (settings.isSaving)
            Save();        
    }

    void Save()
    {
        PlayerPrefs.SetInt("record", gameRecord);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("record"))
        {
            gameRecord = PlayerPrefs.GetInt("record");
            recordText.text = "Ваш рекорд : " + gameRecord;
        }
    }

    #endregion
}
