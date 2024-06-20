using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTarot : MonoBehaviour, IPointerClickHandler
{
    private int x;
    private int y;
    public int X { get { return x; } set {  x = value; } }
    public int Y { get { return y; } set {  y = value; } }

    private TarotType type;
    public TarotType Type { get { return type; } set { type = value; } }

    private IEnumerator moveCoroutine;

    private ClearTarot clearComponent;

    public ClearTarot ClearTarotComponent
    {
        get { return clearComponent; }
    }

    public void Init(int x,int y,TarotType tarotType)
    {
        this.x = x;
        this.y = y;
        type = tarotType;
    }

    public void Move(int newX,int newY,float time)
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = MoveCoroutine(newX,newY,time);
        StartCoroutine(moveCoroutine);
    }

    private void Awake()
    {
        clearComponent = GetComponent<ClearTarot>();
    }

    private IEnumerator MoveCoroutine(int newX,int newY,float time)
    {
        x = newX;
        y = newY;

        Vector3 startPos = transform.position;
        Vector3 endPos = TarotManager.Instance.GridPositionToWorldPosition(newX,newY);

        float timer = 0f;
        while(timer < time)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, timer / time);
            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(type);
    }
}
