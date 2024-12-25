using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private TextMeshProUGUI BricksCountText;
    [SerializeField] private TextMeshProUGUI DropCountText;
    [SerializeField] private TextMeshProUGUI BricksToWinText;

    private bool isSettingsOpen = false;
    private int bricksCount = 0;
    private int dropCount = 0;
    private int bricksToWin;
    private int comboCount = 0;
    private int maxCombo = 0;


    void OnEnable()
    {
        GameEvents.OnBrickHit += UpdateBricksCountUI;
        GameEvents.OnBallDrop += UpdateDropUI;
        GameEvents.OnAllBricksDestroyed += OpenWinScreen;
        GameEvents.OnWallHit += ResetComboCount;
        GameEvents.TotalBricksCount += SetBricksToWin;
    }

    void OnDisable()
    {
        GameEvents.OnBrickHit -= UpdateBricksCountUI;
        GameEvents.OnBallDrop -= UpdateDropUI;
        GameEvents.OnAllBricksDestroyed -= OpenWinScreen;
        GameEvents.OnWallHit -= ResetComboCount;
        GameEvents.TotalBricksCount -= SetBricksToWin;
    }

    void Awake()
    {
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputListener();
    }

    private void SetBricksToWin(int bricksToWin)
    {
        this.bricksToWin = bricksToWin;
        BricksCountText.text = $"{bricksCount}/{bricksToWin}";
    }

    private void InputListener()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenInGameSettings();
        }
    }

    private void OpenWinScreen()
    {
        if(!isSettingsOpen)
        {
            Time.timeScale = 0;            
            WinScreen.SetActive(true);  
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateBricksCountUI()
    {
        bricksCount++;
        comboCount++;
        if(comboCount>maxCombo)
        {
            maxCombo = comboCount;
            BricksToWinText.text = $"x{maxCombo}";
        }
        BricksCountText.text = $"{bricksCount}/{bricksToWin}";
    }

    private void ResetComboCount()
    {
        comboCount=0;
    }

    private void UpdateDropUI()
    {
        dropCount++;
        DropCountText.text = $"Drops:{dropCount}";
    }

    public void OpenInGameSettings()
    {
        if(isSettingsOpen)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
            isSettingsOpen = false;
        }
        else
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
            isSettingsOpen = true;
        }
    }

}
