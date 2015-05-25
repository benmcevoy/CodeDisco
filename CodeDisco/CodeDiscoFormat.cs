using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CodeDisco
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the CodeDisco type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CodeDisco")]
    [Name("CodeDisco")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class CodeDiscoFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "CodeDisco" classification type
        /// </summary>
        public CodeDiscoFormat()
        {
            //human readable version of the name
            this.DisplayName = "CodeDisco"; 
            


        }
    }
    #endregion //Format definition
}
