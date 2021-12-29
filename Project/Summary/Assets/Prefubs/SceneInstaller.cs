using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    CarManager CarManager;
    //[SerializeField]
    //List<GameObject> Cars = new List<GameObject>();
    //[SerializeField]
    //GameObject GoodCar;
    //[SerializeField]
    //GameObject ElComina;
    //[SerializeField]
    //GameObject FordBronco;

    public override void InstallBindings()
    {
        //foreach (var item in Cars)
        //{
        //    Container.InstantiateComponent<CarSpeed>(item);
        //}
        Container.Bind<CarManager>().FromInstance(CarManager);
        //Container.InstantiateComponent<CarSpeed>(GoodCar);
        //Container.InstantiateComponent<CarSpeed>(ElComina);
        //Container.InstantiateComponent<CarSpeed>(FordBronco);
    }
}