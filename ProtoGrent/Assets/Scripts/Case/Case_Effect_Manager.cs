using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class Case_Effect_Manager : MonoBehaviour
{
    public Vector2 pos;

    public GameObject BoomEffect;

    public Image[] images;
    public Sprite[] modifierSprite;
    public ImageSize[] imageSizes;

    public bool isFired = false;
    public bool isWatered = false;
    public bool isOiled = false;
    public bool isEncouraged = false;

    public bool isDestroy = false;

    public int index;

    private void Start()
    {
        pos = GetComponent<Case_Script>().pos;
    }

    public void CheckExplose()
    {
        if(isFired && isOiled)
        {
            GetComponent<Case_Script>().SetCard(null);

            Case_Unit_Manager unitManager = GetComponent<Case_Unit_Manager>();
            if (unitManager != null)
            {
                Transform unit = unitManager.unitsParent;
                if (unit != null)
                    unitManager.DestroyUnitOnCase();
            }
            GameObject effect = Instantiate(BoomEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2);
        }
    }

    public void AddImage(Sprite sprite)
    {
        int lastIndex = index;
        GetModifierNomber();
        int newIndex = index - lastIndex;

        for (int i = 0; i < index; i++)
        {
            if (images[i].sprite == null)
            {
                SetImagePosSizeAndSprite(images[i], imageSizes[index - 1], index-1, sprite);
            }
            else
            {
                SetImagePosSize(images[i], imageSizes[index - 1], i);
            }
        }
    }

    public void ClearImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
            images[i].gameObject.SetActive(false);
        }
        if (isEncouraged)
            SetImagePosSizeAndSprite(images[0], imageSizes[0], 0, modifierSprite[3]);
    }

    public void ClearAllImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
            images[i].gameObject.SetActive(false);
        }
    }

    void SetImagePosSizeAndSprite(Image image,ImageSize imageSize,int ImageIndex, Sprite sprite)
    {
        image.gameObject.SetActive(true);
        image.rectTransform.localPosition = imageSize.pos[ImageIndex];
        image.rectTransform.sizeDelta = new Vector2(imageSize.size, imageSize.size);
        image.sprite = sprite;
    }

    void SetImagePosSize(Image image,ImageSize imageSize, int ImageIndex)
    {
        image.rectTransform.localPosition = imageSize.pos[ImageIndex];
        image.rectTransform.sizeDelta = new Vector2(imageSize.size, imageSize.size);
    }

    void GetModifierNomber()
    {
        index = 0;
        if (isFired)
        {
            index++;
        }
        if (isWatered)
        {
            index++;
        }
        if (isOiled)
        {
            index++;
        }
        if (isEncouraged)
        {
            index++;
        }
    }
}

[System.Serializable]
public class ImageSize
{
    public Vector3[] pos;
    public float size;
}
