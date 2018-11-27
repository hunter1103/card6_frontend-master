using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Raycast : MonoBehaviour,IDragHandler, IEndDragHandler {

    // Use this for initialization
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject panel;
    
    
	void Start () {
       
	}
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3( Input.mousePosition.x, 230, 0) ;
        //transform.rotation = new Vector3(0, Input.mousePosition.x*0.5f, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        
    }
	// Update is called once per frame
	void Update () {
        //if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    RaycastHit hitInfo;
        //    target = returnClickObject(out hitInfo);
        //    if (target != null)
        //    {
        //        isMouseDraging = true;
        //       
        //        //target.transform.localPosition = Input.mousePosition;
        //    }
            
           
        //}
    }
    //GameObject returnClickObject(out RaycastHit hit)
    //{
    //    GameObject targetObject = null;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit, raylength, layermask))
    //    {
    //        targetObject = hit.collider.gameObject;
    //    }
    //    return targetObject;
    //}
}
