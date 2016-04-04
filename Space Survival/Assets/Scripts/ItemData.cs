using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {

    public Item item;
    public int amount = 1;
    public int slot;

    private Transform slotPanel;
    private Transform originalParent;
    private Vector2 offset;
    private Inventory inv;

    void Start() {
        originalParent = transform.parent;
        slotPanel = transform.parent.parent;
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(item != null) {
            offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if(item != null) {
            transform.SetParent(slotPanel);
            transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if(item != null) {
            transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(inv.slots[slot].transform);
        transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
