using NeuralNetworkProject.Core;
using System;


namespace NeuralNetworkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] layers = { 1, 1, 1 };
            float[] input = {1};


            

            NeuralNetwork neuralNetwork = new NeuralNetwork(layers);
            var neurons=neuralNetwork.FeedForward(input);
            var weights = neuralNetwork.weights;
            var biases = neuralNetwork.biases;
            neuralNetwork.Mutate(0.75f, 0.05f);
            var mutatedNeurons = neuralNetwork.FeedForward(input);
            var mutatedWeights = neuralNetwork.weights;
            writeBiases(neuralNetwork.biases, neuralNetwork.weights);



            Console.ReadKey();
        }

        static void writeBiases(float[][] biases,float[][][] weights)
        {
            Console.Write("\nBias Matrix : \n");
            for (int i = 0; i < biases.Length; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < biases[i].Length; j++)
                {
                    
                    Console.Write("{0}\t", biases[i][j]);
                }
                Console.Write("\n");
            }

            Console.Write("\nWeight Matrix : \n");
            for (int i = 0; i < weights.Length; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < weights[i].Length; j++)
                {
                    Console.Write("\n");
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        Console.Write("{0}\t", weights[i][j][k]);

                    }
                    Console.Write("\n\n");
                }
            }
        }
    }
}
