using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    private float health = 100f;
    Transform camTransform;
    public Transform ui;
    public Image healthBar;

    
    void Start()
    {
        camTransform = Camera.main.transform;   
    }

   
    void Update()
    {
        Vector3 lookDirection = (ui.position - camTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        ui.rotation = Quaternion.Lerp(ui.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void TakeDamage(int amount)
    {
        if (health < amount)
        {
            return;
        }
        else
        {
            StartCoroutine(TakeDamageSmoothly(amount));
        }
    }
    private IEnumerator TakeDamageSmoothly(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            health--;
            healthText.text = health.ToString();
            healthBar.fillAmount = health / 100f;
            yield return null;
        }
    }

}