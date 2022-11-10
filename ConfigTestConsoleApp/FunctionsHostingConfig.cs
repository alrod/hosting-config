using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConfigTestConsoleApp
{
    public class FunctionsHostingConfig : Dictionary<string, string>
    {
        public bool SomeFeatureEnabled
        {
            get
            {
                if (this.TryGetValue("feature1", out string value))
                {
                    return value == "1";
                }
                return false;
            }
        }

    }
}
