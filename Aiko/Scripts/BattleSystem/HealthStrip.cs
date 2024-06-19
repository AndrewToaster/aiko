using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthStrip : MonoBehaviour
{
    public Image healthStrip;
    public Image sanityStrip;

    public float Health => _healthFrac;
    public float Sanity => _sanityFrac;

    [SerializeField] private float _healthFrac = 1;
    [SerializeField] private float _sanityFrac = 1;

    public void SetHealthAmount(float frac)
    {
        _healthFrac = frac;
        healthStrip.transform.localScale = new Vector3(healthStrip.transform.localScale.x, frac, healthStrip.transform.localScale.y);
    }

    public void SetSanityAmount(float frac)
    {
        _sanityFrac = frac;
        sanityStrip.transform.localScale = new Vector3(sanityStrip.transform.localScale.x, frac, sanityStrip.transform.localScale.y);
    }
}
