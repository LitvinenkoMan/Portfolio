using UnityEngine;
using Zenject;

public class CarArcadeInstaller : MonoInstaller
{
    [SerializeField] Race race;
    public override void InstallBindings()
    {
        //Container.Bind<IRace>().FromInstance(race);
    }
}