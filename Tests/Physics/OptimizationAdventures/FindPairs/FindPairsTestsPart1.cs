﻿using NUnit.Framework;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.PerformanceTesting;

namespace Latios.PhysicsEngine.Tests
{
    public class FindPairsTestsPart1
    {
        //This version biases towards smaller boxes.
        [BurstCompile]
        public struct GenerateRandomAabbs : IJob
        {
            public Random                  random;
            public NativeArray<AabbEntity> aabbs;

            public void Execute()
            {
                float3 neg = new float3(-100000f, -100000f, -100000f);
                float3 pos = new float3(100000f, 100000f, 100000f);

                for (int i = 0; i < aabbs.Length; i++)
                {
                    float3 min  = random.NextFloat3(neg, pos);
                    float3 max  = random.NextFloat3(min, pos);
                    float3 diff = max - min;
                    float3 mult = random.NextFloat3();
                    mult        = mult * mult;
                    max         = min + diff * mult;

                    AabbEntity aabbEntity = new AabbEntity
                    {
                        entity = new Entity
                        {
                            Index   = i,
                            Version = 0
                        },
                        aabb = new AABB
                        {
                            min = min,
                            max = max
                        }
                    };
                    aabbs[i] = aabbEntity;
                }
            }
        }

        public void SweepPerformanceTests(int count, uint seed, int preallocate = 1)
        {
            Random random = new Random(seed);
            random.InitState(seed);
            NativeArray<AabbEntity> randomAabbs = new NativeArray<AabbEntity>(count, Allocator.TempJob);
            var                     jh          = new GenerateRandomAabbs { random = random, aabbs = randomAabbs }.Schedule();
            jh                                  = randomAabbs.SortJob(jh);
            jh.Complete();

            NativeList<EntityPair> pairsNaive     = new NativeList<EntityPair>(preallocate, Allocator.TempJob);
            NativeList<EntityPair> pairsBool4     = new NativeList<EntityPair>(preallocate, Allocator.TempJob);
            NativeList<EntityPair> pairsLessNaive = new NativeList<EntityPair>(preallocate, Allocator.TempJob);
            NativeList<EntityPair> pairsFunny     = new NativeList<EntityPair>(preallocate, Allocator.TempJob);
            NativeList<EntityPair> pairsBetter    = new NativeList<EntityPair>(preallocate, Allocator.TempJob);
            NativeList<EntityPair> pairsNew       = new NativeList<EntityPair>(preallocate, Allocator.TempJob);

            SampleUnit unit = count > 999 ? SampleUnit.Millisecond : SampleUnit.Microsecond;

            Measure.Method(() => { new NaiveSweep { aabbs = randomAabbs, overlaps = pairsNaive }.Run(); })
            .Definition("NaiveSweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            Measure.Method(() => { new Bool4Sweep { aabbs = randomAabbs, overlaps = pairsBool4 }.Run(); })
            .Definition("Bool4Sweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            Measure.Method(() => { new LessNaiveSweep { aabbs = randomAabbs, overlaps = pairsLessNaive }.Run(); })
            .Definition("LessNaiveSweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            Measure.Method(() => { new FunnySweep { aabbs = randomAabbs, overlaps = pairsFunny }.Run(); })
            .Definition("FunnySweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            Measure.Method(() => { new BetterSweep { aabbs = randomAabbs, overlaps = pairsBetter }.Run(); })
            .Definition("BetterSweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            Measure.Method(() => { new NewSweep { aabbs = randomAabbs, overlaps = pairsNew }.Run(); })
            .Definition("NewSweep", unit)
            .WarmupCount(0)
            .MeasurementCount(1)
            .Run();

            UnityEngine.Debug.Log("Pairs: " + pairsNaive.Length);
            UnityEngine.Debug.Log("Pairs: " + pairsBetter.Length);
            UnityEngine.Debug.Log("Pairs: " + pairsNew.Length);

            randomAabbs.Dispose();
            pairsNaive.Dispose();
            pairsBool4.Dispose();
            pairsLessNaive.Dispose();
            pairsFunny.Dispose();
            pairsBetter.Dispose();
            pairsNew.Dispose();
        }

        [Test, Performance]
        public void Sweep_10()
        {
            SweepPerformanceTests(10, 1);
        }

        [Test, Performance]
        public void Sweep_100()
        {
            SweepPerformanceTests(100, 56);
        }

        [Test, Performance]
        public void Sweep_1000()
        {
            SweepPerformanceTests(1000, 76389);
        }

        [Test, Performance]
        public void Sweep_10000()
        {
            SweepPerformanceTests(10000, 2348976);
        }

        [Test, Performance]
        public void Sweep_20()
        {
            SweepPerformanceTests(20, 1);
        }

        [Test, Performance]
        public void Sweep_200()
        {
            SweepPerformanceTests(200, 76);
        }

        [Test, Performance]
        public void Sweep_2000()
        {
            SweepPerformanceTests(2000, 56324);
        }

        [Test, Performance]
        public void Sweep_20000()
        {
            SweepPerformanceTests(20000, 2980457);
        }

        [Test, Performance]
        public void Sweep_50()
        {
            SweepPerformanceTests(50, 1);
        }

        [Test, Performance]
        public void Sweep_500()
        {
            SweepPerformanceTests(500, 23);
        }

        [Test, Performance]
        public void Sweep_5000()
        {
            SweepPerformanceTests(5000, 47893);
        }

        [Test, Performance]
        public void Sweep_50000()
        {
            SweepPerformanceTests(50000, 237648);
        }

        //[Test, Performance]
        //public void Sweep_100000()
        //{
        //    SweepPerformanceTests(100000, 234896);
        //}
    }
}

