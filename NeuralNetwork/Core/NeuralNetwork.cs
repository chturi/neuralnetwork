namespace NeuralNetworkProject.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class NeuralNetwork
    {
        private int[] layers;

        public float[][] neurons;

        public float[][] biases;

        public float[][][] weights;

        private int[] activations;

        public float fitness = 0;

        private Random random = new Random();

        public NeuralNetwork(int[] layers)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = layers[i];
            }

            InitNeurons();
            InitBiases();
            InitWeights();
        }

        private void InitNeurons()
        {
            List<float[]> neuronList = new List<float[]>();

            for (int i = 0; i < layers.Length; i++)
            {
                neuronList.Add(new float[layers[i]]);
            }

            neurons = neuronList.ToArray();
        }

        private void InitBiases()
        {
           
            List<float[]> biasList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    bias[j] = randomGenerator(0.5f, -0.5f);
                }
                biasList.Add(bias);
            }
            biases = biasList.ToArray();
        }

        private void InitWeights()
        {
            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j<neurons[i].Length; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {

                        neuronWeights[k] = randomGenerator(0.5f, -0.5f);

                    }
                    layerWeightsList.Add(neuronWeights);

                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }

        public float activate (float value)
        {
            return
                (float)Math.Tanh(value);
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i ++)
            {
                neurons[0][i] = inputs[i];
            }

            for (int i = 1; i < layers.Length; i++)
            {
                int layer = 1 - i;
                for(int j = 0; j < neurons[i].Length; j++)
                {
                    float value = 0f;
                    for(int k = 0; k < neurons[i-1].Length; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i][j]);
                }
            }
            return neurons[neurons.Length - 1];

        }

        //Compare fittness value of one Neural network to current instance
        public int CompareTo(NeuralNetwork other)
        {
            if (other == null)
                return 1;
            if (fitness > other.fitness)
                return 1;
            else if (fitness < other.fitness)
                return -1;
            else
                return 0;
        }

        //Load biases and weights from file

        public void Load(string path)
        {
            TextReader tr = new StreamReader(path);
            int numberOfLines = (int)new FileInfo(path).Length;
            string[] listLines = new string[numberOfLines];
            int lineIndex = 1;

            for (int i = 1; i < numberOfLines; i++)
            {
                listLines[i] = tr.ReadLine();
            }
            tr.Close();

            if (numberOfLines > 0)
            {
                for (int i = 0; i < biases.Length; i++)
                {
                    for (int j = 0; j < biases[i].Length; j++)
                    {
                        biases[i][j] = float.Parse(listLines[lineIndex++]);
                        lineIndex++;
                    }
                }

                for (int i = 0; i < weights.Length; i++)
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                       for (int k = 0; k < weights[i][j].Length; k++)
                        {
                            weights[i][j][k] = float.Parse(listLines[lineIndex]);
                            lineIndex++;
                        }
                    }
                }
            }
        }


        //Mutation of Neural Network parameter
        public void Mutate (float chance, float mutationValue)
        {
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    biases[i][j] = (random.NextDouble() <= (1-chance)) ? 
                        biases[i][j] += randomGenerator(mutationValue, -mutationValue) : biases[i][j];
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (random.NextDouble() <= (1 - chance)) ? 
                            weights[i][j][k] += randomGenerator(mutationValue,-mutationValue) : weights[i][j][k];
                    }
                }
            }
        }

        public float randomGenerator (float max,float min)
        {

            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;

        }

    }


}
