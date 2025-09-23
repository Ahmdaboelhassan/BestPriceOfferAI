using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BestPriceOffer.Helpers
{
    public class EmbeddingHelper
    {
        private readonly InferenceSession _session;
        private readonly Dictionary<string, float[]> _labelEmbeddings;

        public EmbeddingHelper(string modelPath, IEnumerable<string> standardLabels)
        {
            _session = new InferenceSession(modelPath);

            // Precompute embeddings for your known labels
            _labelEmbeddings = standardLabels.ToDictionary(
                label => label,
                label => GetEmbedding(label)
            );
        }

        public string MapToStandardLabel(string headerText)
        {
            var headerEmbedding = GetEmbedding(headerText);

            string bestLabel = "Unknown";
            double bestScore = -1;

            foreach (var kvp in _labelEmbeddings)
            {
                var score = CosineSimilarity(headerEmbedding, kvp.Value);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestLabel = kvp.Key;
                }
            }

            return bestLabel;
        }

        private float[] GetEmbedding(string text)
        {
            // ⚠️ This depends on your ONNX model.
            // If your model has input "text" → string tensor, this works.
            // If not, you must tokenize and provide "input_ids", "attention_mask".

            var input = new DenseTensor<string>(new[] { 1, 1 });
            input[0, 0] = text;
            int maxLen = 512;
            var inputIds = new DenseTensor<long>(new[] { 1, maxLen });
            var attentionMask = new DenseTensor<long>(new[] { 1, maxLen });
            var tokenTypeIds = new DenseTensor<long>(new[] { 1, maxLen });

            // Fill inputIds and attentionMask from tokenizer
            // tokenTypeIds is usually all zeros if you're just embedding single sentences
            for (int i = 0; i < maxLen; i++)
            {
                tokenTypeIds[0, i] = 0;
            }

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids", inputIds),
                NamedOnnxValue.CreateFromTensor("attention_mask", attentionMask),
                NamedOnnxValue.CreateFromTensor("token_type_ids", tokenTypeIds),
            };

            using var results = _session.Run(inputs);

            return results.First().AsTensor<float>().ToArray();
        }

        private static double CosineSimilarity(float[] v1, float[] v2)
        {
            double dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }
            return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
