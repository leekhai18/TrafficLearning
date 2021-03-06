﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MarchingBytes;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] public string poolName;

    Slot begin;
    public Command item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject.GetComponent<Command>();
            }

            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            try
            {
                begin = DragHandler.itemBeginDragged.transform.parent.gameObject.GetComponent<Slot>();

                if (DragHandler.itemBeginDragged.GetCurrentState() == EState.begin)
                {
                    GameObject obj = EasyObjectPool.Instance.GetObjectFromPool(begin.poolName, begin.transform.position, Quaternion.identity);
                    obj.transform.SetParent(begin.transform);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;
                }

                DragHandler.itemBeginDragged.transform.SetParent(transform);
                DragHandler.itemBeginDragged.SetState(EState.end);

                if (DragHandler.itemBeginDragged.transform.parent.localPosition.x > 500)
                {
                    GameManager.Instance.Backward(120, 0.8f);
                }

                poolName = begin.poolName;
            }
            catch (Exception)
            {
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
