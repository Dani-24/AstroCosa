using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Variables")]
    public int stage = 0;
    public int score = 0;
    public int totalScore = 0;
    public float difficulty = 10;

    public int timePerStage = 120;
    float currentTimer;

    public bool gameEnded = false;
    bool bossSpawned = false;

    [SerializeField] int increaseDifficultyScore = 200;
    int actualScoreAdded = 0;

    [Header("Enemies")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<GameObject> enemySquadsAlive = new();
    [SerializeField] FormationScriptableObject[] formationsAvailable;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    [Header("Canvas")]
    [SerializeField] CanvasGroup panelHUD;
    [SerializeField] TMP_Text txt_score;
    [SerializeField] TMP_Text txt_totalScore;
    [SerializeField] TMP_Text txt_stage;
    [SerializeField] TMP_Text txt_difficulty;
    [SerializeField] Slider slider_timer;

    [Header("Endgame Canvas")]
    [SerializeField] CanvasGroup panelEndGame;
    [SerializeField] TMP_Text txt_summary;

    [Header("Player instance (autoset on start)")]
    public GameObject playerInstance;

    #region Instance

    private static GameManagerScript _instance;
    public static GameManagerScript Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && Instance != null)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    void Start()
    {
        currentTimer = timePerStage;

        txt_score.text = $"Score: {score:000000}";
        txt_totalScore.text = $"Total: {totalScore:000000}";
        txt_stage.text = $"Stage: {stage}";
        txt_difficulty.text = $"Difficulty: {difficulty:000}%";

        slider_timer.maxValue = timePerStage;
        slider_timer.value = currentTimer;
    }

    void Update()
    {
        if (gameEnded || bossSpawned) return;

        if (enemySquadsAlive.Count <= 0)
        {
            if (stage > 3)
            {
                bossSpawned = true;
                panelHUD.DOFade(0f, 2f);
                Instantiate(bossPrefab);
                return;
            }

            var emptyGO = new GameObject();
            emptyGO.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            emptyGO.AddComponent<SquadController>();
            emptyGO.GetComponent<SquadController>().squadFormation = GetNextFormation();

            enemySquadsAlive.Add(emptyGO);
        }

        if (currentTimer > 0 && stage <= 3)
        {
            currentTimer -= Time.deltaTime;
            slider_timer.value = currentTimer;
        }
        else if (currentTimer <= 0)
        {
            // TODO: Next Stage visual Feedback

            currentTimer = timePerStage;
            slider_timer.value = currentTimer;

            // Reset Score
            totalScore += score;
            txt_totalScore.text = $"Total: {totalScore:000000}";

            score = 0;
            AddScore(0);

            stage++;
            txt_stage.text = stage > 3 ? $"Stage: ???" : $"Stage: {stage}";
            IncreaseDifficulty(20);
        }
    }

    FormationScriptableObject GetNextFormation()
    {
        bool formationSelected = false;
        int selectedFormation;
        do
        {
            selectedFormation = Random.Range(0, formationsAvailable.Length);
            if (formationsAvailable[selectedFormation].minStage <= stage
                && formationsAvailable[selectedFormation].maxStage >= stage)
            {
                formationSelected = true;
            }
        } while (!formationSelected);

        return formationsAvailable[selectedFormation];
    }

    public void RemoveSquad(GameObject squad)
    {
        AddScore(1000);
        enemySquadsAlive.Remove(squad);
    }

    public void AddScore(int pts)
    {
        score += pts;
        txt_score.text = $"Score: {score:000000}";

        actualScoreAdded += pts;
        if (actualScoreAdded > increaseDifficultyScore)
        {
            actualScoreAdded = 0;
            IncreaseDifficulty(1);
        }
    }

    public void IncreaseDifficulty(int increment)
    {
        difficulty += increment;
        txt_difficulty.text = $"Difficulty: {difficulty}%";
    }

    public void EndGame()
    {
        playerInstance = null;
        gameEnded = true;

        Debug.Log("Game finished");

        Sequence seq = DOTween.Sequence();

        seq.Append(panelHUD.DOFade(0f, 2f))
           .Append(panelEndGame.DOFade(0.9f, 2f))
           .OnComplete(() =>
           {
               panelEndGame.blocksRaycasts = true;
               panelEndGame.interactable = true;
           });
        
        string stageText = stage > 3 ? "boss" : $"{stage}";

        totalScore += score;
        txt_summary.text = $"Final Score: {totalScore:000000}\r\n\r\nStage Reached: {stageText}\r\nDifficulty: {difficulty}%";
    }
}
