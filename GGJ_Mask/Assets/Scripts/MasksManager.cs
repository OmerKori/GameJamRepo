using UnityEngine;

public class MasksManager : MonoBehaviour
{
    [SerializeField] GameObject[] masks;
    [SerializeField] GameObject masksUIParent;
    [SerializeField] RectTransform maskUIHighlight;
    GameObject currentMask,IndicatedMask = null;

    private void Start()
    {
        currentMask = masks[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            IndicateMask(0);
        else if (Input.GetKeyDown(KeyCode.I))
            IndicateMask(1);
        else if(Input.GetKeyDown(KeyCode.O))
            IndicateMask(2);
        else if(Input.GetKeyDown(KeyCode.P))
            IndicateMask(3);
            
        if(Input.GetKeyDown(KeyCode.Space) && IndicatedMask!=null)
            SwapMasks();
    }

    void IndicateMask(int i)
    {
        if (masks[i] == currentMask) 
            return;
        
        if(masks[i] == IndicatedMask)
        { 
            RemoveIndicatedMask();
            return;
        }

        if (i > masks.Length)
        {
            Debug.LogError("Mask index out of range: " + i);
            return;
        }

        if(IndicatedMask != null)
            IndicatedMask.SetActive(false);

        //enable indicated mask and change transparencies
        IndicatedMask = masks[i];
        IndicatedMask.GetComponent<PolygonCollider2D>().enabled = false;
        IndicatedMask.SetActive(true);
        IndicatedMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
        currentMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        //move highlight in UI
        maskUIHighlight.SetParent(masksUIParent.transform.GetChild(i), false);
        maskUIHighlight.anchoredPosition = Vector3.zero;
    }

    void SwapMasks()
    {
        currentMask.SetActive(false);
        currentMask = IndicatedMask;
        currentMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        currentMask.SetActive(true);
        IndicatedMask.GetComponent<PolygonCollider2D>().enabled = true;

        IndicatedMask = null;
    }

    private void RemoveIndicatedMask()
    {
        IndicatedMask.SetActive(false);
        IndicatedMask = null;
        currentMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
    
}
