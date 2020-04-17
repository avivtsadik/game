using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private bool _canDamage = true;
    private void Start()
    {
        if (transform.position.x < 0)
            Destroy(this.gameObject, 5.0f);
        else
            Destroy(this.gameObject, 3.0f);
    }
    private void Update()
    {
        if (transform.position.x < 0)
            transform.Translate(Vector3.right * 3 * Time.deltaTime);
        else
            transform.Translate(Vector3.left * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();

            if (hit != null)
            {
                if (_canDamage)
                {
                    hit.Damage();
                    _canDamage = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
