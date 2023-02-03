using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MoneyHolder : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> cashInHolder;

    [SerializeField] private List<GameObject> itemsInCollector;
    [SerializeField] private List<Transform> itemInBag;

    public List<Transform> itemInBag1 => itemInBag;

    [SerializeField] private float size;

    [SerializeField] private Transform lastPosition;

    [SerializeField] private Transform lastItem;

    private Vector3 myPos;
    private void Start()
    {
        var thisTransform = transform;
        lastPosition.localPosition = thisTransform.localPosition;
        lastItem = thisTransform;
    }

    private void LateUpdate()
    {
        myPos = transform.position;
    }

    private Vector3 _lastLocalPos;
    private float previousPos;
    private float previousHeight;
    [SerializeField] private float previousScaleToAdd;
    private float actualValue;

    //CASH
    public void AddNewItemInHolder(Transform newItem)
    {
        LeanTween.move(newItem.gameObject, lastPosition.position, 0.1f).setOnComplete(() =>
        {
            newItem.rotation = gameObject.transform.rotation;
            _lastLocalPos = cashInHolder.Count == 0
                ? new Vector3(0, lastItem.localPosition.y, lastPosition.localPosition.z)
                : new Vector3(0, size + lastItem.position.y, lastPosition.localPosition.z);
            newItem.localPosition = _lastLocalPos;
            cashInHolder.Add(newItem.gameObject);
            newItem.GetComponent<CashCtrl>().UpdateCubePosition(lastItem, true);
            lastItem = newItem;
        });
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    //OBJECTS
    public IEnumerator AddNewItemToCollector(Transform itemToAdd)
    {
        itemToAdd.SetParent(null);
        itemToAdd.GetComponent<BoxCollider>().enabled = true;
        itemToAdd.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1f);
            itemToAdd.GetComponent<BoxCollider>().enabled = false;
            itemToAdd.GetComponent<Rigidbody>().isKinematic = true;
            //itemToAdd.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            //itemToAdd.transform.GetComponent<Rigidbody>().isKinematic = true;
            LeanTween.rotateY(itemToAdd.gameObject, 360f, 1f);
            itemToAdd.DOMove(lastItem.position + new Vector3(0,0,3), .3f).OnComplete(
                () =>
                {
                    
                    if (itemsInCollector.Count > 0)
                    {
                        previousPos = itemsInCollector.LastOrDefault().transform.localPosition.y;
                        previousHeight = itemsInCollector.LastOrDefault().transform.localScale.y;
                        previousScaleToAdd = itemsInCollector.LastOrDefault().GetComponent<ObjectSettings>().ValueToAdd;
                    }
                    itemToAdd.GetComponent<CashCtrl>().UpdateCubePosition(lastItem, true);
                    //itemToAdd.SetParent(transform, true);
                    lastItem = itemToAdd;
                    itemToAdd.localRotation = Quaternion.identity;
                    float previousItem = previousPos  + previousScaleToAdd;
                    if (itemsInCollector.Count == 0)
                    {
                        itemToAdd.localPosition = lastPosition.localPosition;
                        itemsInCollector.Add(itemToAdd.gameObject);
                    }
                    else
                    {
                        itemToAdd.localPosition = Vector3.up * previousItem;
                        itemsInCollector.Add(itemToAdd.gameObject);
                    }
                }
            );
    }

    public void AddItemToBag(Transform itemAdd)
    {
        itemInBag.Add(itemAdd);
        itemAdd.parent = this.transform;
        itemAdd.localScale = Vector3.zero;
        itemAdd.GetComponent<ObjectSettings>().follow1 = false;
        itemAdd.GetComponent<Rigidbody>().isKinematic = true;
    }

    

}
