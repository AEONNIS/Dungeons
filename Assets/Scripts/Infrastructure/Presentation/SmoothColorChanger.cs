using System;
using System.Collections;
using UnityEngine;

namespace Dungeons.Infrastructure.Presentation
{
    public class SmoothColorChanger : MonoBehaviour
    {
        private Coroutine _colorChangeRoutine;

        public void Begin(SpriteRenderer renderer, Color color, float duration, Action onEnd = null)
        {
            if (_colorChangeRoutine != null)
                StopCoroutine(_colorChangeRoutine);

            _colorChangeRoutine = StartCoroutine(ChangeColor(renderer, color, duration, onEnd));
        }

        private IEnumerator ChangeColor(SpriteRenderer renderer, Color endingColor, float duration, Action onEnd)
        {
            float time = 0.0f;

            while (time <= duration)
            {
                renderer.color = Color.Lerp(renderer.color, endingColor, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            onEnd?.Invoke();
        }
    }
}
