using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainMenuBase : MonoBehaviour
{
    public Events.EventFadeComplete OnMainMenuFadeComplete;

    [SerializeField] protected Animation _mainMenuAnimator;
    [SerializeField] protected AnimationClip _fadeOutAnimation;
    [SerializeField] protected AnimationClip _fadeInAnimation;
    [SerializeField] protected AnimationClip _startAnimator;


    protected virtual void Start()
    {
        GameManagerBase.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        _mainMenuAnimator.clip = _startAnimator;
        _mainMenuAnimator.Play();
    }
    protected virtual void HandleGameStateChanged(GameManagerBase.GameState currentState, GameManagerBase.GameState previousState)
    {
        if (previousState == GameManagerBase.GameState.PREGAME && currentState == GameManagerBase.GameState.RUNING)
        {
            FadeOut();
        }
        if (previousState != GameManagerBase.GameState.PREGAME && currentState == GameManagerBase.GameState.PREGAME)
        {
            FadeIn();
        }
    }

    protected virtual IEnumerator FadeOutRutine()
    {

        yield return new WaitForSeconds(_fadeOutAnimation.length);

        OnMainMenuFadeComplete?.Invoke(true);
        UIManagerBase.Instance.SetMenuCameraActive(false);
        //gameObject.SetActive(false);

    }

    protected virtual IEnumerator FadeInRutine()
    {
        UIManagerBase.Instance.SetMenuCameraActive(true);

        yield return new WaitForSeconds(_fadeInAnimation.length);

        OnMainMenuFadeComplete?.Invoke(false);
        _mainMenuAnimator.clip = _startAnimator;
        _mainMenuAnimator.Play();
    }

    protected virtual void FadeIn()
    {
        // TODO: This just don't want to work, no idea why. Save it for later. Temporary Fade In Rutine. 
        // (fade in Animation just don't want to kick in)
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();


        StartCoroutine(FadeInRutine());
    }

    protected virtual void FadeOut()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
        StartCoroutine(FadeOutRutine());
    }

}
