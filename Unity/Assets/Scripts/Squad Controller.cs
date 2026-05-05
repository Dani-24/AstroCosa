using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public List<GameObject> enemies = new();
    public FormationScriptableObject squadFormation;

    // TODO: Move this values to Formation ScriptableObject
    float appearDuration = 5f;
    float frequency = 0.1f;
    float amplitude = 2f;

    public bool wandering = false;

    void Start()
    {
        // Create enemies
        foreach (var slot in squadFormation.slots)
        {
            GameObject enemySlot = Instantiate(GameManagerScript.Instance.enemyPrefab, transform);
            enemySlot.transform.SetLocalPositionAndRotation(slot.position, Quaternion.identity);
            enemySlot.GetComponent<EnemyController>().squad = this;
            enemySlot.GetComponent<EnemyController>().originalPosInSquad = slot.position;
            enemySlot.GetComponent<EnemyController>().enemyData = slot.enemy;
            enemies.Add(enemySlot);
        }

        // Move from origin to 0,0. Then moves only on X a bit
        transform.DOMove(new Vector3(0f, 0f, transform.position.z), appearDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(StartWander);
    }

    void StartWander()
    {
        DOVirtual.Float(0f, Mathf.PI * 2f, 1f / frequency, t =>
        {
            float x = Mathf.Sin(t) * amplitude;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        })
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

        wandering = true;
    }

    void Update()
    {
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count <= 0)
        {
            GameManagerScript.Instance.RemoveSquad(this.gameObject);
        }
    }
}
