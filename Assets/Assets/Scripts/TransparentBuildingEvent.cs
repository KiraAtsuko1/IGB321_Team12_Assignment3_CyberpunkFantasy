using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBuildingEvent : MonoBehaviour
{
    public GameObject transparentObject;
    public GameObject[] additionalObjects;

    [SerializeField]
    private GameObject[] exitTrigger,enterTrigger;

    public IEnumerator fadeObject(GameObject objToFade)
    {
        MeshRenderer meshRenderer = objToFade.GetComponent<MeshRenderer>();

        Color color = meshRenderer.materials[0].color;

        while (color.a > 0)
        {
            color.a -= 0.1f;
            meshRenderer.materials[0].color = color;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => meshRenderer.materials[0].color.a <= 0f);
        Debug.Log("Faded");

        foreach(GameObject addObject in additionalObjects)
        {
            addObject.SetActive(false);
        }
    }

    public IEnumerator waitForTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
        foreach (GameObject trigger in enterTrigger)
        {
            trigger.SetActive(false);
        }
        foreach (GameObject trigger in exitTrigger)
        {
            trigger.SetActive(true);
        }
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
