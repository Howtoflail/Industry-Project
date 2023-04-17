using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
    public IEnumerator MenuAnimation(GameObject menuOptions, bool collapsed)
    {
        Animator animator = menuOptions.GetComponent<Animator>();
        animator.SetBool("collapsed", collapsed);
        animator.SetTrigger("click");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        //if (collapsed) menuOptions.SetActive(false); enable when creating collapse / exapand animations
    }
}
