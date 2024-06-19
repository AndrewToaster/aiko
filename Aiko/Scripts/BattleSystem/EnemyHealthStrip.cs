using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthStrip : MonoBehaviour
{
    public Image healthStrip;

    public float Health => _healthFrac;

    [SerializeField] private float _healthFrac;

    public void SetHealthAmount(float frac)
    {
        _healthFrac = frac;
        healthStrip.transform.localScale = new Vector3(healthStrip.transform.localScale.x, frac, healthStrip.transform.localScale.y);
    }
}
