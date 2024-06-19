using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public bool Running => _coro != null;

    public Image image;
    public SpriteSheet sheet;
    public float speedCoef;

    public event Action<Sprite> OnNextFrame;
    public event Action OnStart;
    public event Action OnEnd;
    public event Action OnLoop;

    private Coroutine _coro;

    public bool Begin(bool noLoop)
    {
        if (Running) return false;

        _coro = StartCoroutine(Animate(noLoop));
        return true;
    }

    public bool End()
    {
        if (!Running) return false;

        StopCoroutine(_coro);
        _coro = null;
        return true;
    }

    private IEnumerator Animate(bool noLoop)
    {
        OnStart?.Invoke();
        do
        {
            for (int i = 0; i < sheet.frames.Length; i++)
            {
                SpriteSheet.Frame frame = sheet.frames[i];
                image.sprite = frame.sprite;
                OnNextFrame?.Invoke(frame.sprite);
                yield return new WaitForSeconds(frame.time * speedCoef);
            }

            if (noLoop || !sheet.loop)
            {
                break;
            }

            OnLoop?.Invoke();
        } while (true);
        OnEnd?.Invoke();
        _coro = null;
    }
}
