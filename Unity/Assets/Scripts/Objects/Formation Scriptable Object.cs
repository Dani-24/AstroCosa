using UnityEngine;

[CreateAssetMenu(fileName = "FormationScriptableObject", menuName = "Scriptable Objects/New Formation")]
public class FormationScriptableObject : ScriptableObject
{
    public FormationSlot[] slots;

    public int minStage = 0;
    public int maxStage = 3;
}

[System.Serializable]
public class FormationSlot
{
    public Vector2 position;
    public EnemyScriptableObject enemy;
}