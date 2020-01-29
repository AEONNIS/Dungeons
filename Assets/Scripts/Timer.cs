using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Coroutine _timerRoutine;
    private float _pastTime;

    public void StartTimer(float duration, Action onEnd = null)
    {
        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);

        _timerRoutine = StartCoroutine(Run(duration, onEnd));
    }

    public float StopTimer()
    {
        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);

        return _pastTime;
    }

    private IEnumerator Run(float duration, Action onEnd)
    {
        _pastTime = 0.0f;

        while (_pastTime < duration)
        {
            _pastTime += Time.deltaTime;
            yield return null;
        }

        onEnd?.Invoke();
    }
}
