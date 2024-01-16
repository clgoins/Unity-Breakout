using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs;   // List of powerup prefabs to choose from
    [SerializeField] private float spawnChance;     // Chance that a powerup will spawn at all
    private List<int> powerupIndicies;  // List of indexes in powerupPrefabs array. Each index will appear in this list powerup.probability times


    private void Start()
    {
        // Listen for block destroyed events
        Block.OnBlockDestroyed += spawnNewPowerup;

        powerupIndicies = new List<int>();
        
        for (int i = 0; i < powerupPrefabs.Length; i++)
        {
            Powerup power = powerupPrefabs[i].GetComponent<Powerup>();

            // Add this index to powerupIndicies powerup.probability times
            for (int j = 0; j < power.probability; j++)
            {
                powerupIndicies.Add(i);
            }
        }
    }


    // Checks if a power up should be spawned, based on its spawn chance. 
    // Then determines the probability of a specific powerup spawning and instantiates it.
    void spawnNewPowerup(Vector2 position)
    {
        if (Random.Range(0.0f, 1.0f) <= spawnChance)
        {
            // Pick a random element from powerupIndicies
            int i = Mathf.FloorToInt(Random.Range(0.0f, powerupIndicies.Count));

            // Instantiate an object from the powerupPrefabs array, using the randomly selected index
            Instantiate(powerupPrefabs[powerupIndicies[i]], position, Quaternion.identity);
        }
    }


}
