
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

    [SerializeField] int increaseDifficultyScore = 200;
    int actualScoreAdded = 0;

    [Header("Enemies")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<GameObject> enemySquadsAlive = new();
    [SerializeField] FormationScriptableObject[] formationsAvailable;
    public GameObject enemyPrefab;

    [Header("Canvas")]
    [SerializeField] TMP_Text txt_score;
    [SerializeField] TMP_Text txt_totalScore;
    [SerializeField] TMP_Text txt_stage;
    [SerializeField] TMP_Text txt_difficulty;
    [SerializeField] Slider slider_timer;

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
        if (enemySquadsAlive.Count <= 0)
        {
            var emptyGO = new GameObject();
            emptyGO.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            emptyGO.AddComponent<SquadController>();
            emptyGO.GetComponent<SquadController>().squadFormation = GetNextFormation();

            enemySquadsAlive.Add(emptyGO);
        }

        if( currentTimer > 0 && stage <= 3)
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
            totalScore = score;
            txt_totalScore.text = $"Total: {totalScore:000000}";

            score = 0;
            AddScore(0);

            stage++;
            txt_stage.text = stage > 3 ? $"Stage: boss" : $"Stage: {stage}";
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
        txt_difficulty.text = $"Difficulty: {difficulty:000}%";
    }
}
