using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CodeDisco
{
    internal static class CodeDiscoClassificationDefinition
    {
        /// <summary>
        /// Defines the "CodeDisco" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CodeDisco")]
        internal static ClassificationTypeDefinition CodeDiscoType = null;
    }
}
