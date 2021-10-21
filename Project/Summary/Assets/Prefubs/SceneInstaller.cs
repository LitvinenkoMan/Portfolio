using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    CarManager CarManager;


    public override void InstallBindings()
    {
        Container.Bind<CarManager>().FromInstance(CarManager);
    }
}