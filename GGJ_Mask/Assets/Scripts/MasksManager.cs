using UnityEngine;
using UnityEngine.UI;
public class MasksManager : MonoBehaviour
{
    [SerializeField] GameObject[] masks;
    [SerializeField] GameObject masksUIParent;
    [SerializeField] RectTransform maskUIHighlight;
    [SerializeField] Image OverlapImg1,OverlapImg2;
    [SerializeField] GameObject player;
    [SerializeField] Color previewValidColor = new Color(1f, 1f, 1f, 0.03f);
    [SerializeField] Color previewInvalidColor = new Color(1f, 0f, 0f, 0.2f);

    GameObject currentMask,IndicatedMask = null;
    public int currentMaskIndex = 0, indicatedMaskIndex = -1;
    LevelManager levelManager;
    Collider2D playerCollider;

    private void Start()
    {
        currentMask = masks[0];
        levelManager = FindObjectOfType<LevelManager>();
        if (player != null)
            playerCollider = player.GetComponent<Collider2D>();
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

        if (IndicatedMask != null)
            UpdatePreviewColor();
    }

    public void IndicateMask(int i)
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
        UpdatePreviewColor();
        IndicatedMask.transform.Find("Outline").GetComponent<SpriteRenderer>().enabled = false;
        currentMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);

      //  OverlapImg1.sprite = currentMask.GetComponent<SpriteRenderer>().sprite;
      //  OverlapImg2.sprite = IndicatedMask.GetComponent<SpriteRenderer>().sprite;
      //  OverlapImg1.gameObject.SetActive(true);
        indicatedMaskIndex = i;

        //move highlight in UI
        maskUIHighlight.SetParent(masksUIParent.transform.GetChild(i), false);
        maskUIHighlight.anchoredPosition = Vector3.zero;
    }

    public void SwapMasks()
    {
        if (!CanSwapToMask(IndicatedMask))
            return;

        currentMask.SetActive(false);
        currentMask = IndicatedMask;
        currentMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0,1);
        currentMask.SetActive(true);
        currentMask.GetComponent<PolygonCollider2D>().enabled = true;
        currentMask.transform.Find("Outline").GetComponent<SpriteRenderer>().enabled = true;

        IndicatedMask = null;
        currentMaskIndex = indicatedMaskIndex;
        indicatedMaskIndex = -1;
    }

    private bool CanSwapToMask(GameObject mask)
    {
        if (player == null || playerCollider == null || mask == null)
            return false;

        PolygonCollider2D collider = mask.GetComponent<PolygonCollider2D>();
        if (collider == null)
            return false;

        bool wasEnabled = collider.enabled;
        if (!wasEnabled)
            collider.enabled = true;

        bool isTouching = playerCollider.IsTouching(collider);
        bool isInside = collider.OverlapPoint(playerCollider.bounds.center);

        if (!wasEnabled)
            collider.enabled = false;

        return !(isTouching || isInside);
    }

    private void UpdatePreviewColor()
    {
        if (IndicatedMask == null)
            return;

        SpriteRenderer spriteRenderer = IndicatedMask.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            return;

        spriteRenderer.color = CanSwapToMask(IndicatedMask) ? previewValidColor : previewInvalidColor;
    }

    public void RemoveIndicatedMask()
    {
        IndicatedMask.SetActive(false);
        IndicatedMask = null;
        currentMask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

        maskUIHighlight.SetParent(masksUIParent.transform.GetChild(currentMaskIndex), false);
        maskUIHighlight.anchoredPosition = Vector3.zero;
     //   OverlapImg1.gameObject.SetActive(false);
    }
    
}
