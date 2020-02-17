using UnityEngine;
using System.Collections;
using System;

namespace Game
{
    public class SmoothColorChanger : MonoBehaviour
    {
        private Coroutine _colorChangeRoutine;

        public void StartColorChange(SpriteRenderer renderer, Color color, float duration, Action onEnd = null)
        {
            if (_colorChangeRoutine != null)
                StopCoroutine(_colorChangeRoutine);

            _colorChangeRoutine = StartCoroutine(SmoothlyChangeColor(renderer, color, duration, onEnd));
        }

        private IEnumerator SmoothlyChangeColor(SpriteRenderer renderer, Color color, float duration, Action onEnd)
        {
            float time = 0.0f;

            while (time <= duration)
            {
                renderer.color = Color.Lerp(renderer.color, color, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            onEnd?.Invoke();
        }
    }
}
