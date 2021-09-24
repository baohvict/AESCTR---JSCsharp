using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            RunJS();
            Console.ReadLine();
        }
        public static void RunJS()
        {
            var engine = new Jint.Engine();
            engine.Execute(File.ReadAllText("index.js"));
            engine.SetValue("strKey", "mjU8DYL2PJcT1ZFf");
            engine.SetValue("dataRaw", "Hello test");
            engine.Execute(@"
                    var key = aesjs.utils.utf8.toBytes(strKey);
                    var textBytes = aesjs.utils.utf8.toBytes(dataRaw);
                    var aesCtr = new aesjs.ModeOfOperation.ctr(key, new aesjs.Counter(5));
                    var encryptedBytes = aesCtr.encrypt(textBytes);
                    var encryptedHex = aesjs.utils.hex.fromBytes(encryptedBytes);
            ");   
            var result = engine.GetValue("encryptedHex");
            Console.WriteLine(result);
            engine.Execute(@"
                var encryptedBytes = aesjs.utils.hex.toBytes(encryptedHex);
                var aesCtr = new aesjs.ModeOfOperation.ctr(key, new aesjs.Counter(5));
                var decryptedBytes = aesCtr.decrypt(encryptedBytes);               
                var decryptedText = aesjs.utils.utf8.fromBytes(decryptedBytes);    
            ");
            var decryptedText = engine.GetValue("decryptedText");
            Console.WriteLine(decryptedText);
        }
    }
}
