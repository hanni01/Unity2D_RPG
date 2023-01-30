using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHP : MonoBehaviour
{
    public Image charHPImage;
    public Animator charAni;

    void Start()
    {
        charHPImage.fillAmount = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameObject.Find("Wizard").GetComponent<MovingObject>().isAttacked)
        {
            charHPImage.fillAmount -= 5f/100f;
            charAni.SetBool("isHurt", true);
            StartCoroutine(wait());
            GameObject.Find("Wizard").GetComponent<MovingObject>().isAttacked = false;
        }

        if(GameObject.Find("Wizard").GetComponent<charRespawn>().isFloorEnd)
        {
            charHPImage.fillAmount -= 2f/100f;
            GameObject.Find("Wizard").GetComponent<charRespawn>().isFloorEnd = false;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        charAni.SetBool("isHurt", false);
    }
}
