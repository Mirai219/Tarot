using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int turn = 0;

    [SerializeField]
    public TarotPrefab[] tarotPrefabs; 
    public Dictionary<TarotType, GameObject> prefabDictionaryByType;

    [SerializeField]
    private Transform containerTransform;
   
    protected override void Awake()
    {
        base.Awake();        
        prefabDictionaryByType = new Dictionary<TarotType, GameObject>();
    }

    private void OnEnable()
    {
        //登记所有的预制体
        foreach (var tarotPrefab in tarotPrefabs)
        {
            if (!prefabDictionaryByType.ContainsKey(tarotPrefab.TarotType))
            {
                prefabDictionaryByType.Add(tarotPrefab.TarotType, tarotPrefab.Prefab);
            }
        }
    }

    private void Start()
    {
        for (int x = 0; x < Constant.columns; x++)
        {
            for (int y = 0; y < Constant.rows; y++)
            {
                TarotManager.Instance.CreateNewGameTarot(x, y, TarotType.None);
            }
        }       
        StartCoroutine(TarotManager.Instance.AllFill());
    }
}