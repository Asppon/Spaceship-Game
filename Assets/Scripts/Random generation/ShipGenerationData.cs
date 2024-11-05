using UnityEngine;

[CreateAssetMenu(fileName = "ShipGenerationData.asset", menuName = "ShipGenerationData/Ship Data")]

public class ShipGenerationData : ScriptableObject
{
    public int numberOfCrawlers; //crawlers choose cartinal directions
    public int iterationMin;
    public int iterationMax; //limits for how far the crawlers go

}

