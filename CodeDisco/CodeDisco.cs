using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using CodeDiscoPlayer;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace CodeDisco
{
    /// <summary>
    /// This class causes a classifier to be added to the set of classifiers. Since 
    /// the content type is set to "text", this classifier applies to all text files
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")]
    internal class CodeDiscoProvider : IClassifierProvider
    {
        /// <summary>
        /// Import the classification registry to be used for getting a reference
        /// to the custom classification type later.
        /// </summary>
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty<CodeDisco>(delegate { return new CodeDisco(ClassificationRegistry); });
        }
    }

    /// <summary>
    /// Classifier that classifies all text as an instance of the OrinaryClassifierType
    /// </summary>
    class CodeDisco : IClassifier
    {
        readonly IClassificationType _classificationType;
        readonly Router _router = new Router(new Player());

        internal CodeDisco(IClassificationTypeRegistryService registry)
        {
            _classificationType = registry.GetClassificationType("CodeDisco");
        }

        /// <summary>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </summary>
        /// <param name="span">The span currently being classified</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var lastLine = GetLastLine(span);

            if (lastLine.Length > 0)
            {
                _router.RouteKey(lastLine.GetText()[lastLine.Length - 1]);
            }

            return new List<ClassificationSpan>();
        }

        /// <summary>
        /// Get the last line included in the SnapshotSpan
        /// </summary>
        private static ITextSnapshotLine GetLastLine(SnapshotSpan span)
        {
            return span.Length > 0
                ? span.End.Subtract(1).GetContainingLine()
                : GetStartLine(span);
        }

        private static ITextSnapshotLine GetStartLine(SnapshotSpan span)
        {
            return span.Start.GetContainingLine();
        }

#pragma warning disable 67
        // This event gets raised if a non-text change would affect the classification in some way,
        // for example typing /* would cause the classification to change in C# without directly
        // affecting the span.
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
}
