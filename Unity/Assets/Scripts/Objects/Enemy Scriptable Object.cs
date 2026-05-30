using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/New Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int HP;
    public int DMG;
    public int EXP;

    public Sprite sprite;
    public EnemyType type;

    public float moveDuration;
    public float specialMoveSpeed;
    public float specialMoveAmpl;
}

[System.Serializable]
public enum EnemyType
{
    Kamikaze,
    ZigZag,
    Hunter
}