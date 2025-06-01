using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCustomizer : MonoBehaviour
{
    [Header("Town Layout Settings")]
    public int maxHouses = 10;
    public float minSpacing = 5f;
    public float placementRadius = 20f;

    private void Start()
    {
        CustomizeTown();
    }

    public void CustomizeTown()
    {
        Transform[] allHouses = GetComponentsInChildren<Transform>(true);
        List<Vector3> usedPositions = new List<Vector3>();

        int housesPlaced = 0;

        foreach (Transform house in allHouses)
        {
            // Skip self
            if (house == transform) continue;

            // Randomly decide whether to activate this house
            if (housesPlaced >= maxHouses || Random.value > 0.6f)
            {
                house.gameObject.SetActive(false);
                continue;
            }

            // Try to find a non-overlapping spot
            Vector3 randomPos;
            int maxTries = 10;
            int tries = 0;
            do
            {
                float angle = Random.Range(0f, 360f);
                float distance = Random.Range(0f, placementRadius);
                randomPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

                tries++;
            } while (IsTooClose(randomPos, usedPositions) && tries < maxTries);

            if (tries >= maxTries)
            {
                house.gameObject.SetActive(false);
                continue;
            }

            // Place and rotate the house
            house.localPosition = randomPos;
            house.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            house.gameObject.SetActive(true);

            usedPositions.Add(randomPos);
            housesPlaced++;
        }
    }

    private bool IsTooClose(Vector3 position, List<Vector3> usedPositions)
    {
        foreach (var used in usedPositions)
        {
            if (Vector3.Distance(position, used) < minSpacing)
                return true;
        }
        return false;
    }
}
