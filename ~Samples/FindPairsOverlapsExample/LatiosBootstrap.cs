﻿using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Latios;

public class LatiosBootstrap : ICustomBootstrap
{
    public bool Initialize(string defaultWorldName)
    {
        var world                             = new LatiosWorld(defaultWorldName);
        World.DefaultGameObjectInjectionWorld = world;

        var initializationSystemGroup = world.GetExistingSystem<InitializationSystemGroup>();
        var simulationSystemGroup     = world.GetExistingSystem<SimulationSystemGroup>();
        var presentationSystemGroup   = world.GetExistingSystem<PresentationSystemGroup>();
        var systems                   = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default);

        systems.RemoveSwapBack(typeof(InitializationSystemGroup));
		systems.RemoveSwapBack(typeof(SimulationSystemGroup));
		systems.RemoveSwapBack(typeof(PresentationSystemGroup));
		
		BootstrapTools.InjectSystemsFromNamespace(systems, "Unity", world, simulationSystemGroup);
        BootstrapTools.InjectRootSuperSystems(systems, world, simulationSystemGroup);

        initializationSystemGroup.SortSystemUpdateList();
        simulationSystemGroup.SortSystemUpdateList();
        presentationSystemGroup.SortSystemUpdateList();

        ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);
        return true;
    }
}