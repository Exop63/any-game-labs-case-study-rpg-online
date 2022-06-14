using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTest : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip[] animations;
    [SerializeField] private int index = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animations = animator.runtimeAnimatorController.animationClips;
        StartCoroutine("PlayAnimation");
    }

    // Update is called once per frame
    IEnumerator PlayAnimation()
    {
        AnimationClip clip = animations[index];
        animator.Play(clip.name);
        Debug.Log(index);
        yield return new WaitForSeconds(clip.length);
        index++;
        if (index == animations.Length) index = 0;
        StartCoroutine("PlayAnimation");
    }
}
