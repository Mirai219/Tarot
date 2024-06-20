using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotManager : SingletonBehaviour<TarotManager>
{     
    [SerializeField] private Transform containerTransform;
    public GameTarot[,] gameTarots;

    protected override void Awake()
    {
        base.Awake();
        gameTarots = new GameTarot[Constant.columns, Constant.rows];
    }

    public IEnumerator AllFill()
    {
        bool needRefill = true;

        while (needRefill)
        {
            yield return new WaitForSeconds(Constant.fillTime);
            while (Fill())
            {
                yield return new WaitForSeconds(Constant.fillTime);
            }

            ClearAllEmpty();
            needRefill = ClearAllMatchedTarot(out Dictionary<TarotType, int> ClearNum);
        }
    }

    public Vector3 GridPositionToWorldPosition(int x, int y)
    {
        Vector3 origin = new Vector3(-3.05f, 2.1f, 0);
        return origin + new Vector3(x * Constant.cellWidth, y * Constant.cellHeight, 0);
    }

    public void SwapGameTarot(GameTarot tarot1, GameTarot tarot2)
    {        
        gameTarots[tarot1.X, tarot1.Y] = tarot2;
        gameTarots[tarot2.X, tarot2.Y] = tarot1;

        if (MatchTarots(tarot1, tarot2.X, tarot2.Y) != null || MatchTarots(tarot2, tarot1.X, tarot1.Y) != null)
        {
            GameManager.Instance.turn++;
            int tempX = tarot1.X;
            int tempY = tarot1.Y;

            tarot1.Move(tarot2.X, tarot2.Y, Constant.fillTime);
            tarot2.Move(tempX, tempY, Constant.fillTime);

            ClearAllMatchedTarot(out Dictionary<TarotType, int> ClearNum);

            StartCoroutine(AllFill());
        }
        else
        {
            gameTarots[tarot1.X, tarot1.Y] = tarot1;
            gameTarots[tarot2.X, tarot2.Y] = tarot2;
        }
    }

    public bool ClearTarot(int x, int y)
    {
        if (!gameTarots[x, y].ClearTarotComponent.IsClearing)
        {
            gameTarots[x, y].ClearTarotComponent.Clear();

            CreateNewGameTarot(x, y, TarotType.None);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ClearAllMatchedTarot(out Dictionary<TarotType, int> ClearNum)
    {
        ClearNum = new Dictionary<TarotType, int>();
        bool needRefill = false;
        List<GameTarot> allMatchedTarots = new List<GameTarot>();
        for (int i = 0; i < Constant.columns; i++)
        {
            for (int j = 0; j < Constant.rows; j++)
            {
                List<GameTarot> matchedTarots = MatchTarots(gameTarots[i, j], i, j);
                if (matchedTarots != null)
                {
                    foreach (var ele in matchedTarots)
                    {
                        if (!allMatchedTarots.Contains(ele))
                        {
                            allMatchedTarots.Add(ele);
                        }
                    }
                }
            }
        }

        if (allMatchedTarots != null)
        {
            foreach (var matchedTarot in allMatchedTarots)
            {
                TarotType tarotType = matchedTarot.Type;
                if (!ClearNum.ContainsKey(tarotType))
                {
                    ClearNum.Add(tarotType, 1);
                }
                else
                {
                    ClearNum[tarotType] += 1;
                }
                if (ClearTarot(matchedTarot.X, matchedTarot.Y))
                {
                    needRefill = true;
                }
            }
        }
        foreach (var item in ClearNum)
        {
            Debug.Log(item);
        }
        return needRefill;
    }

    public List<GameTarot> MatchTarots(GameTarot gameTarot, int newX, int newY)
    {
        TarotType tarotType = gameTarot.Type;

        List<GameTarot> matchRowList = new List<GameTarot>();
        List<GameTarot> matchColList = new List<GameTarot>();
        List<GameTarot> finishedMatchList = new List<GameTarot>();

        matchRowList.Add(gameTarot);
        for (int i = 0; i <= 1; i++) //0表示左
        {
            for (int xDistance = 1; xDistance < Constant.columns; xDistance++)
            {
                int x;
                if (i == 0)
                {
                    x = newX - xDistance;
                }
                else
                {
                    x = newX + xDistance;
                }

                if (x < 0 || x >= Constant.columns)
                {
                    break;
                }

                if (gameTarots[x, newY].Type == tarotType)
                {
                    matchRowList.Add(gameTarots[x, newY]);
                }
                else
                {
                    break;
                }
            }
            if (matchRowList.Count >= 3)
            {
                foreach (var rowTarot in matchRowList)
                {
                    if (!finishedMatchList.Contains(rowTarot))
                        finishedMatchList.Add(rowTarot);
                }
            }
        }

        matchColList.Add(gameTarot);
        for (int i = 0; i <= 1; i++) //0表示下
        {
            for (int yDistance = 1; yDistance < Constant.rows; yDistance++)
            {
                int y;
                if (i == 0)
                {
                    y = newY - yDistance;
                }
                else
                {
                    y = newY + yDistance;
                }

                if (y < 0 || y >= Constant.rows)
                {
                    break;
                }

                if (gameTarots[newX, y].Type == tarotType)
                {
                    matchColList.Add(gameTarots[newX, y]);
                }
                else
                {
                    break;
                }
            }
            if (matchColList.Count >= 3)
            {
                foreach (var colTarot in matchColList)
                {
                    if (!finishedMatchList.Contains(colTarot))
                        finishedMatchList.Add(colTarot);
                }
            }
        }
        if (finishedMatchList.Count >= 3)
        {
            return finishedMatchList;
        }
        else
        {
            return null;
        }
    }

    public bool Fill()
    {
        bool filledNotFinished = false;

        for (int y = 1; y < Constant.rows; y++)
        {
            for (int x = 0; x < Constant.columns; x++)
            {
                GameTarot gameTarot = gameTarots[x, y];
                GameTarot belowGameTarot = gameTarots[x, y - 1];

                if (belowGameTarot.Type == TarotType.None)
                {
                    gameTarot.Move(x, y - 1, Constant.fillTime);
                    gameTarots[x, y - 1] = gameTarot;
                    CreateNewGameTarot(x, y, TarotType.None);
                    filledNotFinished = true;
                }
            }
        }

        for (int x = 0; x < Constant.columns; x++)
        {
            GameTarot topGameTarot = gameTarots[x, Constant.rows - 1];

            if (topGameTarot.Type == TarotType.None)
            {
                TarotPrefab randomPrefab = TarotPrefab.GetRandomTarotPrefab(GameManager.Instance.tarotPrefabs);
                GameObject newTarot = Instantiate(GameManager.Instance.prefabDictionaryByType[randomPrefab.TarotType], GridPositionToWorldPosition(x, Constant.rows), Quaternion.identity, containerTransform);

                gameTarots[x, Constant.rows - 1] = newTarot.GetComponent<GameTarot>();
                gameTarots[x, Constant.rows - 1].Init(x, Constant.rows, randomPrefab.TarotType);
                gameTarots[x, Constant.rows - 1].Move(x, Constant.rows - 1, Constant.fillTime);

                filledNotFinished = true;
            }
        }
        return filledNotFinished;
    }

    private void ClearAllEmpty()
    {
        GameObject[] empty = GameObject.FindGameObjectsWithTag("TarotEmpty");
        foreach (GameObject go in empty)
        {
            Destroy(go);
        }
    }

    public GameTarot CreateNewGameTarot(int x, int y, TarotType tarotType)
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabDictionaryByType[tarotType], GridPositionToWorldPosition(x, y), Quaternion.identity, containerTransform);
        gameTarots[x, y] = obj.GetComponent<GameTarot>();
        gameTarots[x, y].Init(x, y, tarotType);
        return gameTarots[x, y];
    }
}
