using UnityEngine;

[CreateAssetMenu(fileName = "FormationScriptableObject", menuName = "Scriptable Objects/New Formation")]
public class FormationScriptableObject : ScriptableObject
{
    public FormationSlot[] slots;

    public int minStage = 0;
    public int maxStage = 3;

    public float spawnDuration = 5f;
    public float wanderFreq = 0.1f;
    public float wanderAmpl = 2f;
}

[System.Serializable]
public class FormationSlot
{
    public Vector2 position;
    public EnemyScriptableObject enemy;
}