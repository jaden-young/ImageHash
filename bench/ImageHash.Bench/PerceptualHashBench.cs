namespace CoenM.ImageHash
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet;
    using BenchmarkDotNet.Attributes;
    using CoenM.ImageHash.HashAlgorithms;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    public class PerceptualHashBench
    {
        private static Image<Rgba32>[] ImageSuite;

        [GlobalSetup]
        public void GlobalSetup()
        {
            // Yes, this is beefy but we want everything in RAM before we do stuff
            ImageSuite = Directory.GetFiles(Path.Combine("Data", "image_suite"), "*.jpg")
                        .Select(fp => Image.Load<Rgba32>(fp))
                        .ToArray();
        }


        [Benchmark(Baseline = true)]
        public ulong[] PerceptualHash()
        {
            var alg = new PerceptualHash();
            var results = new ulong[ImageSuite.Length];
            for (int i = 0; i < ImageSuite.Length; i++)
            {
                var hash = alg.Hash(ImageSuite[i]);
                results[i] = hash;
            }
            return results;
        }

        [Benchmark]
        public ulong[] PerceptualHashOptimized()
        {
            var alg = new PerceptualHashOptimized();
            var results = new ulong[ImageSuite.Length];
            for (int i = 0; i < ImageSuite.Length; i++)
            {
                var hash = alg.Hash(ImageSuite[i]);
                results[i] = hash;
            }
            return results;
        }
    }
}
