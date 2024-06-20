using UnityEngine;

[System.Serializable]
public struct TarotPrefab
{
    public TarotType TarotType;
    public GameObject Prefab;

    public static  TarotPrefab GetRandomTarotPrefab(TarotPrefab[] tarotPrefabs)
    {
        int randomIndex = Random.Range(1, tarotPrefabs.Length);
        return tarotPrefabs[randomIndex];
    }
}