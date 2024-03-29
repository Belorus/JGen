﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 11.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace JGen.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonStringReaderTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class JsonStringReaderTemplate : JsonStringReaderTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nusing System.IO;\r\nusing System.Text;\r\n\r\nnamespace JGen.Readers\r\n{\r\n\tpublic stat" +
                    "ic class JsonStringReader\r\n\t{\r\n\t\tpublic static string Read(TextReader textReader" +
                    ")\r\n\t\t{\r\n\t\t\tvar buffer = new StringBuilder(10);\r\n\r\n\t\t\tint ls = textReader.Read();" +
                    "\r\n\t\t\tif (ls  != \'\"\')\r\n\t\t\t\tthrow new JsonException(\"Invalid JSON string literal f" +
                    "ormat\");\r\n\r\n\t\t\tbuffer.Length = 0;\r\n\t\t\twhile (true)\r\n\t\t\t{\r\n\t\t\t\tint c = textReader" +
                    ".Read();\r\n\t\t\t\tif (c < 0)\r\n\t\t\t\t\tthrow new JsonException(\"JSON string is not close" +
                    "d\");\r\n\t\t\t\tif (c == \'\"\')\r\n\t\t\t\t\treturn buffer.ToString();\r\n\t\t\t\telse if (c != \'\\\\\')" +
                    "\r\n\t\t\t\t{\r\n\t\t\t\t\tbuffer.Append((char)c);\r\n\t\t\t\t\tcontinue;\r\n\t\t\t\t}\r\n\r\n\t\t\t\t// escaped e" +
                    "xpression\r\n\t\t\t\tc = textReader.Read();\r\n\t\t\t\tif (c < 0)\r\n\t\t\t\t\tthrow new JsonExcept" +
                    "ion(\"Invalid JSON string literal; incomplete escape sequence\");\r\n\t\t\t\tswitch (c)\r" +
                    "\n\t\t\t\t{\r\n\t\t\t\t\tcase \'\"\':\r\n\t\t\t\t\tcase \'\\\\\':\r\n\t\t\t\t\tcase \'/\':\r\n\t\t\t\t\t\tbuffer.Append((ch" +
                    "ar)c);\r\n\t\t\t\t\t\tbreak;\r\n\t\t\t\t\tcase \'b\':\r\n\t\t\t\t\t\tbuffer.Append(\'\\x8\');\r\n\t\t\t\t\t\tbreak;\r" +
                    "\n\t\t\t\t\tcase \'f\':\r\n\t\t\t\t\t\tbuffer.Append(\'\\f\');\r\n\t\t\t\t\t\tbreak;\r\n\t\t\t\t\tcase \'n\':\r\n\t\t\t\t\t" +
                    "\tbuffer.Append(\'\\n\');\r\n\t\t\t\t\t\tbreak;\r\n\t\t\t\t\tcase \'r\':\r\n\t\t\t\t\t\tbuffer.Append(\'\\r\');\r" +
                    "\n\t\t\t\t\t\tbreak;\r\n\t\t\t\t\tcase \'t\':\r\n\t\t\t\t\t\tbuffer.Append(\'\\t\');\r\n\t\t\t\t\t\tbreak;\r\n\t\t\t\t\tca" +
                    "se \'u\':\r\n\t\t\t\t\t\tushort cp = 0;\r\n\t\t\t\t\t\tfor (int i = 0; i < 4; i++)\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t" +
                    "\t\tcp <<= 4;\r\n\t\t\t\t\t\t\tif ((c = textReader.Read()) < 0)\r\n\t\t\t\t\t\t\t\tthrow new JsonExce" +
                    "ption(\"Incomplete unicode character escape literal\");\r\n\t\t\t\t\t\t\tif (\'0\' <= c && c " +
                    "<= \'9\')\r\n\t\t\t\t\t\t\t\tcp += (ushort)(c - \'0\');\r\n\t\t\t\t\t\t\tif (\'A\' <= c && c <= \'F\')\r\n\t\t\t" +
                    "\t\t\t\t\tcp += (ushort)(c - \'A\' + 10);\r\n\t\t\t\t\t\t\tif (\'a\' <= c && c <= \'f\')\r\n\t\t\t\t\t\t\t\tcp" +
                    " += (ushort)(c - \'a\' + 10);\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t\tbuffer.Append((char)cp);\r\n\t\t\t\t\t\tbrea" +
                    "k;\r\n\t\t\t\t\tdefault:\r\n\t\t\t\t\t\tthrow new JsonException(\"Invalid JSON string literal; u" +
                    "nexpected escape character\");\r\n\t\t\t\t}\r\n\t\t\t}\r\n\t\t}\r\n\t}\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public class JsonStringReaderTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
