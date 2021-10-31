using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentbuildingExit : MonoBehaviour
{
    public GameObject transparentObject;
    public TransparentBuildingEvent additionalObject;

    [SerializeField]
    private GameObject[] enterTrigger, exitTrigger;

    private void Start()
    {
        additionalObject = GetComponent<TransparentBuildingEvent>();
    }

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

        foreach (GameObject addObject in additionalObject.additionalObjects)
        {
            addObject.SetActive(true);
        }
    }

    public IEnumerator waitForTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject trigger in exitTrigger)
        {
            trigger.SetActive(false);
        }
        foreach (GameObject trigger in enterTrigger)
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
