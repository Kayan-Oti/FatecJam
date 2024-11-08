public interface IInteractable
{
    public void Interact(Interactor interactor);
    public void EndInteraction();
    public bool CanInteract();
    public void SetIsInRange(bool state);
}
