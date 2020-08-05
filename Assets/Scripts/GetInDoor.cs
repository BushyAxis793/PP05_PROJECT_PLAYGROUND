using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GetInDoor : MonoBehaviour
{
    public float poziom_fade = .1f;
    public int ile_s = 1;
    public GameObject[] sufit = new GameObject[1];

    public int ile_g = 0;
    public GameObject[] rzeczy = new GameObject[0];

    private Color kolor;

    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Player") && (moze == true))
        {
            for (int x = 0; x < ile_s; x++)
            {
                SetMaterialTransparent(sufit[x]);
                StartCoroutine(czek_1(sufit[x]));
            }
            for (int z = 0; z < ile_g; z++)
            {
                rzeczy[z].SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if ((collider.tag == "Player") && (moze == true))
        {
            for (int x = 0; x < ile_s; x++)
            {
                StartCoroutine(czek_2(sufit[x]));
            }
            for (int z = 0; z < ile_g; z++)
            {
                rzeczy[z].SetActive(true);
            }
        }
    }

    private void SetMaterialTransparent(GameObject Roof)
    {
        foreach (Material m in Roof.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_Mode", 2);
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }

    }

    private void SetMaterialOpaque(GameObject Roof)
    {
        foreach (Material m in Roof.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            m.SetInt("_ZWrite", 1);
            m.DisableKeyword("_ALPHATEST_ON");
            m.DisableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = -1;
        }
    }

    bool moze = true;
    IEnumerator czek_1(GameObject Roof)
    {
        moze = false;
        kolor = Roof.GetComponent<Renderer>().material.color;
        for (float i = 1; i > poziom_fade; i = i - 0.1f)
        {
            Roof.GetComponent<Renderer>().material.color = kolor;
            kolor.a = i;
            yield return new WaitForSeconds(0.05f);
        }
        moze = true;
    }

    IEnumerator czek_2(GameObject Roof)
    {
        moze = false;
        kolor = Roof.GetComponent<Renderer>().material.color;
        for (float i = 0.05f; i < 1.0f; i = i + 0.1f)
        {
            Roof.GetComponent<Renderer>().material.color = kolor;
            kolor.a = i;
            yield return new WaitForSeconds(0.05f);
        }
        SetMaterialOpaque(Roof);
        moze = true;
    }
}
