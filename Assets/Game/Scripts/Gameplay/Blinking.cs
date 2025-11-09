using System.Collections;
using UnityEngine;

public class Blinking 
{
    private Animator _animator;
    private MonoBehaviour _owner;


    private const string Blink = "Blink";
    private const float MinBlinkTime = 2.5f;
    private const float MaxBlinkTime = 4.5f;


    public Blinking(MonoBehaviour owner, Animator animator)
    {
        _animator = animator;
        _owner = owner;

        _owner.StartCoroutine(BlinkCoroutine(Random.Range(MinBlinkTime, MaxBlinkTime)));
    }


    private IEnumerator BlinkCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        _animator.SetTrigger(Blink);

        _owner.StartCoroutine(BlinkCoroutine(Random.Range(MinBlinkTime, MaxBlinkTime)));
    }
}
