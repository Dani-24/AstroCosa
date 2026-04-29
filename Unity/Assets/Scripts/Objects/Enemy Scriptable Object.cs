using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int HP;
    public int DMG;
    public int EXP;

    public Sprite sprite;
    public EnemyType type;
}

[System.Serializable]
public enum EnemyType
{
    Kamikaze,
    ZigZag,
    Hunter
}