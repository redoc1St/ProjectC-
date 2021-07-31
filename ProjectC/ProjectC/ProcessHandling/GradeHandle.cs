using ProjectC.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectC.ProcessHandling
{
    class GradeHandle
    {
        
        internal static List<TestCase> loadTestCase(string TestCaseFolder)
        {
            List<TestCase> list = new List<TestCase>();
            string[] files = Directory.GetFiles(TestCaseFolder);
            for (int i = 0; i < files.Length; i++)
            {
                string filePath = files[i];
                TestCase tc = GradeHandle.GetFile(filePath);
                list.Add(tc);
            }
            return list;
        }

        private static TestCase GetFile(string filePath)
        {
            TestCase tc = new TestCase();
            StreamReader streamReader = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            /*string text = "";
            string text2;
            while ((text2 = streamReader.ReadLine()) != null)
            {
                text2 = text2.Trim();
                if (text2.ToUpper().StartsWith("INPUT:"))
                {
                    text = "INPUT:";
                }
                else if (text2.ToUpper().StartsWith("OUTPUT:"))
                {
                    text = "OUTPUT:";
                }
                else
                {
                    if (text.Equals("INPUT:"))
                    {
                        tc.Input.Add(text2);
                        //Console.WriteLine("INput"+text2);
                    }
                    if (text.Equals("OUTPUT:"))
                    {
                        //TestCase before = tc;
                        tc.Output = tc.Output + text2 + "\n";
                        // Console.WriteLine(" output :"+tc.Output);
                    }
                    if (text.Equals("MARK:"))
                    {
                        tc.Mark = Convert.ToSingle(text2);
                        break;
                    }
                }
                ///////////////////
            }*/
            string text2 = streamReader.ReadToEnd();
            string[] stringArr = text2.Split('|');

            for (int k = Array.IndexOf(stringArr, "OUTPUT:") + 1; k < stringArr.Length; k++)
            {

                tc.Output =tc.Output+ stringArr[k];

            }
            for (int j = Array.IndexOf(stringArr, "INPUT:") + 1; j < Array.IndexOf(stringArr, "OUTPUT:"); j++)
            {
                tc.Input.Add(stringArr[j]);
            }
            streamReader.Close();
            tc.Output = tc.Output.Trim();
            return tc;
        }
        
        internal static Answer Grade(string programPath, TestCase tc,string txtTestCase,int ques,ref string wrong)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            StreamWriter streamWriter = process.StandardInput;
            StreamReader streamReader = process.StandardOutput;
            //xem có mấy câu hỏi (mấy Q) để tí cho vào vòng for bên dưới để không fix cứng viết vào writeline........
            /* string[] filesCountQuestion = Directory.GetFiles("D:\C core\PFC_ST1");
             Console.WriteLine(filesCountQuestion.Length);*/
            //
            streamWriter.WriteLine(@"cd \");
            streamWriter.WriteLine(@"cd /d " + programPath);
           /* for (int i = 1; i < 6; i++) //tạm thời là cho có 5 câu từ 1-5
            {
                if (txtTestCase.Contains(@"\Q" + i))  //tự gọi đến câu đấy khi vào file đấy
                {
                    streamWriter.WriteLine("Q" + i + ".exe");
                }
            }*/

            streamWriter.WriteLine("Q"+ques+".exe");
            foreach (string item in tc.Input)
            {
                streamWriter.WriteLine(item);
            }
            streamWriter.Flush();
            streamWriter.Close();
            string text = "";
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                text += str;
            }
            process.WaitForExit();
            process.Close();
            int num = text.LastIndexOf("OUTPUT:");
            //Console.WriteLine("test"+test);
            string text2 = text.Substring(num + 7, text.Length - (num + 7 + (programPath.Length + 1)));
            text2 = text2.Trim();
            Answer answer = new Answer();
            answer.IsPassed = tc.Output.Equals(text2);
            answer.Mark = (answer.IsPassed ? tc.Mark : 0f);
            if (answer.IsPassed == true)
            {
                answer.Mark = 1;

            }
            else
            {
                wrong+= "Output of Student in Question " + ques + " is " + tc.Output + 
                    "\nOutput of Testcase in Question " + ques + " is" + text2;

            }
            Console.WriteLine("Test case output" + tc.Output.Trim());
            Console.WriteLine("text2 after exe:" + text2 + "\n");
            return answer;

        }
    }
}
