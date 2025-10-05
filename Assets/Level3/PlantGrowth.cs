using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [Header("Assign Plant Models")]
    public GameObject smallModel;
    public GameObject mediumModel;
    public GameObject largeModel;

    private int stage = 1; // 1 = small, 2 = medium, 3 = large

    public void Grow()
    {
        stage++;
        if (stage > 3) stage = 3;
        UpdateModels();
    }

    private void UpdateModels()
    {
        if (smallModel) smallModel.SetActive(stage == 1);
        if (mediumModel) mediumModel.SetActive(stage == 2);
        if (largeModel) largeModel.SetActive(stage == 3);
    }
}
