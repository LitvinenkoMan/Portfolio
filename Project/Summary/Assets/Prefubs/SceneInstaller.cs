using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public CarMovment car;

    public override void InstallBindings()
    {
        Container.Bind<CarMovment>().FromInstance(car);
    }
}