using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject prefab;   //the prefab to spawn
        public float weight;        //the probability weight of spawning this prefab
    }

    [SerializeField] private List<Spawnable> spawnableItems = new List<Spawnable>();   //list of spawnable items

    private float totalWeight;  //total weight of all spawnable items

    //awake is called when the script instance is being loaded
    void Awake()
    {
        CalculateTotalWeight();  //calculate the total weight of all spawnable items
    }

    //start is called before the first frame update
    void Start()
    {
        //check if there are no items to spawn
        if (spawnableItems.Count == 0)
        {
            Debug.LogWarning("ItemSpawner has no items to spawn!");  //log a warning if there are no spawnable items
            return;  //exit the method if there are no items to spawn
        }

        //generate a random value within the range of total weight
        float pick = Random.value * totalWeight;

        //choose an item index based on the random value
        int chosenIndex = ChooseItemIndex(pick);

        //spawn the chosen item
        SpawnChosenItem(chosenIndex);
    }

    //calculate the total weight of all spawnable items
    void CalculateTotalWeight()
    {
        totalWeight = 0f;  //initialize total weight to zero

        //iterate through each spawnable item
        foreach (var spawnable in spawnableItems)
        {
            totalWeight += spawnable.weight;  //add the weight of each item to the total weight
        }
    }

    //choose an item index based on a random value
    int ChooseItemIndex(float pick)
    {
        int chosenIndex = 0;  //initialize the chosen index to the first item
        float cumulativeWeight = spawnableItems[0].weight;  //initialize cumulative weight to the weight of the first item

        //iterate through each spawnable item
        while (pick > cumulativeWeight && chosenIndex < spawnableItems.Count - 1)
        {
            chosenIndex++;  //move to the next item index
            cumulativeWeight += spawnableItems[chosenIndex].weight;  //add the weight of the current item to the cumulative weight
        }

        return chosenIndex;  //return the chosen index
    }

    //spawn the chosen item at the current position
    void SpawnChosenItem(int index)
    {
        //instantiate the chosen item's prefab at the current position with no rotation
        Instantiate(spawnableItems[index].prefab, transform.position, Quaternion.identity);
    }
}


