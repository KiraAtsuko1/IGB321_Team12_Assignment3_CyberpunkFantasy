using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentbuildingExit : MonoBehaviour
{
    public GameObject transparentObject;

    [SerializeField]
    private GameObject enterTrigger;

    public IEnumerator fadeObject(GameObject objToFade)
    {
        MeshRenderer meshRenderer = objToFade.GetComponent<MeshRenderer>();

        Color color = meshRenderer.materials[0].color;

        while (color.a < 255)
        {
            color.a += 0.1f;
            meshRenderer.materials[0].color = color;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => meshRenderer.materials[0].color.a == 255f);
        Debug.Log("Faded");
    }

    public IEnumerator waitForTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
        enterTrigger.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fade");
            StartCoroutine(fadeObject(transparentObject));
            StartCoroutine(waitForTrigger());
        }
    }
}
