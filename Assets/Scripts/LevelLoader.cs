using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator _animator;

    private readonly int _fadeOut = Animator.StringToHash("Out");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void NextScene()
    {
        StartCoroutine(FadeOut(1)); 
    }

    public void ReStartScene()
    {
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().buildIndex));
    } 

    private IEnumerator FadeOut(int index)
    {
        _animator.SetTrigger(_fadeOut);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }
}
