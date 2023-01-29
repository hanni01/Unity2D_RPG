using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wasAttacked : MonoBehaviour
{
    Animator animator;
    public Image hpBar;
    public GameObject healthBar;
    Rigidbody2D rb;
    public GameObject attakEffect;

    private void Start() {
        animator = GetComponent<Animator>();
        hpBar.fillAmount = 1f;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "bolt")
        {
            hpBar.fillAmount -= 10f/100f;
            animator.SetBool("isCollided", true);
            attakEffect.SetActive(true);
            StartCoroutine(waitChangeStatus());
        }
    }
    private void Update() {

        if(hpBar.fillAmount == 0)
        {
            animator.SetBool("isDead", true);
            healthBar.SetActive(false);
            Destroy(this.gameObject, 1.0f);
        }
    }

    IEnumerator waitChangeStatus()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isCollided", false);
        attakEffect.SetActive(false);
    }
}
