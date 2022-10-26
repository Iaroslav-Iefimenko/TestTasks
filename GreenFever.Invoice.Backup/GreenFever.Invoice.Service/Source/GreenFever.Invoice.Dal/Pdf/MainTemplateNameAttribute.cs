using System;

namespace GreenFever.Invoice.Dal.Pdf
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MainTemplateNameAttribute : Attribute
    {
        public MainTemplateNameAttribute(string fileName)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// Need to contain the template name.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string FileName { get; }
    }
}
