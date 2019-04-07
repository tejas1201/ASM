using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NHT.ASM.Helpers
{
    public class RemoveEntityModelsFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            IDictionary<string, Schema> pairs = new Dictionary<string, Schema>();
            foreach (var definition in swaggerDoc.Definitions)
            {
                if (definition.Key.Contains("Dto") || definition.Key == "ProblemDetails")
                    pairs.Add(definition);
            }
            swaggerDoc.Definitions.Clear();
            swaggerDoc.Definitions = pairs;
        }
    }
}
