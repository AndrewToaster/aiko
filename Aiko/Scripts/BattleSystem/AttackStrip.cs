using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class AttackStrip : MonoBehaviour
{
    public bool Running => _coro != null;

    public Transform daggerPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public event Action<float> OnHit;

    private Coroutine _coro;

    public void Refresh()
    {
        daggerPoint.transform.position = startPoint.transform.position;
    }

    public bool Attack(float totalTime, float bufferTime = 0.1f)
    {
        if (Running) return false;
        _coro = StartCoroutine(AttackCoro(totalTime, bufferTime));
        return true;
    }

    private IEnumerator AttackCoro(float totalTime, float bufferTime)
    {
        Refresh();
        if (bufferTime != 0)
        {
            yield return new WaitForSeconds(bufferTime);
        }
        float start = Time.time;
        float end = start + totalTime;
        float time;
        while ((time = Time.time) < end)
        {
            float frac = (time - start) / totalTime;
            if (Input.GetMouseButton(0))
            {
                OnHit?.Invoke(frac);
                _coro = null;
                yield break;
            }
            daggerPoint.position = Vector3.Lerp(startPoint.position, endPoint.position, frac);
            yield return new WaitForEndOfFrame();
        }
        OnHit?.Invoke(1);
        _coro = null;
    }
}
