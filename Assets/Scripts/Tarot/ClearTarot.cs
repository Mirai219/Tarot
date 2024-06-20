using System.Collections;
using UnityEngine;

public class ClearTarot: MonoBehaviour
{
    public AnimationClip clearAnimation;
    private bool isClearing;
    public bool IsClearing
    {
        get { return isClearing; }
    }
    public virtual void Clear()
    {
        isClearing = true;
        StartCoroutine(ClearCoroutine());
    }
    
    private IEnumerator ClearCoroutine()
    {
        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {      
            animator.Play(clearAnimation.name);

            yield return new WaitForSeconds(clearAnimation.length);

            if (gameObject!=null)
            {
                Destroy(gameObject);
            }           
        }
        isClearing=false;
    }
    
}
