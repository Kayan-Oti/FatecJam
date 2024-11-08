using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : UI_Abstract_Selectable, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler
{

    protected virtual void OnClickEvent(){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonClick, transform.position);
    }

    protected virtual void OnEnterEvent(){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonHover, transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    protected override void OnSelectDo(BaseEventData eventData)
    {
        OnEnterEvent();
        Vector3 scale  = new Vector3(1.25f, 1.25f, 1);
        transform.DOScale(scale, 0.25f);
    }

   protected override void OnDeselectDo(BaseEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.25f);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        OnClickEvent();
    }

}