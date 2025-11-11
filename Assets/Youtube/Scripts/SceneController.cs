using UnityEngine;
using UnityEngine.Playables;

public class SceneController : MonoBehaviour
{
    [SerializeField] PlayableDirector _timeLine;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _timeLine.Play();
    }
}
