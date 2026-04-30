using UnityEngine;

public class FormationBuilder : MonoBehaviour
{
    public FormationScriptableObject formation;

    [ContextMenu("Save Formation")]
    public void Save()
    {
        var children = GetComponentsInChildren<Transform>();
        formation.slots = new FormationSlot[children.Length - 1];

        for (int i = 1; i < children.Length; i++)
        {
            var child = children[i];

            formation.slots[i - 1] = new FormationSlot
            {
                position = child.localPosition,
                enemy = child.GetComponent<EnemyPlaceholder>().enemy
            };
        }
    }
}
