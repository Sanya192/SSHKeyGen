using SSHKeyGenerator= SshKeyGenerator.SshKeyGenerator;

namespace SSHKeyGen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                // Elegant or not... Of course not but lol
                var keygen = new MultipleKeyGen(File.ReadAllLines(args[0]));
                keygen.outputDir = args[1];
                keygen.ExportKeys();
                Console.WriteLine("GG");
            }
            else
            {
                var keygen = new MultipleKeyGen(File.ReadAllLines("militaryAlphabet.txt"));
                keygen.ExportKeys();
                Console.WriteLine("GG");
            }
           
        }
    }

    class MultipleKeyGen
    {
        List<string> keyNames;
        Dictionary<string, SSHKeyGenerator> keyGens;
        const int KeySize = 4096;
        public  string outputDir = "GenKeys";

        public MultipleKeyGen(IEnumerable<string> keyNames)
        {
            this.keyNames = keyNames.ToList();
            keyGens = new Dictionary<string, SSHKeyGenerator>();

            //Fuck Random lol
            this.keyNames.ForEach(x => { Thread.Sleep(21);  keyGens.Add(x, new SSHKeyGenerator(KeySize)); });
        }

        public void ExportKeys()
        {
            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }
            Directory.CreateDirectory(outputDir);
            foreach (var item in keyGens)
            {
                Directory.CreateDirectory($"{outputDir}/{item.Key}");
                File.WriteAllText($"{outputDir}/{item.Key}/public", item.Value.ToPublicKey());
                File.WriteAllText($"{outputDir}/{item.Key}/private", item.Value.ToPrivateKey());
            }
        }
    }
}