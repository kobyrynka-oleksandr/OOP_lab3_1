namespace OOP_lab3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> files = new List<string>();
            string directoryPath = @"C:\C# Projects\OOP_lab3_1";
            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";
                string filePath = Path.Combine(directoryPath, fileName);
                files.Add(filePath);

            }

            List<string> noFile = new List<string>();
            List<string> badData = new List<string>();
            List<string> overflow = new List<string>();
            List<int> validProducts = new List<int>();

            try
            {
                foreach (var file in files)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(file);
                        try
                        {
                            string[] nums = new string[2];
                            for (int i = 0; i < nums.Length; i++)
                            {
                                nums[i] = lines[i];
                            }
                            int a = ParseInt(nums[0]);
                            int b = ParseInt(nums[1]);
                            checked
                            {
                                int product = a * b;
                                validProducts.Add(product);
                            }
                        }
                        catch (Exception ex) when (ex is IndexOutOfRangeException || ex is ArgumentNullException)
                        {
                            badData.Add(file);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        noFile.Add(file);
                    }
                    catch (FormatException)
                    {
                        badData.Add(file);
                    }
                    catch (OverflowException)
                    {
                        overflow.Add(file);
                    }
                }

                WriteToFile("no_file.txt", noFile);
                WriteToFile("bad_data.txt", badData);
                WriteToFile("overflow.txt", overflow);

                try
                {
                    double average = (double)validProducts.Sum() / validProducts.Count;
                    Console.WriteLine($"Average of valid products: {average}");
                }
                catch
                {
                    throw new Exception("No valid products found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to create or write to output files. " + ex.Message);
            }
        }
        static void WriteToFile(string filename, List<string> data)
        {
            string directoryPath = @"C:\C# Projects\OOP_lab3_1";
            string fullPath = Path.Combine(directoryPath, filename);
            try
            {
                File.WriteAllLines(fullPath, data);
            }
            catch
            {
                throw new IOException($"Unable to create or write to file {filename}");
            }
        }
        static int ParseInt(string line)
        {
            int result;
            try
            {
                result = int.Parse(line);
            }
            catch (FormatException)
            {
                throw new FormatException("Line is not a valid integer.");
            }
            return result;
        }
    }
}
