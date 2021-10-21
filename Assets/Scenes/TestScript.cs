using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using UnityEngine;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


public class TestScript : MonoBehaviour
{
    public const int IMAGE_HEIGHT = 240;
    public const int IMAGE_WIDTH = 320;

    const string INPUT_NAME = "X";
    const string OUTPUT_NAME = "Y";

    private const float IMAGE_MEAN = 127.5f;
    private const float IMAGE_STD = 127.5f;

    string modelPath = Application.streamingAssetsPath + "/model/best_model.onnx"; //@"/home/trojan/unity/Projects/HeadLocation/best_model.onnx";

    private string[] labels = {"head_left", "head_right", "none", "standing"}; 
    private string prediction;

    private string path = Application.streamingAssetsPath + "/image";
    public string[] image_list;

    public List<float[]> input_list = new List<float[]>();

    // Start is called before the first frame update
    void Start()
    {

        image_list = System.IO.Directory.GetFiles(@path, "*.png");
        int cnt = image_list.Length;
        Debug.Log($"Total input objects: {cnt}");

        for (int i=0; i<cnt; i++)
        {
            // Load image
            using Image<Rgb24> image = Image.Load<Rgb24>(image_list[i]);

            // Preporcess image
            Tensor<float> input = new DenseTensor<float>(new[] { 1, 3, IMAGE_HEIGHT, IMAGE_WIDTH});
            for (int y = 0; y < image.Height; y++)
            {
                Span<Rgb24> pixelSpan = image.GetPixelRowSpan(y);
                for (int x = 0; x < image.Width; x++)
                {
                    input[0, 0, y, x] = (pixelSpan[x].R - IMAGE_MEAN) / IMAGE_STD;
                    input[0, 1, y, x] = (pixelSpan[x].G - IMAGE_MEAN) / IMAGE_STD;
                    input[0, 2, y, x] = (pixelSpan[x].B - IMAGE_MEAN) / IMAGE_STD;
                }
            }

            // Setup inputs
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("X", input)
            };

            // var tmp = input.ToList().Max();
            // Debug.Log(tmp);
            var tmp = input.ToList();
            Debug.Log(tmp[1000] + "," + tmp[10000] + "," + tmp[100000]);
            
            // Run inference
            using var session = new InferenceSession(modelPath);
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);

            // Postprocess to get softmax vector
            IEnumerable<float> output = results.First().AsEnumerable<float>();
            float sum = output.Sum(x => (float)Math.Exp(x));
            IEnumerable<float> softmax = output.Select(x => (float)Math.Exp(x) / sum);

            var temp = softmax.ToList();
            Debug.Log(temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[3]);
            var maxIndex = (int)temp.IndexOf(temp.Max());

            prediction = labels[maxIndex];
            Debug.Log($"Network prediction for object {i} is: {prediction}");
        }
    }

}
