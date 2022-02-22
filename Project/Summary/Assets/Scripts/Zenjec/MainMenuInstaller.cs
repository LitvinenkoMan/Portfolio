using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MenuManager MenuManager;
    [SerializeField] private CameraRayCaster CameraRayCaster;
    [SerializeField] private CarManager CarManager;

    public override void InstallBindings()
    {
        Container.BindInstance(MenuManager);
        Container.BindInstance(CameraRayCaster);
        Container.BindInstance(CarManager);
    }
}