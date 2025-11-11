using UnityEngine;

public class Tavernkeeper : MonoBehaviour
{
    private Animator _animator;
    private Blinking _blinking;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _blinking = new Blinking(this, _animator);
    }

}
