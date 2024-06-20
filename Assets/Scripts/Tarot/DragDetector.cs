using UnityEngine;
using UnityEngine.EventSystems;

public class DragDetector : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private int x => gameObject.GetComponent<GameTarot>().X;
    private int y => gameObject.GetComponent<GameTarot>().Y;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2Int direction = CalculateDirection(mouseWorldPosition);

        Vector2Int target = new Vector2Int(x, y) + direction;

        Debug.Log(TarotManager.Instance.gameTarots[target.x, target.y] == null);
        Debug.Log(gameObject.GetComponent<GameTarot>().Type == TarotType.None);
        TarotManager.Instance.SwapGameTarot(gameObject.GetComponent<GameTarot>(), TarotManager.Instance.gameTarots[target.x, target.y]);
    }

    Vector2Int CalculateDirection(Vector3 endPosition)
    {
        Vector2Int direction = Vector2Int.zero;
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
        float deltaX = endPosition.x - startPosition.x;
        float deltaY = endPosition.y - startPosition.y;

        if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
        {
            if (deltaY < 0)
            {
                direction = new Vector2Int(0, -1);
            }
            else
            {
                direction = new Vector2Int(0, 1);
            }
        }
        else
        {
            if (deltaX < 0)
            {
                direction = new Vector2Int(-1, 0);
            }
            else
            {
                direction = new Vector2Int(1, 0);
            }
        }
        return direction;
    }
}
