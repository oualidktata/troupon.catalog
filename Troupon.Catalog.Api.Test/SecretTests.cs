//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Troupon.Catalog.Integration.Tests
//{
//  public class SecretTests : IDisposable
//  {

//    public SecretTests()
//    {
//      string variableId = "secretSimon";
//      string variableId2 = "dotnetAPISecret98";


//      //retrieve clients created with CLI (policy)
//      Client conjurClientDave = new Client("http://localhost:8080", "myConjurAccount");
//      Client admin = new Client("http://localhost:8080", "myConjurAccount");
//      //Login as Dave (human)
//      conjurClientDave.LogIn("Dave@BotApp", "3tqnbp0q3az402gv3m9736gj32j17vhxa96amqay31w9xaq1zmx3pk");
//      admin.LogIn("admin", "3mrc3h72yh2t781exb4ga3nyn2br30g825r1x25xj38292001v8khd5");



//      //Load policy to root with request variable Id

//      Policy policy = admin.Policy("root");
//      using (MemoryStream ms = new MemoryStream())
//      {

//        using (StreamWriter sw = new StreamWriter(ms))
//        {

//          sw.WriteLine("- !variable");
//          sw.WriteLine($"  id: {variableId}");
//          sw.WriteLine("- !variable");
//          sw.WriteLine($"  id: {variableId2}");
//          sw.Flush();
//          Stream policyOutputStream = policy.LoadPolicy(ms);
//          using (StreamReader reader = new StreamReader(policyOutputStream))
//          {
//            string policyLoadOutput = reader.ReadToEnd();
//            Console.WriteLine("Policy load successuly output: '{0}'", policyLoadOutput);
//          }
//        }
//      }

//      //variable creation
//      Variable conjurVariable = admin.Variable("secretSimon");
//      //adding secret
//      conjurVariable.AddSecret(Encoding.ASCII.GetBytes("secretFromSimon"));
//      //show secret
//      Console.WriteLine(conjurVariable.GetValue());



//      //Nom du compte client auquel l'utilisateur appartient
//      Console.WriteLine(conjurClientDave.GetAccountName());
//      //Nombre de var = 1 
//      Console.WriteLine(conjurClientDave.CountVariables().ToString());
//      //get value of secret var entered in Conjur = "thisIsASecretFromCLI")
//      Console.WriteLine(conjurClientDave.ListVariables().First().GetValue());
//      Console.WriteLine(conjurClientDave.ListVariables("secretVar").First().GetValue());
//    }

//    public void Test_Conjur_onOkd()
//    {
//      var client=
//    }
//    public void Dispose()
//    {
//      throw new NotImplementedException();
//    }
//  }
//}
