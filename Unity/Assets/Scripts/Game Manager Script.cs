
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int stage = 0;
    public int score = 0;
    public float difficulty = 10;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<GameObject> enemySquadsAlive = new();
    [SerializeField] FormationScriptableObject[] formationsAvailable;
    public GameObject enemyPrefab;

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
}
