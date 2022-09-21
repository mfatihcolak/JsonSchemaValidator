using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json.Schema;

//var schema = JSchema.Parse(File.ReadAllText(@"C:\Users\alper\Desktop\BookShop\JsonSchemadotNet\schemaone.json"));


JObject patient = JObject.Parse(@"{
    'id': 1,
    'name':'fatih',
    'gender':'male',
    'phone':'(555)555-5555',
    'address': {'street': 'kalemkar sokak','city':'Burdur','country': 'turkey','postCode':15100}
}"); 

using (StreamReader file = File.OpenText(@"C:\Users\pardu\Source\Repos\JsonSchemaValidatorr\JsonSchemaValidatorr\JsonSchema.json"))
using (JsonTextReader reader = new JsonTextReader(file))
{
    JSchemaUrlResolver resolver = new JSchemaUrlResolver();

    JSchema schema2 = JSchema.Load(reader, new JSchemaReaderSettings
    {
        Resolver = resolver,
        // where the schema is being loaded from
        // referenced 'address.json' schema will be loaded from disk at 'c:\address.json'
        BaseUri = new Uri(@"C:\Users\pardu\Source\Repos\JsonSchemaValidatorr\JsonSchemaValidatorr\JsonSchema.json")
    });

    Console.WriteLine(schema2);
    var valid = patient.IsValid(schema2, out IList<string> messages);
    Console.WriteLine(valid);
    // validate JSON
    foreach (var message in messages)
    {
        Console.WriteLine(message);
    }
}


//var valid = patient.IsValid(schema2, out IList<string> messages);
//Console.WriteLine(valid);