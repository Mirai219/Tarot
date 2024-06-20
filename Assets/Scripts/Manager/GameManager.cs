using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public int turn = 0;

    [SerializeField]
    public TarotPrefab[] allTarotPrefabs;
    //public int[] selectedTarotIndex;
    //public TarotPrefab[] selectedTarotPrefabs;   

    public Dictionary<TarotType, GameObject> prefabDictionaryByType;

    [SerializeField]
    private Transform containerTransform;
   
    protected override void Awake()
    {
        base.Awake();        
        prefabDictionaryByType = new Dictionary<TarotType, GameObject>();
        //selectedTarotIndex = new int[Constant.selectedTaritLimit];
        //selectedTarotPrefabs = new TarotPrefab[Constant.selectedTaritLimit];
    }

    private void OnEnable()
    {
        //登记所有的预制体
        foreach (var tarotPrefab in allTarotPrefabs)
        {
            if (!prefabDictionaryByType.ContainsKey(tarotPrefab.TarotType))
            {
                prefabDictionaryByType.Add(tarotPrefab.TarotType, tarotPrefab.Prefab);
            }
        }

        //for (int i = 0; i < selectedTarotIndex.Length; i++)
        //{
            //TarotPrefab tarotPrefab = allTarotPrefabs[selectedTarotIndex[i]]; //0表示愚者
            //selectedTarotPrefabs[i] = tarotPrefab;
        //}
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