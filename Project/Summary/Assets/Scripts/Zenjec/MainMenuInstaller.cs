using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MenuManager MenuManager;
    [SerializeField] private CameraRayCaster CameraRayCaster; 

    public override void InstallBindings()
    {
        Container.BindInstance(MenuManager);
        Container.BindInstance(CameraRayCaster);
    }
}