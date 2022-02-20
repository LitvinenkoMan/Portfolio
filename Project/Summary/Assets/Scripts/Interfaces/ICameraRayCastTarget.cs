public interface ICameraRayCastTarget
{
    public delegate void MouseDrag();
    public event MouseDrag OnMouseDrag;
    
    public delegate void MouseClick();
    event  MouseClick OnMouseClick;

    public void Action();
}
